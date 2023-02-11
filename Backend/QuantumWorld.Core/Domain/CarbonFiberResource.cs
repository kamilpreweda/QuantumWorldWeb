

namespace QuantumWorld.Core.Domain
{
    public class CarbonFiberResource : Resource
    {              
        protected override float BaseValue => 100000;

        public CarbonFiberResource() : base()
        {

        }
        public CarbonFiberResource(float value) : base(value)
        {

        }
    }
}