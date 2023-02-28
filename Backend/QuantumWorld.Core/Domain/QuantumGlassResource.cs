

namespace QuantumWorld.Core.Domain
{
    public class QuantumGlassResource : Resource
    {
        
        protected override float BaseValue => 100000;

        protected override float BaseIncome => 10;

        public QuantumGlassResource() : base()
        {

        }
        public QuantumGlassResource(float value) : base(value)
        {

        }
    }
}