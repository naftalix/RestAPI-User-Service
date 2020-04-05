using System;
using System.Collections.Generic;
using System.Linq;
using UserManagment.Models;
using UserManagment.Persistence.Context;

namespace UserManagment.Persistence.Repositories
{
    public class FileRepository : BaseRepository, IFileRepository
    {
        public FileRepository(UserManagmentContext context) : base(context)
        {
        }

        public void CreateFile(FileModel fileModel)
        {
            fileModel.Status = EntityStatus.Active;
            _context.Files.Add(fileModel);
            _context.SaveChanges();
        }

        public void DeleteFile(string fileId)
        {
            var dbFile = _context.Files.Find(fileId);

            dbFile.Status = EntityStatus.Deleted;
            UpdateFile(dbFile);

        }

        public FileModel GetFile(string fileId)
        {
            var dbFile = _context.Files.Find(fileId);

            return dbFile;


        }

        public List<FileModel> GetFiles()
        {
            var files = _context.Files.ToList();
            return files;
        }

        public void UpdateFile(FileModel fileModel)
        { 
            _context.Files.Update(fileModel);
            _context.SaveChanges();

        }
    }
}
