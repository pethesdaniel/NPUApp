using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPUApp.BLL.Model.DTOs;
using NPUApp.BLL.Services;

namespace NPUApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private PostsService _postsService { get; set; }
        public PostsController(PostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDto>> GetRecentPosts()
        {
            return await _postsService.GetRecentPosts();
        }
    }
}
