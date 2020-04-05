using System;
using System.Collections.Generic;
using UserManagment.Models;
using UserManagment.Infrastructure;
using System.IO;
using System.Linq;
using UserManagment.Persistence.Context;
using UserManagment.Persistence.Repositories;
using Microsoft.AspNetCore.Http;

namespace UserManagment.Services
{
    public class DefaultFileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUserRepository _userRepository;

        public DefaultFileService(IFileRepository fileRepository, IUserRepository userRepository)
        {
            _fileRepository = fileRepository;
            _userRepository = userRepository;
        }

        public void AddFileToUser(string fileId, string userId)
        {

            var file = GetFileMetadata(fileId);

            if (file.UserId.NotEmpty() || !_userRepository.IsUserExist(userId))
            {
                throw new ArgumentException($"The file id: {fileId} is already set, or user id: {userId} is not exist");
            }

            file.UserId = userId;
            UpdateFile(file);

        }

        public void UploadFile(IFormFile file, FileModel fileModel)
        {
            try
            {
                /**/
                using (var stream = new FileStream(fileModel.FilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            catch (Exception)
            {
                throw new ArgumentException($"Error uploading file id: {fileModel.Id}");
            }


        }

        public void CreateFile(FileModel fileModel)
        {

            var dbFile = _fileRepository.GetFile(fileModel.Id);

            if (dbFile.IsNotNull())
            {
                throw new ArgumentException($"The file id: {fileModel.Id} is already exist");
            }

            _fileRepository.CreateFile(fileModel);

        }

        public void DeleteFile(string fileId)
        {
            var dbFile = _fileRepository.GetFile(fileId);

            if (dbFile.IsNull() || dbFile.Status == EntityStatus.Deleted)
            {
                throw new ArgumentException($"The file id: {fileId} is not exist");
            }

            _fileRepository.DeleteFile(fileId);
        }

        public FileModel GetFileWithData(string fileId)
        {
            var fileMetadata = GetFileMetadata(fileId);

            if (!File.Exists(fileMetadata.FilePath))
            {
                throw new FileNotFoundException("The file data is not exist", fileMetadata.Name);
            }

            var content = File.ReadAllBytes(fileMetadata.FilePath);

            fileMetadata.Content = content;

            return fileMetadata;
        }

        public FileModel GetFileMetadata(string fileId)
        {
            var dbFile = _fileRepository.GetFile(fileId);

            if (dbFile.IsNull() || !dbFile.ValidateStatus())
            {
                throw new ArgumentException($"The file id: {fileId} is not exist");
            }

            return dbFile;
        }

        public List<FileModel> GetFiles()
        {
            var files = _fileRepository.GetFiles();
            return files;
        }

        public void UpdateFile(FileModel file)
        {
            var dbFile = GetFileMetadata(file.Id);

            if (file.Name != string.Empty && file.Name != dbFile.Name)
            {
                MapFileContentPath(file, dbFile);
            }

            _fileRepository.UpdateFile(file);
        }

        private void MapFileContentPath(FileModel file, FileModel dbFile)
        {
            try
            {
                var newFilePath = GenerateNewFilePath(file, dbFile);

                File.Delete(newFilePath);

                File.Move(dbFile.FilePath, newFilePath);
            }
            catch (Exception)
            {
                throw new FileLoadException($"Can't rename file id: {file.Id}", file.Name);
            }

        }

        private string GenerateNewFilePath(FileModel file, FileModel dbFile)
        {
            var prefixCurrentFile = dbFile.UniqueFileName.EvaluateFileName(true);

            var newFullFileName = prefixCurrentFile + "_" + file.Name;

            var directoryName = Path.GetDirectoryName(dbFile.FilePath);

            var newFilePath = Path.Combine(directoryName, newFullFileName);

            file.UniqueFileName = newFullFileName;
            file.FilePath = newFilePath;

            return newFilePath;
        }
    }
}
