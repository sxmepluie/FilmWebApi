using Entities.Models;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class FilmRepositoryExtensions
    {
        public static IQueryable<Film> Search(this  IQueryable<Film> films, string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return films;

            var lowCaseTerm = searchTerm.Trim().ToLower();
            return films
                .Where(x => x.FilmTitle
                .ToLower()
                .Contains(searchTerm));
        }
        public static IQueryable<Film> Sort(this IQueryable<Film> books,
                    string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(b => b.FilmId);

            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Film>(orderByQueryString);

            if (orderQuery is null)
                return books.OrderBy(b => b.FilmId);

            return books.OrderBy(orderQuery);
        }
    }
}
