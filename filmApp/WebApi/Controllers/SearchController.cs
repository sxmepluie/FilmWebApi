using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public SearchController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetDirectorByName(string name)
        {
            var director = await _manager.FilmService.GetDirectorByNameAsync(name);
            if (director == null)
            {
                return NotFound();
            }
            return Ok(director);
        }

    }
}
