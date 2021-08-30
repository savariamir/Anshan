using Anshan.Domain;
using MongoDB.Driver;

namespace Anshan.Mongo.Query
{
    public class OperatorSetter<TAggregateRoot, TDerivedBuilder> where TAggregateRoot : AggregateRoot<string>
                                                                 where TDerivedBuilder : MongoFilterBuilder<TAggregateRoot, TDerivedBuilder>
    {
        private readonly TDerivedBuilder _builder;

        public OperatorSetter(TDerivedBuilder builder)
        {
            _builder = builder;
        }

        public TDerivedBuilder And
        {
            get
            {
                _builder.SetNextOperator(Operator.And);
                return _builder;
            }
        }

        public TDerivedBuilder Or
        {
            get
            {
                _builder.SetNextOperator(Operator.Or);
                return _builder;
            }
        }

        public FilterDefinition<TAggregateRoot> Build()
        {
            return _builder.Build();
        }
    }
}