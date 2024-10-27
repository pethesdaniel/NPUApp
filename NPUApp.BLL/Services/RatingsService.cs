using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NPUApp.BLL.Model.RequestDTOs;
using NPUApp.Database.Context;
using NPUApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Services
{
    public class RatingsService
    {
        private NpuAppDbContext _context { get; set; }
        private UserService _userService { get; set; }
        private IMapper _mapper { get; set; }
        private IValidator<CreateOrEditRatingDto> _ratingDtoValidator { get; set; }
        public RatingsService(NpuAppDbContext context, UserService userService, IMapper mapper, IValidator<CreateOrEditRatingDto> ratingDtoValidator)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _ratingDtoValidator = ratingDtoValidator;
        }
        public async Task RatePost(CreateOrEditRatingDto dto)
        {
            var validationResult = _ratingDtoValidator.Validate(dto);

            if (!validationResult.IsValid)
            {
                throw new AggregateException(validationResult.Errors.Select(e => new ArgumentException(e.ErrorMessage)));
            }

            var post = await _context.NpuPosts.FirstOrDefaultAsync(x => x.Id == dto.PostId);

            var user = await _userService.GetAuthorizedUser();

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            if (post == null)
            {
                throw new ArgumentException("No such post id!", "postId");
            }

            var existingRating = await _context.Ratings.Where(x => x.UserId == user.Id && x.PostId == x.Id).FirstOrDefaultAsync();
            if(existingRating != null)
            {
                existingRating.CreativityScore = dto.CreativityScore;
                existingRating.UniquenessScore = dto.UniquenessScore;
            } else 
            {
                var dbo = new Rating
                {
                    PostId = post.Id,
                    UserId = user.Id,
                    CreativityScore = dto.CreativityScore,
                    UniquenessScore = dto.UniquenessScore
                };

                _context.Ratings.Add(dbo);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRating(long postId, long userId)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);

            if(rating == null)
            {
                throw new ArgumentException("No such existing rating", "postId");
                
            }

            var user = await _userService.GetAuthorizedUser();

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            if (rating.UserId != user.Id)
            {
                throw new ArgumentException("Not authorized to delete another user's rating", "Authorization");
            }

            _context.Remove(rating);

            await _context.SaveChangesAsync();
            return;
        }
    }
}
