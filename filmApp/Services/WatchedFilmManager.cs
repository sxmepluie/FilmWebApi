using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class WatchedFilmManager : IWatchedFilmService
    {
        private readonly IRepositoryManager _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WatchedFilmManager(IRepositoryManager manager, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddFilmToWatchedAsync(Guid userId, int filmId)
        {
            var existingWatchedFilm = await _manager.WatchedFilm.FindByCondition(
                wf => wf.UserId == userId && wf.FilmId == filmId,
                trackChanges: false
            ).SingleOrDefaultAsync();

            if (existingWatchedFilm != null)
            {
                throw new InvalidOperationException("Film has already watched.");
            }

            var watchedFilm = new WatchedFilm
            {
                UserId = userId,
                FilmId = filmId
            };

            _manager.WatchedFilm.CreateWatchedFilm(watchedFilm);
            await _manager.SaveAsync();
        }


        public async Task RemoveFilmFromWatchedAsync(Guid userId, int filmId)
        {
            var watchedFilm = await _manager.WatchedFilm.FindByCondition(
                wf => wf.UserId == userId && wf.FilmId == filmId,
                trackChanges: false
            ).SingleOrDefaultAsync();

            if (watchedFilm == null)
            {
                throw new InvalidOperationException("Film has not been watched.");
            }

            _manager.WatchedFilm.DeleteWatchedFilm(watchedFilm);
            await _manager.SaveAsync();
        }


        public async Task<IEnumerable<Film>> GetWatchedFilmsAsync(Guid userId)
        {
            return await _manager.WatchedFilm.GetWatchedFilmsAsync(userId);
        }
    }
}
