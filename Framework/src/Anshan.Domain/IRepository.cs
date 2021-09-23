using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Anshan.Collections.Pagination;
using MongoDB.Driver;

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

        [Obsolete(message: "please use the 'GetByIdAsync' method with versioning support")]
        Task<TAggregateRoot> GetByIdAsync(TKey id);
        Task<TAggregateRoot> GetByIdAsync(TKey id, int version);

        [Obsolete(message: "please use the 'GetByIdAsync' method with versioning support")]
        Task<TDerivedTAggregateRoot> GetByIdAsync<TDerivedTAggregateRoot>(TKey id)
            where TDerivedTAggregateRoot : TAggregateRoot;
        
        Task<TDerivedTAggregateRoot> GetByIdAsync<TDerivedTAggregateRoot>(TKey id, int version)
            where TDerivedTAggregateRoot : TAggregateRoot;

        [Obsolete(message: "please use the 'DeleteAsync' method with versioning support")]
        Task DeleteAsync(TAggregateRoot value);
        Task DeleteAsync(TAggregateRoot value, int version);
        
        Task DeleteAsync(IEnumerable<TAggregateRoot> values);
        
        [Obsolete(message: "please use the 'DeleteHardAsync' method with versioning support")]
        Task DeleteHardAsync(TAggregateRoot value);
        Task DeleteHardAsync(TAggregateRoot value, int version);
        
        [Obsolete(message: "please use the 'DeleteHardAsync' method with versioning support")]
        Task DeleteHardAsync(TKey id);
        Task DeleteHardAsync(TKey id, int version);

        [Obsolete(message: "please use the 'UpdateAsync' method with versioning support")]
        Task UpdateAsync(TAggregateRoot value);
        Task UpdateAsync(TAggregateRoot value, int version);

        Task<TKey> GetNextIdAsync();

        IAsyncEnumerable<TDerivedTAggregateRoot> StreamAsync<TDerivedTAggregateRoot>()
            where TDerivedTAggregateRoot : TAggregateRoot;
        
        IAsyncEnumerable<TAggregateRoot> StreamAsync();

        IAsyncEnumerable<TAggregateRoot> StreamAsync(int offset, int size);

        Task<IReadOnlyPaginatedCollection<TAggregateRoot>> PaginateAsync(int pageNumber = 1, int pageSize = 10);
        Task<IReadOnlyPaginatedCollection<TAggregateRoot>> PaginateAsync(int pageNumber, int pageSize, FilterDefinition<TAggregateRoot> filter);

        [Obsolete(message: "please use the other 'ExistsAsync' method with versioning support")]
        Task<bool> ExistsAsync(TKey id);
        Task<bool> ExistsAsync(TKey id, int version);

    }
}