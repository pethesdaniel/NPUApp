using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPUApp.BLL.Model.DTOs;
using NPUApp.BLL.Model.RequestDTOs;
using NPUApp.BLL.Services;
using NPUApp.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace NPUApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService { get; set; }

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var result = await _userService.Register(request.Username, request.Email, request.Password);
            if (result != null && result.Succeeded)
            {
                request.Password = "";
                return CreatedAtAction(nameof(Register), new { email = request.Email }, request);
            }
            if (result != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponseDto>> Authenticate([FromBody] LoginDto request)
        {
            try
            {
                var accessToken = await _userService.Authorize(request.Email, request.Password);
                return Ok(new LoginResponseDto
                {
                    Email = request.Email,
                    Token = accessToken,
                });
            }
            catch (ArgumentException)
            {
                return BadRequest("Bad credentials");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("verify")]
        public IActionResult Verify()
        {
            return Ok();
        }
    }
}
