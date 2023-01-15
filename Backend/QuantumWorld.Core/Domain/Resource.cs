namespace QuantumWorld.Core.Domain
{
    public class Resource
    {
        public ResourceType Type { get; set; }
        public float Value { get; set; }
        public float Cap { get; set; }

        public Resource(ResourceType type, float value, float cap)
        {
            SetResourceType(type);
            SetValue(value);
            SetCap(cap);
        }

        private void SetResourceType(ResourceType type)
        {
            Type = type;
        }

        private void SetValue(float value)
        {
            if (value > Cap)
            {
                Value = Cap;
            }
            Value = value;
        }

        private void SetCap(float cap)
        {
            Cap = cap;
        }
    }


}