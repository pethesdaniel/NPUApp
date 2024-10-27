using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPUApp.BLL.Model.DTOs;
using NPUApp.BLL.Services;

namespace NPUApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private PartsService _partsService { get; set; }
        public PartsController(PartsService partsService)
        {
            _partsService = partsService;
        }

        [HttpGet]
        public async Task<IEnumerable<PartDto>> GetValidParts()
        {
            return await _partsService.GetValidParts();
        }
    }
}
