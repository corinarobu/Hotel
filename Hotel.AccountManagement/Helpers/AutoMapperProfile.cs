using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.AccountManagement.DTOs;
using Hotel.AccountManagement.Entities;

namespace Hotel.AccountManagement.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() {
            CreateMap<RegisterDto, User>();
        }
    }
}
