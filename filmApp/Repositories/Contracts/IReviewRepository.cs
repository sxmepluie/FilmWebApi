using Entities.Models;

namespace Repositories.Contracts
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<IEnumerable<Review>> GetAllReviewsByFilmIdAsync(int filmId, bool trackChanges);

        void CreateReview(Review review);

        Task<IEnumerable<Review>> GetAllReviewsByUserIdAsync(Guid userId, bool trackChanges);
    }
}
