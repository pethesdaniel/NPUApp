using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPUApp.BLL.Model.RequestDTOs;
using NPUApp.BLL.Services;

namespace NPUApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private RatingsService _ratingsService { get; set; }
        private UserService _userService { get; set; }
        public RatingsController(RatingsService ratingsService, UserService userService)
        {
            _ratingsService = ratingsService;
            _userService = userService;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> RatePost(CreateOrEditRatingDto dto)
        {
            try
            {
                await _ratingsService.RatePost(dto);
            } catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteRating([FromQuery] long postId)
        {
            try
            {
                var user = await _userService.GetAuthorizedUser();
                if (user == null) throw new InvalidOperationException("Not authorized");
                await _ratingsService.DeleteRating(postId, user.Id);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
