

namespace QuantumWorld.Core.Domain
{
    public class CarbonFiberResource : Resource
    {              
        protected override float BaseValue => 100000;

        protected override float BaseIncome => 15;

        public CarbonFiberResource() : base()
        {

        }
        public CarbonFiberResource(float value) : base(value)
        {

        }
    }
}