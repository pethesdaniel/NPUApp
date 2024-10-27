using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPUApp.BLL.Model.RequestDTOs;
using NPUApp.BLL.Model.DTOs;
using NPUApp.BLL.Services;
using Microsoft.AspNetCore.Authorization;

namespace NPUApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private PostsService _postsService { get; set; }
        private UserService _userService { get; set; }

        public PostsController(PostsService postsService, UserService userService)
        {
            _postsService = postsService;
            _userService = userService;
        }

        [HttpGet("recent")]
        public async Task<IEnumerable<PostDto>> GetRecentPosts()
        {
            return await _postsService.GetRecentPosts();
        }

        [HttpGet]
        public async Task<PostDto> GetPostById([FromQuery] long postId)
        {
            return await _postsService.GetPostById(postId);
        }

        [HttpPost("search")]
        public async Task<IEnumerable<PostDto>> SearchPosts(SearchPostDto searchPostDto)
        {
            return await _postsService.SearchPosts(searchPostDto);
        }

        [Authorize]
        [HttpGet("mine")]
        public async Task<IEnumerable<PostDto>> MyPosts()
        {
            var user = await _userService.GetAuthorizedUser();
            if (user == null) throw new InvalidOperationException("Not authorized");
            return await _postsService.SearchPosts(new SearchPostDto { FromUser = user.Id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateNewPost(CreateOrEditPostDto postDto)
        {
            try
            {
                await _postsService.CreatePost(postDto);
            }
            catch (AggregateException e)
            {
                foreach(var ex in e.InnerExceptions)
                {
                    ModelState.AddModelError((ex as ArgumentException)?.ParamName ?? "", ex.Message);
                    return ValidationProblem(ModelState);
                }
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(e.ParamName ?? "", e.Message);
                return ValidationProblem(ModelState);
            }
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeletePost(long postId)
        {
            try
            {
                await _postsService.DeletePost(postId);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(e.ParamName ?? "", e.Message);
                return ValidationProblem(ModelState);
            }
            return Ok();
        }
    }
}
