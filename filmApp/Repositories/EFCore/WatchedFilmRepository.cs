using Entities.Models;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EFCore
{
    public class WatchedFilmRepository : RepositoryBase<WatchedFilm>, IWatchedFilmRepository
    {
        private readonly RepositoryContext _context;

        public WatchedFilmRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }

        public void CreateWatchedFilm(WatchedFilm watchedFilm) => Create(watchedFilm);
        public void DeleteWatchedFilm(WatchedFilm watchedFilm) => Delete(watchedFilm);

        public async Task<IEnumerable<Film>> GetWatchedFilmsAsync(Guid userId) =>
        await (from wf in _context.WatchedFilms
               join f in _context.Films on wf.FilmId equals f.FilmId
               where wf.UserId == userId
               select f).ToListAsync();
    }
}
