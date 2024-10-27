using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NPUApp.BLL.Model.DTOs;
using NPUApp.BLL.Model.RequestDTOs;
using NPUApp.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Services
{
    public class PartsService
    {
        private NpuAppDbContext _context { get; set; }
        private IMapper _mapper { get; set; }
        public PartsService(NpuAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IList<PartDto>> GetValidParts()
        {
            return await _context.Parts.ProjectTo<PartDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
