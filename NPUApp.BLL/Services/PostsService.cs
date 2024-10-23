using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NPUApp.BLL.Model.DTOs;
using NPUApp.Database.Context;

namespace NPUApp.BLL.Services
{
    public class PostsService
    {
        private NpuAppDbContext _context { get; set; }
        private IMapper _mapper { get; set; }
        public PostsService(NpuAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PostDto>> GetRecentPosts()
        {
            return await _context.NpuPosts
                .Where(x => x.CreatedOn - DateTime.UtcNow < TimeSpan.FromHours(24))
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
