

namespace QuantumWorld.Core.Domain
{
    public class QuantumGlassResource : Resource
    {
        
        protected override float BaseValue => 100000;

        public QuantumGlassResource() : base()
        {

        }
        public QuantumGlassResource(float value) : base(value)
        {

        }
    }
}