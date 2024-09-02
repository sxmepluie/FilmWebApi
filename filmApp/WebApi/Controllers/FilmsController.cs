using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Text.Json;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
    [Route("api/films")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public FilmsController(IServiceManager manager)
        {
            _manager = manager;
        }

        #region [Film Cruds]

        [HttpHead]
        [HttpGet]
        public async Task<IActionResult> GetAllFilmsAsync([FromQuery]FilmParameters filmParameters)
        {
            var pagedResult = await _manager.FilmService.GetAllFilmsAsync(filmParameters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.films);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFilmByIdAsync([FromRoute(Name = "id")] int id)
        {
            var film = await _manager
                .FilmService
                .GetFilmByIdAsync(id, false);

            if (film is null)
                throw new FilmNotFoundException(id);

            return Ok(film);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateOneFilmAsync([FromBody] FilmDtoForInsertion filmDto)
        {
            var film = await _manager.FilmService.CreateFilmAsync(filmDto);
            return StatusCode(201, film);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneFilmAsync([FromRoute(Name = "id")] int id,
            [FromBody] FilmDtoForUpdate filmDto)
        {
            await _manager.FilmService.UpdateFilmAsync(id, filmDto, false); 
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneFilmAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.FilmService.DeleteFilmAsync(id, false);
            return NoContent();
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneFilmAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<FilmDtoForUpdate> filmPatch)
        {
            if (filmPatch == null)
                return BadRequest();

            var result = await _manager.FilmService.GetFilmForPatchAsync(id, true);

            filmPatch.ApplyTo(result.filmDtoForUpdate, ModelState);

            TryValidateModel(result.filmDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.FilmService.SaveChangesForPatchAsync(result.filmDtoForUpdate, result.film);
            return NoContent();

        }

        [Authorize(Roles = "Admin")]
        [HttpOptions]
        public IActionResult GetFilmOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }

        #endregion

        #region [Reviews]

        [HttpGet("{id:int}/reviews")]
        public async Task<IActionResult> GetReviewsByFilmId(int id)
        {
            var reviews = await _manager.ReviewService.GetReviewsByFilmIdAsync(id, false);
            return Ok(reviews);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("{filmId:int}/reviews")]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromRoute] int filmId, [FromBody] ReviewDtoForInsertion reviewDto)
        {
            var film = await _manager.FilmService.GetFilmByIdAsync(filmId, false);
            if (film == null)
                throw new FilmNotFoundException(filmId);

            var review = await _manager.ReviewService.CreateReviewAsync(filmId, reviewDto);
            return StatusCode(201, review);
        }

        #endregion

    }
}

 