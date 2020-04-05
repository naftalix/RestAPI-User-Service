using System;
using AutoMapper;
using UserManagment.Models;
using UserManagment.Resources;

namespace UserManagment.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<FileResource, FileModel>();
        }
    }
}
