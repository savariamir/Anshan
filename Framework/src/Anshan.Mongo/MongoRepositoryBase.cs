using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anshan.Collections.Pagination;
using Anshan.Domain;
using Anshan.Mongo.Abstractions;
using Anshan.Mongo.Extensions;
using Anshan.Mongo.Internal.Handlers;
using ChainRunner;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Anshan.Mongo
{
    public abstract class MongoRepositoryBase<TAggregateRoot> : IRepository<TAggregateRoot, string>
        where TAggregateRoot : AggregateRoot<string>
    {
        protected readonly IMongoCollection<TAggregateRoot> Collection;
        protected readonly IMongoConnection MongoConnection;

        protected MongoRepositoryBase(IMongoConnection mongoConnection, string collectionName = default)
        {
            MongoConnection = mongoConnection;
            Collection = mongoConnection.GetCollection<TAggregateRoot>(collectionName);
        }

        public virtual Task AddAsync(TAggregateRoot value)
        {
            var chain = new ChainBuilder<TAggregateRoot>()
                        .WithHandler(new AuditingHandler<TAggregateRoot>(true))
                        .WithHandler(new CreateHandler<TAggregateRoot>(Collection))
                        .Build();

            return chain.RunAsync(value);
        }

        public virtual Task AddAsync(IEnumerable<TAggregateRoot> values)
        {
            var chain = new ChainBuilder<IEnumerable<TAggregateRoot>>()
                        .WithHandler(new AuditingHandler<TAggregateRoot>(true))
                        .WithHandler(new CreateHandler<TAggregateRoot>(Collection))
                        .Build();

            return chain.RunAsync(values);
        }

        public virtual async Task<TAggregateRoot> GetByIdAsync(string id)
        {
            return await Collection.AsQueryable()
                                   .Where(a => a.Id == id && a.IsDeleted == false)
                                   .SingleOrDefaultAsync();
        }

        public async Task<TAggregateRoot> GetByIdAsync(string id, int version)
        {
            return await Collection.AsQueryable()
                                   .Where(a => a.Id == id && a.Version == version && a.IsDeleted == false)
                                   .SingleOrDefaultAsync();
        }

        public async Task<TDerivedTAggregateRoot> GetByIdAsync<TDerivedTAggregateRoot>(string id)
            where TDerivedTAggregateRoot : TAggregateRoot
        {
            return await Collection.OfType<TDerivedTAggregateRoot>()
                                   .AsQueryable()
                                   .Where(a => a.Id == id)
                                   .SingleOrDefaultAsync();
        }

        public async Task<TDerivedTAggregateRoot> GetByIdAsync<TDerivedTAggregateRoot>(string id, int version)
            where TDerivedTAggregateRoot : TAggregateRoot
        {
            return await Collection.OfType<TDerivedTAggregateRoot>()
                                   .AsQueryable()
                                   .Where(a => a.Id == id && a.Version == version)
                                   .SingleOrDefaultAsync();
        }

        public virtual Task DeleteAsync(TAggregateRoot value)
        {
            var chain = new ChainBuilder<TAggregateRoot>()
                        .WithHandler(new AuditingHandler<TAggregateRoot>())
                        .WithHandler(new DeleteHandler<TAggregateRoot>(Collection))
                        .Build();

            return chain.RunAsync(value);
        }

        public Task DeleteAsync(TAggregateRoot value, int version)
        {
            var chain = new ChainBuilder<TAggregateRoot>()
                        .WithHandler(new AuditingHandler<TAggregateRoot>())
                        .WithHandler(new DeleteHandler<TAggregateRoot>(Collection, version))
                        .Build();

            return chain.RunAsync(value);
        }

        public virtual Task DeleteAsync(IEnumerable<TAggregateRoot> values)
        {
            var chain = new ChainBuilder<IEnumerable<TAggregateRoot>>()
                        .WithHandler(new AuditingHandler<TAggregateRoot>())
                        .WithHandler(new DeleteBulkHandler<TAggregateRoot>(Collection))
                        .Build();

            return chain.RunAsync(values);
        }

        public Task DeleteHardAsync(TAggregateRoot value)
        {
            return Collection.DeleteOneAsync(c => c.Id == value.Id);
        }

        public Task DeleteHardAsync(TAggregateRoot value, int version)
        {
            return Collection.DeleteOneAsync(c => c.Id == value.Id && c.Version == version);
        }

        public Task DeleteHardAsync(string id)
        {
            return Collection.DeleteOneAsync(c => c.Id == id);
        }

        public Task DeleteHardAsync(string id, int version)
        {
            return Collection.DeleteOneAsync(c => c.Id == id && c.Version == version);
        }

        public Task UpdateAsync(TAggregateRoot value)
        {
            var chain = new ChainBuilder<TAggregateRoot>()
                        .WithHandler(new AuditingHandler<TAggregateRoot>())
                        .WithHandler(new VersioningHandler<TAggregateRoot>())
                        .WithHandler(new UpdateHandler<TAggregateRoot>(Collection, false))
                        .Build();

            return chain.RunAsync(value);
        }

        public virtual Task UpdateAsync(TAggregateRoot value, int version)
        {
            var chain = new ChainBuilder<TAggregateRoot>()
                        .WithHandler(new AuditingHandler<TAggregateRoot>())
                        .WithHandler(new VersioningHandler<TAggregateRoot>())
                        .WithHandler(new UpdateHandler<TAggregateRoot>(Collection, true))
                        .Build();

            return chain.RunAsync(value);
        }

        public virtual Task<string> GetNextIdAsync()
        {
            var objectId = ObjectId.GenerateNewId().ToString();

            return Task.FromResult(objectId);
        }

        public async IAsyncEnumerable<TDerivedTAggregateRoot> StreamAsync<TDerivedTAggregateRoot>()
            where TDerivedTAggregateRoot : TAggregateRoot
        {
            var asyncCursor = await Collection.OfType<TDerivedTAggregateRoot>()
                                              .AsQueryable()
                                              .Where(aggregateRoot => aggregateRoot.IsDeleted == false)
                                              .ToCursorAsync();

            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    yield return current;
                }
            }
        }

        public virtual async IAsyncEnumerable<TAggregateRoot> StreamAsync()
        {
            var asyncCursor = await Collection.AsQueryable()
                                              .Where(aggregateRoot => aggregateRoot.IsDeleted == false)
                                              .ToCursorAsync();

            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    yield return current;
                }
            }
        }

        public virtual async IAsyncEnumerable<TAggregateRoot> StreamAsync(int offset, int size)
        {
            var asyncCursor = await Collection
                                    .AsQueryable()
                                    .Where(aggregateRoot => aggregateRoot.IsDeleted == false)
                                    .Skip((offset - 1) * size)
                                    .Take(size)
                                    .ToCursorAsync();

            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    yield return current;
                }
            }
        }

        public async Task<IReadOnlyPaginatedCollection<TAggregateRoot>> PaginateAsync(int pageNumber = 1,
            int pageSize = 10)
        {
            var filter = Builders<TAggregateRoot>.Filter.Empty;
            return await ReadOnlyPaginatedCollection(pageNumber, pageSize, filter);
        }

        private async Task<IReadOnlyPaginatedCollection<TAggregateRoot>> ReadOnlyPaginatedCollection(int pageNumber,
            int pageSize,
            FilterDefinition<TAggregateRoot> filter)
        {
            
            var isDeletedFilter = Builders<TAggregateRoot>.Filter.Eq(d => d.IsDeleted, false);

            filter &= isDeletedFilter; 
            
            var countFacet = AggregateFacet
                .Create("count",
                        PipelineDefinition<TAggregateRoot, AggregateCountResult>.Create(new[]
                        {
                            PipelineStageDefinitionBuilder.Count<TAggregateRoot>()
                        }));

            var dataFacet = AggregateFacet
                .Create("data",
                        PipelineDefinition<TAggregateRoot, TAggregateRoot>.Create(new[]
                        {
                            PipelineStageDefinitionBuilder.Skip<TAggregateRoot>((pageNumber - 1) * pageSize),
                            PipelineStageDefinitionBuilder.Limit<TAggregateRoot>(pageSize)
                        }));

            var aggregation = await Collection
                                    .Aggregate()
                                    .SortByDescending(e => e.CreatedAt)
                                    .Match(filter)
                                    .Facet(countFacet, dataFacet)
                                    .ToListAsync();

            var data = aggregation.First()
                                  .Facets
                                  .First(x => x.Name == "data")
                                  .Output<TAggregateRoot>();

            var count = aggregation.First()
                                   .Facets.First(x => x.Name == "count")
                                   .Output<AggregateCountResult>()
                                   ?.FirstOrDefault()
                                   ?.Count ?? 0;

            return new ReadOnlyPaginatedCollection<TAggregateRoot>(data, count, pageNumber, pageSize);
        }

        public async Task<IReadOnlyPaginatedCollection<TAggregateRoot>> PaginateAsync(int pageNumber,
            int pageSize,
            FilterDefinition<TAggregateRoot> filter)
        {
            return await ReadOnlyPaginatedCollection(pageNumber, pageSize, filter);
        }

        public async Task<bool> ExistsAsync(string id)
        {
            id.EnsureIdIsValid();

            var count = await Collection.CountDocumentsAsync(c => c.Id == id, new CountOptions
            {
                Limit = 1
            });

            return count == 1;
        }

        public async Task<bool> ExistsAsync(string id, int version)
        {
            id.EnsureIdIsValid();

            var count = await Collection.CountDocumentsAsync(c => c.Id == id && c.Version == version, new CountOptions
            {
                Limit = 1
            });

            return count == 1;
        }
    }
}