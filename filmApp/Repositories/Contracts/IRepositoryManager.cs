namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IFilmRepository Film { get; }
        IReviewRepository Review { get; }
        IWatchedFilmRepository WatchedFilm { get; }
        IUserRepository User { get; }
        IWatchListRepository WatchList { get; }
        public Task SaveAsync();
    }
}
