using System;
using System.Collections.Generic;
using UserManagment.Models;
using UserManagment.Persistence.Context;

namespace UserManagment.Persistence.Repositories
{
    public interface IFileRepository
    {
        public List<FileModel> GetFiles();
        public FileModel GetFile(string fileId);
        public void UpdateFile(FileModel fileModel);
        public void DeleteFile(string fileId);
        public void CreateFile(FileModel fileModel);
    }
}
