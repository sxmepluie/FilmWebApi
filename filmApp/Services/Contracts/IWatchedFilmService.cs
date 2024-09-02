using Entities.Models;


namespace Services.Contracts
{
    public interface IWatchedFilmService
    {
        Task AddFilmToWatchedAsync(Guid userId, int filmId);
        Task RemoveFilmFromWatchedAsync(Guid userId, int filmId);
        Task<IEnumerable<Film>> GetWatchedFilmsAsync(Guid userId);
    }
}
