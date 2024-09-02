using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class WatchListRepository : RepositoryBase<WatchList>, IWatchListRepository
    {
        private readonly RepositoryContext _context;
        public WatchListRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public void AddFilmForWatchList(WatchList watchList) => Create(watchList);
        public void DeleteFilmForWatchList(WatchList watchList) => Delete(watchList);

        public async Task<IEnumerable<Film>> GetWatchListAsync(Guid userId) =>
        await (from wl in _context.WatchLists
               join f in _context.WLFilms on wl.FilmId equals f.FilmId
               where wl.UserId == userId
               select f).ToListAsync();
    }
}
