using AutoMapper;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

public class WatchListManager : IWatchListService
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;

    public WatchListManager(IRepositoryManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task AddFilmForWatchListAsync(Guid userId, int filmId)
    {
        var existingWatchList = await _manager.WatchList.FindByCondition(wl => wl.UserId == userId && wl.FilmId == filmId,false)
            .SingleOrDefaultAsync();

        if (existingWatchList != null)
        {
            throw new InvalidOperationException("Film is already in the watchlist.");
        }

        var watchList = new WatchList
        {
            UserId = userId,
            FilmId = filmId
        };

        _manager.WatchList.AddFilmForWatchList(watchList);
        await _manager.SaveAsync();
    }

    public async Task DeleteFilmForWatchListAsync(Guid userId, int filmId)
    {
        var watchList = await _manager.WatchList.FindByCondition(
            wl => wl.UserId == userId && wl.FilmId == filmId,
           false
        ).SingleOrDefaultAsync();

        if (watchList == null)
        {
            throw new InvalidOperationException("Film is not in the watchlist.");
        }

        if (watchList != null)
        {
            _manager.WatchList.DeleteFilmForWatchList(watchList);
            await _manager.SaveAsync();
        }
    }

    public async Task<IEnumerable<Film>> GetWatchListAsync(Guid userId)
    {
        return await _manager.WatchList.GetWatchListAsync(userId);
    }
}
