using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Apps.UserApi.DTOs;
using WebApplication1.Data.Entities;

namespace WebApplication1.Apps.UserApi.Profiles
{
    public class UserApiProfile:Profile
    {
        public UserApiProfile()
        {
            CreateMap<AppUser, UserGetDto>();
        }
    }
}
