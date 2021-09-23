using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Anshan.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Anshan.Mongo.Query
{
    internal enum Operator
    {
        And,
        Or
    }

    public class MongoFilterBuilder
    {
        private MongoFilterBuilder()
        {
            
        }

        public static MongoFilterBuilder<TAggregateRoot> Filter<TAggregateRoot>() where TAggregateRoot : AggregateRoot<string>
        {
            return new MongoFilterBuilder<TAggregateRoot>();
        } 
    }  
    
    public sealed class MongoFilterBuilder<TAggregateRoot> where TAggregateRoot : AggregateRoot<string>
    {
        private readonly FilterDefinitionBuilder<TAggregateRoot> _filterDefinitionBuilder;
        private FilterDefinition<TAggregateRoot> _filterDefinition = default;
        private Operator _nextOperator;

        public MongoFilterBuilder()
        {
            _filterDefinitionBuilder = Builders<TAggregateRoot>.Filter;
        }

        internal void SetNextOperator(Operator nextOperator)
        {
            _nextOperator = nextOperator;
        }

        public OperatorSetter<TAggregateRoot> IsDeleted(bool include = true)
        {
            return ApplyFilter(builder => builder.Eq(m => m.IsDeleted, include));
        }

        public OperatorSetter<TAggregateRoot> SearchOn(Expression<Func<TAggregateRoot, object>> field,
                                                       string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return NoFilter();
            }

            return ApplyFilter(builder =>
            {
                var searchRegexExpression = new BsonRegularExpression(Regex.Escape(search), "i");

                return builder.Regex(field, searchRegexExpression);
            });
        }

        private OperatorSetter<TAggregateRoot> ApplyFilter(
            Func<FilterDefinitionBuilder<TAggregateRoot>, FilterDefinition<TAggregateRoot>> filterFunc)
        {
            var filterDefinition = filterFunc(_filterDefinitionBuilder);

            // First Filter
            if (_filterDefinition is null)
            {
                _filterDefinition = filterDefinition;
                return new OperatorSetter<TAggregateRoot>(this);
            }
            
            if (_nextOperator == Operator.And)
            {
                _filterDefinitionBuilder.And(_filterDefinition, filterDefinition);
            }
            else
            {
                _filterDefinitionBuilder.Or(_filterDefinition, filterDefinition);
            }

            return new OperatorSetter<TAggregateRoot>(this);
        }

        private OperatorSetter<TAggregateRoot> NoFilter()
        {
            return new OperatorSetter<TAggregateRoot>(this);
        }

        public FilterDefinition<TAggregateRoot> Build()
        {
            return _filterDefinition ?? FilterDefinition<TAggregateRoot>.Empty;
        }
    }

    public abstract class MongoFilterBuilder<TAggregateRoot, TDerivedBuilder>
        where TAggregateRoot : AggregateRoot<string>
        where TDerivedBuilder : MongoFilterBuilder<TAggregateRoot, TDerivedBuilder>
    {
        private readonly FilterDefinitionBuilder<TAggregateRoot> _filterDefinitionBuilder;
        private FilterDefinition<TAggregateRoot> _filterDefinition;
        private Operator _nextOperator;

        protected MongoFilterBuilder()
        {
            _filterDefinitionBuilder = Builders<TAggregateRoot>.Filter;
            _filterDefinition = _filterDefinitionBuilder.Empty;
        }

        internal void SetNextOperator(Operator nextOperator)
        {
            _nextOperator = nextOperator;
        }

        public OperatorSetter<TAggregateRoot, TDerivedBuilder> IsDeleted(bool include = true)
        {
            return ApplyFilter(builder => builder.Eq(m => m.IsDeleted, include));
        }

        public OperatorSetter<TAggregateRoot, TDerivedBuilder> SearchOn(Expression<Func<TAggregateRoot, object>> field,
                                                                        string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return NoFilter();
            }

            return ApplyFilter(builder =>
            {
                var searchRegexExpression = new BsonRegularExpression(Regex.Escape(search), "i");

                return builder.Regex(field, searchRegexExpression);
            });
        }

        protected OperatorSetter<TAggregateRoot, TDerivedBuilder> ApplyFilter(
            Func<FilterDefinitionBuilder<TAggregateRoot>, FilterDefinition<TAggregateRoot>> filterFunc)
        {
            var filterDefinition = filterFunc(_filterDefinitionBuilder);

            if (_filterDefinition == FilterDefinition<TAggregateRoot>.Empty)
            {
                _filterDefinition = filterDefinition;
                return new OperatorSetter<TAggregateRoot, TDerivedBuilder>(this as TDerivedBuilder);
            }

            if (_nextOperator == Operator.And)
            {
                _filterDefinitionBuilder.And(_filterDefinition, filterDefinition);
            }
            else
            {
                _filterDefinitionBuilder.Or(_filterDefinition, filterDefinition);
            }

            return new OperatorSetter<TAggregateRoot, TDerivedBuilder>(this as TDerivedBuilder);
        }

        protected OperatorSetter<TAggregateRoot, TDerivedBuilder> NoFilter()
        {
            return new OperatorSetter<TAggregateRoot, TDerivedBuilder>(this as TDerivedBuilder);
        }

        public FilterDefinition<TAggregateRoot> Build()
        {
            return _filterDefinition;
        }
    }
}