using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using UserManagment.Models;

namespace UserManagment.Services
{
    public interface IFileService
    {
        public List<FileModel> GetFiles();
        public FileModel GetFileMetadata(string fileId);
        public FileModel GetFileWithData(string fileId);
        public void UpdateFile(FileModel fileId);
        public void DeleteFile(string fileId);
        public void CreateFile(FileModel fileModel);
        public void AddFileToUser(string fileId, string userId);
        public void UploadFile(IFormFile file, FileModel resource);


    }
}
