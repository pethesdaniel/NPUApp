using AutoMapper;
using NPUApp.BLL.Model.DTOs;
using NPUApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NpuPost, PostDto>()
                .ForMember(p => p.UserName, x => x.MapFrom(s => s.User.Username));
        }
    }
}
