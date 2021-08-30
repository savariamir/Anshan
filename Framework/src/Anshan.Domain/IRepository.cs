using System.Collections.Generic;
using System.Threading.Tasks;
using Anshan.Collections.Pagination;

namespace Anshan.Domain
{
    /// <summary>
    ///     The base of all repositories
    /// </summary>
    public interface IRepository
    {
    }

    public interface IRepository<TAggregateRoot, TKey> : IRepository where TAggregateRoot : IAggregate
    {
        Task AddAsync(TAggregateRoot value);
        
        Task AddAsync(IEnumerable<TAggregateRoot> value);

        Task<TAggregateRoot> GetByIdAsync(TKey id);

        Task DeleteAsync(TAggregateRoot value);
        
        Task DeleteHardAsync(TAggregateRoot value);
        Task DeleteHardAsync(TKey id);

        Task UpdateAsync(TAggregateRoot value);

        Task<TKey> GetNextIdAsync();

        IAsyncEnumerable<TAggregateRoot> StreamAsync();

        IAsyncEnumerable<TAggregateRoot> StreamAsync(int offset, int size);

        Task<IReadOnlyPaginatedCollection<TAggregateRoot>> PaginateAsync(int pageNumber = 1, int pageSize = 10);
    }
}