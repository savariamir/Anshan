using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anshan.Collections.Pagination;
using Anshan.Domain;
using Anshan.Mongo.Abstractions;
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
                        .WithHandler(new AuditingDelegate<TAggregateRoot>(true))
                        .WithHandler(new CreateHandler<TAggregateRoot>(Collection))
                        .Build();

            return chain.RunAsync(value);
        }

        public virtual Task AddAsync(IEnumerable<TAggregateRoot> values)
        {
            var chain = new ChainBuilder<IEnumerable<TAggregateRoot>>()
                        .WithHandler(new AuditingDelegate<TAggregateRoot>(true))
                        .WithHandler(new CreateHandler<TAggregateRoot>(Collection))
                        .Build();

            return chain.RunAsync(values);
        }

        public virtual Task<TAggregateRoot> GetByIdAsync(string id)
        {
            var idFilter = Builders<TAggregateRoot>.Filter.Eq(d => d.Id, id);
            var isDeletedFilter = Builders<TAggregateRoot>.Filter.Eq(d => d.IsDeleted, false);

            var filter = Builders<TAggregateRoot>.Filter.And(idFilter, isDeletedFilter);

            return Collection
                   .Find(filter)
                   .FirstOrDefaultAsync();
        }

        public virtual Task DeleteAsync(TAggregateRoot value)
        {
            var chain = new ChainBuilder<TAggregateRoot>()
                        .WithHandler(new AuditingDelegate<TAggregateRoot>())
                        .WithHandler(new DeleteHandler<TAggregateRoot>(Collection))
                        .Build();

            return chain.RunAsync(value);
        }

        public Task DeleteHardAsync(TAggregateRoot value)
        {
            return Collection.DeleteOneAsync(c => c.Id == value.Id);
        }
        
        public Task DeleteHardAsync(string id)
        {
            return Collection.DeleteOneAsync(c => c.Id == id);
        }

        public virtual Task UpdateAsync(TAggregateRoot value)
        {
            var chain = new ChainBuilder<TAggregateRoot>()
                        .WithHandler(new AuditingDelegate<TAggregateRoot>())
                        .WithHandler(new UpdateHandler<TAggregateRoot>(Collection))
                        .Build();

            return chain.RunAsync(value);
        }

        public virtual Task<string> GetNextIdAsync()
        {
            var objectId = ObjectId.GenerateNewId().ToString();

            return Task.FromResult(objectId);
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

        public async Task<IReadOnlyPaginatedCollection<TAggregateRoot>> PaginateAsync(
            int pageNumber = 1, int pageSize = 10)
        {
            
            
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

            var filter = Builders<TAggregateRoot>.Filter.Empty;

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
    }
}