using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Repositories.Contracts;
using Services.Contracts;
using System.Security.Claims;

namespace Services
{
    public class ReviewManager : IReviewService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewManager(IRepositoryManager manager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ReviewDto> CreateReviewAsync(int filmId, ReviewDtoForInsertion reviewDtoForInsertion)
        {
            var entity = _mapper.Map<Review>(reviewDtoForInsertion);

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(userId, out Guid id);

            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

            if (userId == null || userName == null)
            {
                throw new ArgumentNullException();
            }

            var film = await _manager.Film.GetFilmByIdAsync(filmId, false);
            if (film == null)
            {
                throw new Exception("Film not found.");
            }

            var existingReviews = await _manager.Review.GetAllReviewsByFilmIdAsync(filmId, false);
            int reviewCount = existingReviews.Count();
            double AverageRate;
            if (reviewCount == 0)
            {
                AverageRate = reviewDtoForInsertion.Rate;
            }
            else
            {
                AverageRate = (film.Rate + reviewDtoForInsertion.Rate)/2;
            }
            film.Rate = AverageRate;
            entity.UserId = id;
            entity.Username = userName;
            entity.FilmId = filmId;
            entity.Time = DateTime.Now;

            _manager.Review.CreateReview(entity);
            await _manager.SaveAsync();

            _manager.Film.UpdateFilm(film);
            await _manager.SaveAsync();

            var reviewDto = _mapper.Map<ReviewDto>(entity);
            reviewDto.UserName = userName;
            reviewDto.FilmTitle = film.FilmTitle; 

            return reviewDto;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByFilmIdAsync(int id, bool trackChanges)
        {
            var reviews = await _manager.Review.GetAllReviewsByFilmIdAsync(id, trackChanges);

            var film = await _manager.Film.GetFilmByIdAsync(id, false);
            if (film == null)
            {
                throw new Exception("Film not found.");
            }

            var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            foreach (var reviewDto in reviewDtos)
            {
                reviewDto.FilmTitle = film.FilmTitle; 
            }

            return reviewDtos;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(Guid userId, bool trackChanges)
        {
            var reviews = await _manager.Review.GetAllReviewsByUserIdAsync(userId, trackChanges);
            var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

            foreach (var reviewDto in reviewDtos)
            {
                var film = await _manager.Film.GetFilmByIdAsync(reviewDto.FilmId, false);
                if (film != null)
                {
                    reviewDto.FilmTitle = film.FilmTitle;
                }

                var user = await _manager.User.GetUserById(reviewDto.UserId, false);
                if (user != null)   
                {
                    reviewDto.UserName = user.UserName;
                }
            }


            return reviewDtos;
        }
    }
}
