using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System.Dynamic;


namespace Services.Contracts
{
    public interface IFilmService
    {
        Task<(IEnumerable<ExpandoObject> films, MetaData metaData)> GetAllFilmsAsync(FilmParameters filmParameters, bool trackChanges);
        Task<FilmDto> GetFilmByIdAsync(int id, bool trackChanges);
        Task<FilmDto> CreateFilmAsync(FilmDtoForInsertion film);
        Task UpdateFilmAsync(int id, FilmDtoForUpdate filmDto, bool trackChanges);
        Task DeleteFilmAsync(int id, bool trackChanges);
        Task<(FilmDtoForUpdate filmDtoForUpdate, Film film)> GetFilmForPatchAsync(int id, bool trackChanges);
        Task SaveChangesForPatchAsync(FilmDtoForUpdate filmDtoForUpdate, Film film);
        Task <DirectorDto> GetDirectorByNameAsync(string name);
    }
}
