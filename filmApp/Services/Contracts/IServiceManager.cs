namespace Services.Contracts
{
    public interface IServiceManager
    {
        IFilmService FilmService { get; }

        IWatchListService WatchListService { get; }

        IAuthenticationService AuthenticationService { get; }

        IReviewService ReviewService { get; }

        IWatchedFilmService WatchedService { get; }

        IUserService UserService { get; }
    }
}
