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
            CreateMap<Part, long>().ConvertUsing(x => x.PartNumber);
            CreateMap<NpuPost, PostDto>()
                .ForMember(p => p.UserName, x => x.MapFrom(s => s.User.Username))
                .ForMember(p => p.AvgCreativityScore, x => x.MapFrom(s => s.Ratings.Select(r => r.CreativityScore).DefaultIfEmpty().Average()))
                .ForMember(p => p.AvgUniquenessScore, x => x.MapFrom(s => s.Ratings.Select(r => r.UniquenessScore).DefaultIfEmpty().Average()));

            CreateMap<Part, PartDto>();
        }
    }
}
