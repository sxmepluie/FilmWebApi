using Entities.Models;

namespace Repositories.Contracts
{
    public interface IWatchListRepository : IRepositoryBase<WatchList>
    {
        void AddFilmForWatchList(WatchList watchedList);
        void DeleteFilmForWatchList(WatchList watchedList);
        Task<IEnumerable<Film>> GetWatchListAsync(Guid userId);
    }
}
