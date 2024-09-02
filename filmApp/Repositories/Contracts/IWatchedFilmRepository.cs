using Entities.Models;

namespace Repositories.Contracts
{
    public interface IWatchedFilmRepository : IRepositoryBase<WatchedFilm>
    {
        void CreateWatchedFilm(WatchedFilm watchedFilm);
        void DeleteWatchedFilm(WatchedFilm watchedFilm);
        Task<IEnumerable<Film>> GetWatchedFilmsAsync(Guid userId);
    }
}
 