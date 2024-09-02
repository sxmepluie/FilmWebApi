using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateReview(Review review) => Create(review);

        public async Task<IEnumerable<Review>> GetAllReviewsByFilmIdAsync(int filmId, bool trackChanges)
        {
            return await FindByCondition(r => r.FilmId == filmId, trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByUserIdAsync(Guid userId, bool trackChanges)
        {
            return await FindByCondition(r => r.UserId == userId, trackChanges).ToListAsync();
        }
    }
}
