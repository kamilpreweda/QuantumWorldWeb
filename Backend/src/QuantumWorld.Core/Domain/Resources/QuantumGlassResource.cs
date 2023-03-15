

namespace QuantumWorld.Core.Domain
{
    public class QuantumGlassResource : Resource
    {
        
        protected override float BaseValue => 10000;

        protected override float BaseIncome => 14;

        public QuantumGlassResource() : base()
        {

        }
        public QuantumGlassResource(float value) : base(value)
        {

        }
    }
}