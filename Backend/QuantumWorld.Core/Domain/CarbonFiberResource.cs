namespace QuantumWorld.Core.Domain
{
    public class CarbonFiberResource : Resource
    {
        protected override float BaseValue => 500;

        public CarbonFiberResource() : base()
        {

        }
        public CarbonFiberResource(float value) : base(value)
        {

        }
    }
}