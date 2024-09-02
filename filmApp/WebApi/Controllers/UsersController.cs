using AutoMapper;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Security.Claims;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UsersController(IServiceManager service, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.UserService.GetAllUsersAsync(trackChanges: false);
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await _service.UserService.GetUserByIdAsync(userId, trackChanges: false);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDtoForRegistration userDto)
        {
            var result = await _service.UserService.RegisterUserAsync(userDto);

            return Ok(result);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize(Roles = "Admin")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserByAdmin([FromBody] UserDtoForUpdate userDto, Guid userId)
        {
            await _service.UserService.UpdateUserByAdminAsync(userId, userDto, true);
            return Ok();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDtoForUpdate userDto)
        {
            await _service.UserService.UpdateUserAsync(userDto, true);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var user = await _service.UserService.GetUserByIdAsync(userId, trackChanges: false);
            if (user == null)
            {
                return NotFound();
            }

            await _service.UserService.DeleteUserAsync(userId, trackChanges: false);
            return NoContent();
        }

        [Authorize]
        [HttpPost("watched/{filmId}")]
        public async Task<IActionResult> AddFilmToWatchedAsync([FromRoute] int filmId)
        {
            var id = GetUserId();
            await _service.WatchedService.AddFilmToWatchedAsync(id, filmId);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("watched/{filmId}")]
        public async Task<IActionResult> RemoveFilmFromWatchedAsync([FromRoute] int filmId)
        {
            var id = GetUserId();

            await _service.WatchedService.RemoveFilmFromWatchedAsync(id, filmId);
            return NoContent();
        }

        [Authorize]
        [HttpGet("watched")]
        public async Task<IActionResult> GetWatchedFilmsAsync()
        {
            var id = GetUserId();
            var films = await _service.WatchedService.GetWatchedFilmsAsync(id);
            return Ok(films);
        }

        [Authorize]
        [HttpPost("watchlist/{filmId}")]
        public async Task<IActionResult> AddToWatchListAsync([FromRoute] int filmId)
        {
            var id = GetUserId();
            await _service.WatchListService.AddFilmForWatchListAsync(id, filmId);
            return Ok();
        }


        [Authorize]
        [HttpDelete("watchlist/{filmId}")]
        public async Task<IActionResult> RemoveFromWatchListAsync([FromRoute] int filmId)
        {
            var id = GetUserId();
            await _service.WatchListService.DeleteFilmForWatchListAsync(id, filmId);
            return Ok();
        }

        [Authorize]
        [HttpGet("watchlist")]
        public async Task<IActionResult> GetWatchListAsync()
        {
            var id = GetUserId();
            var watchList = await _service.WatchListService.GetWatchListAsync(id);
            return Ok(watchList);
        }

        [Authorize]
        [HttpGet("{userId}/reviews")]
        public async Task<IActionResult> GetReviewsByUserId(Guid userId)
        {
            var reviews = await _service.ReviewService.GetReviewsByUserIdAsync(userId, false);
            return Ok(reviews);
        }

        private Guid GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userId, out Guid id) ? id : Guid.Empty;
        }

    }
}
