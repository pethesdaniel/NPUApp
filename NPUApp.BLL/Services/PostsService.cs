using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NPUApp.BLL.Model.RequestDTOs;
using NPUApp.BLL.Model.DTOs;
using NPUApp.Database.Context;
using NPUApp.Database.Models;

namespace NPUApp.BLL.Services
{
    public class PostsService
    {
        private NpuAppDbContext _context { get; set; }
        private UserService _userService { get; set; }
        private IMapper _mapper { get; set; }
        private IValidator<CreateOrEditPostDto> _postCreateDtoValidator { get; set; }
        public PostsService(NpuAppDbContext context, UserService userService, IMapper mapper, IValidator<CreateOrEditPostDto> postCreateDtoValidator)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _postCreateDtoValidator = postCreateDtoValidator;
        }

        public async Task<List<PostDto>> GetRecentPosts()
        {
            return await _context.NpuPosts
                .Include(x => x.Ratings)
                .Where(x => x.CreatedOn - DateTime.UtcNow < TimeSpan.FromHours(24))
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PostDto> GetPostById(long postId)
        {
            return await _context.NpuPosts
                .Include(x => x.Ratings)
                .Where(x => x.Id == postId)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() ?? throw new ArgumentException("No such postId!");
        }

        public async Task<List<PostDto>> SearchPosts(SearchPostDto dto)
        {
            var query = _context.NpuPosts.AsQueryable();

            if(dto.TitleContains != null)
            {
                query = query.Where(x => x.Title.ToLower().Contains(dto.TitleContains.ToLower()));
            }

            if(dto.FromUser != null)
            {
                query = query.Where(x => x.UserId == dto.FromUser);
            }

            if(dto.Parts != null && dto.Parts.Count > 0)
            {
                query = query.Include(x => x.Parts);
                foreach (var dtoPart in dto.Parts)
                {
                    if(dtoPart.NameContains != null)
                    {
                        query = query.Where(x => x.Parts.Any(p => p.FriendlyName.ToLower().Contains(dtoPart.NameContains.ToLower())));
                    } else if(dtoPart.PartNumber != null)
                    {
                        query = query.Where(x => x.Parts.Any(p => p.PartNumber == dtoPart.PartNumber));
                    }
                }
            }

            return await query
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task CreatePost(CreateOrEditPostDto postDto)
        {
            var isDtoValid = _postCreateDtoValidator.Validate(postDto);

            if (!isDtoValid.IsValid)
            {
                throw new AggregateException(isDtoValid.Errors.Select(e => new ArgumentException(e.ErrorMessage, e.ErrorCode)));
            }

            var user = await _userService.GetAuthorizedUser();

            if(user == null)
            {
                throw new InvalidOperationException();
            }

            var parts = await _context.Parts.Where(p => postDto.Parts.Contains(p.PartNumber)).ToListAsync();

            if(parts.Count == 0)
            {
                throw new ArgumentException("No parts provided.", "Parts");
            }

            var dbo = new NpuPost
            {
                Parts = parts,
                PictureUrl = postDto.PictureUrl,
                UserId = user.Id,
                Title = postDto.Title,
                CreatedOn = DateTime.UtcNow,
            };

            _context.NpuPosts.Add(dbo);

            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(long postId)
        {
            var post = await _context.NpuPosts.Where(x => x.Id == postId).FirstOrDefaultAsync();

            if(post == null)
            {
                throw new ArgumentException("No such post id.", "postId");
            }

            var user = await _userService.GetAuthorizedUser();

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            if(post.UserId != user.Id)
            {
                throw new ArgumentException("Not authorized to delete another user's post", "Authorization");
            }

            post.Parts.Clear();

            _context.Ratings.RemoveRange(post.Ratings);

            _context.NpuPosts.Remove(post);

            await _context.SaveChangesAsync();
        }
    }
}
