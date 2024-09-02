using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFilmService> _filmService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IReviewService> _reviewService;
        private readonly Lazy<IHttpContextAccessor> _httpContextAccessor;
        private readonly Lazy<IWatchedFilmService> _watchedFilmService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IWatchListService> _watchListService;


        public ServiceManager(IRepositoryManager repositoryManager, IHttpContextAccessor httpContextAccessor, ILoggerService logger, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration, IDataShaper<FilmDto> shaper)
        {
            _filmService = new Lazy<IFilmService>(() => new FilmManager(repositoryManager, logger, mapper, shaper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(logger, mapper, userManager, configuration));
            _reviewService = new Lazy<IReviewService>(() => new ReviewManager(repositoryManager, mapper, httpContextAccessor));
            _watchedFilmService = new Lazy<IWatchedFilmService>(() => new WatchedFilmManager(repositoryManager, httpContextAccessor));
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager, mapper, userManager, roleManager, httpContextAccessor));
            _watchListService = new Lazy<IWatchListService>(() => new WatchListManager(repositoryManager, mapper )); 

        }
        
        public IFilmService FilmService => _filmService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IReviewService ReviewService => _reviewService.Value;
        public IWatchedFilmService WatchedService => _watchedFilmService.Value;
        public IUserService UserService => _userService.Value;
        public IWatchListService WatchListService => _watchListService.Value; 

    }
}
