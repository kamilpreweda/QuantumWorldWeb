using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace QuantumWorld.Infrastructure.Mongo
{
    public static class MongoConfiguration
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }
            _initialized = true;
        }

        private static void RegisterConventions()
        {
            ConventionRegistry.Register("QuantumWorldConventions", new MongoConvention(), x => true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };
        }
    }

}