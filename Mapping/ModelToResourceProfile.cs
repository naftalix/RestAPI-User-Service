using System;
using AutoMapper;
using UserManagment.Models;
using UserManagment.Resources;

namespace UserManagment.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<FileModel, FileResource>();
        }
    }
}
