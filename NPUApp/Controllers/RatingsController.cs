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
        public RatingsController(RatingsService ratingsService)
        {
            _ratingsService = ratingsService;
        }

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

        [HttpDelete]
        public async Task<IActionResult> DeleteRating([FromQuery] long postId, [FromQuery] long userId)
        {
            try
            {
                await _ratingsService.DeleteRating(postId, userId);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
