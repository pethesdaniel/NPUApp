using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
        private IMapper _mapper { get; set; }
        private IValidator<CreateOrEditRatingDto> _ratingDtoValidator { get; set; }
        public RatingsService(NpuAppDbContext context, IMapper mapper, IValidator<CreateOrEditRatingDto> ratingDtoValidator)
        {
            _context = context;
            _mapper = mapper;
            _ratingDtoValidator = ratingDtoValidator;
        }
        public async Task RatePost(CreateOrEditRatingDto dto)
        {
            var validationResult = _ratingDtoValidator.Validate(dto);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException(string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var post = await _context.NpuPosts.FirstOrDefaultAsync(x => x.Id == dto.PostId);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);

            if(post == null || user == null)
            {
                throw new ArgumentException("Missing entry for user and post");
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
                    Post = post,
                    User = user,
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

            if(rating != null)
            {
                _context.Remove(rating);

                await _context.SaveChangesAsync();
                return;
            }

            throw new ArgumentException("No such existing rating");
        }
    }
}
