

using MongoDB.Bson.Serialization.Attributes;

namespace QuantumWorld.Core.Domain
{
    [BsonKnownTypes(typeof(CarbonFiberResource), typeof(QuantumGlassResource), typeof(HiggsBosonResource))]
    public abstract class Resource
    {

        public string Name { get; set; } = string.Empty;
        public ResourceType Type { get; set; }
        protected abstract float BaseValue { get; }
        public float Value { get; set; }

        public Resource()
        {
            AutoSetBasicAttributes();
        }
        public Resource(float value)
        {
            AutoSetBasicAttributes();
            SetValue(value);
        }
        private void SetName()
        {
            Name = this.GetType().Name;
        }
        private void SetType()
        {
            if (!Enum.IsDefined(typeof(ResourceType), Name))
            {
                throw new Exception("Resource type not found.");
            }
            Type = (ResourceType)Enum.Parse(typeof(ResourceType), Name);
        }
        private void SetValue(float value = -1)
        {
            Value = BaseValue;

            if (value > 0)
            {
                Value = (float)value;
            }

        }
        private void AutoSetBasicAttributes()
        {
            SetName();
            SetValue();
            SetType();
        }
    }
}