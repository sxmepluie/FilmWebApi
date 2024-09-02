using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface IReviewService
    {
        Task<ReviewDto> CreateReviewAsync(int filmId, ReviewDtoForInsertion reviewDtoForInsertion);

        Task<IEnumerable<ReviewDto>> GetReviewsByFilmIdAsync(int filmId, bool trackChanges);

        Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(Guid userId, bool trackChanges);
    }
}
