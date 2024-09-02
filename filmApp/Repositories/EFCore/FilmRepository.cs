using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore
{
    public class FilmRepository : RepositoryBase<Film>, IFilmRepository
    {
        public FilmRepository(RepositoryContext context) : base(context) 
        { 

        }

        public void CreateFilm(Film film) => Create(film);
        public void DeleteFilm(Film film) => Delete(film);
        public async Task<PagedList<Film>> GetAllFilmsAsync(FilmParameters filmParameters, bool trackChanges)
        {
            var films = await FindAll(trackChanges)
                .Search(filmParameters.SearchTerm)
                .Sort(filmParameters.OrderBy)
                .ToListAsync();

            return PagedList<Film>.ToPagedList(films, filmParameters.PageNumber, filmParameters.PageSize);
        }  
        public async Task<Film> GetFilmByIdAsync(int id, bool trackChanges) => 
            await FindByCondition(x => x.FilmId.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void UpdateFilm(Film film) => Update(film);
        public async Task<string> GetFilmTitleByIdAsync(int filmId, bool trackChanges)
        {
            return await FindByCondition(f => f.FilmId == filmId, trackChanges)
                        .Select(f => f.FilmTitle)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Film>> GetFilmsByDirectorAsync(string directorName, bool trackChanges)
        {
            return await FindByCondition(f => f.DirectorName.ToLower().Contains(directorName.ToLower()), trackChanges)
                .ToListAsync();
        }

    }
}
