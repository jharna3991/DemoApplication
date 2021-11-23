using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TodoApplication.Models;

namespace TodoApplication.Helpers
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<UserModel, RegisterUser>().ReverseMap();
            CreateMap<UserModel, LoginModel>().ReverseMap();

        }
    }
}