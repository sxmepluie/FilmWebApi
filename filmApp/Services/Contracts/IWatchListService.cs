using Entities.Models;

public interface IWatchListService
{
    Task AddFilmForWatchListAsync(Guid userId, int filmId);
    Task DeleteFilmForWatchListAsync(Guid userId, int filmId);
    Task<IEnumerable<Film>> GetWatchListAsync(Guid userId);
}
