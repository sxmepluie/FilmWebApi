using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IFilmRepository> _filmRepository;
        private readonly Lazy<IReviewRepository> _reviewRepository;
        private readonly Lazy<IWatchedFilmRepository> _watchedFilmRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IWatchListRepository> _watchListRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _filmRepository = new Lazy<IFilmRepository>(() => new FilmRepository(_context));
            _reviewRepository = new Lazy<IReviewRepository>(() => new ReviewRepository(_context));
            _watchedFilmRepository = new Lazy<IWatchedFilmRepository>(() => new WatchedFilmRepository(_context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _watchListRepository = new Lazy<IWatchListRepository>(() => new WatchListRepository(_context));
        }

        public IFilmRepository Film => _filmRepository.Value;
        public IReviewRepository Review => _reviewRepository.Value;
        public IWatchedFilmRepository WatchedFilm => _watchedFilmRepository.Value;
        public IUserRepository User => _userRepository.Value;
        public IWatchListRepository WatchList => _watchListRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
