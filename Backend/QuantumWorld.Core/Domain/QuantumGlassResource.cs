namespace QuantumWorld.Core.Domain
{
    public class QuantumGlassResource : Resource
    {
        protected override float BaseValue => 500;

        public QuantumGlassResource() : base()
        {

        }
        public QuantumGlassResource(float value) : base(value)
        {

        }
    }
}