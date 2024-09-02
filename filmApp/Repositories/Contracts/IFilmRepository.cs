using Entities.Models;
using Entities.RequestFeatures;


namespace Repositories.Contracts
{
    public interface IFilmRepository : IRepositoryBase<Film>
    {
        Task<PagedList<Film>> GetAllFilmsAsync(FilmParameters filmParameters, bool trackChanges);
        Task<Film> GetFilmByIdAsync(int id, bool trackChanges);
        void CreateFilm(Film film);
        void UpdateFilm(Film film);
        void DeleteFilm(Film film);
        Task<string> GetFilmTitleByIdAsync(int filmId, bool trackChanges);
        Task<IEnumerable<Film>> GetFilmsByDirectorAsync(string directorName, bool trackChanges);
    }

}
