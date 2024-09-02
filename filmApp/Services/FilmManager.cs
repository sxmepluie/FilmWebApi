using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;


namespace Services
{
    public class FilmManager : IFilmService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<FilmDto> _shaper;
        public FilmManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<FilmDto> shaper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _shaper = shaper;
        }

        public async Task<FilmDto> CreateFilmAsync(FilmDtoForInsertion filmDto)
        {
            var entity = _mapper.Map<Film>(filmDto);
            _manager.Film.CreateFilm(entity);
            await _manager.SaveAsync();
            return _mapper.Map<FilmDto>(entity);
        }

        public async Task DeleteFilmAsync(int id, bool trackChanges)
        {
            var entity = await GetFilmByIdAndCheckExists(id, trackChanges);
            _manager.Film.DeleteFilm(entity);
            await _manager.SaveAsync();

        }
        
        public async Task<(IEnumerable<ExpandoObject> films, MetaData metaData)> GetAllFilmsAsync(FilmParameters filmParameters, bool trackChanges)
        {
            var filmsWithMetaData = await _manager.Film.GetAllFilmsAsync(filmParameters, trackChanges);
            var filmsDto = _mapper.Map<IEnumerable<FilmDto>>(filmsWithMetaData);
            var shapedData = _shaper.ShapeData(filmsDto, filmParameters.Fields);
           return (films : shapedData, metaData : filmsWithMetaData.MetaData);
        }

        public async Task<FilmDto> GetFilmByIdAsync(int id, bool trackChanges)
        { 
            var entity = await GetFilmByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<FilmDto>(entity);
        }

        public async Task<(FilmDtoForUpdate filmDtoForUpdate, Film film)> GetFilmForPatchAsync(int id, bool trackChanges)
        {
            var film = await GetFilmByIdAndCheckExists(id, trackChanges);

            var filmDtoForUpdate = _mapper.Map<FilmDtoForUpdate>(film);
            return (filmDtoForUpdate,film);
        }

        public async Task SaveChangesForPatchAsync(FilmDtoForUpdate filmDtoForUpdate, Film film)
        {
            _mapper.Map(filmDtoForUpdate,film);
            await _manager.SaveAsync();
        }

        public async Task UpdateFilmAsync(int id, FilmDtoForUpdate filmDto, bool trackChanges)
        {
            var entity = await GetFilmByIdAndCheckExists(id, trackChanges);

            if (entity == null)
            {
                throw new ArgumentNullException($"Film with ID not found.");
            }

            filmDto.FilmId = id;
            entity = _mapper.Map<Film>(filmDto);
            _manager.Film.UpdateFilm(entity);
            await _manager.SaveAsync();
            
        }

        public async Task<Film> GetFilmByIdAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.Film.GetFilmByIdAsync(id, trackChanges);
            if (entity == null)
                throw new FilmNotFoundException(id);
            return entity;
        }

        public async Task<DirectorDto> GetDirectorByNameAsync(string name)
        {
            var films = await _manager.Film.GetFilmsByDirectorAsync(name, false);

            var directorName = name;
            var directorDto = new DirectorDto
            {
                DirectorId = films.First().DirectorId,
                Name = directorName,
                Films = _mapper.Map<IEnumerable<FilmDto>>(films)
            };
            return directorDto;
        }
    }
}
