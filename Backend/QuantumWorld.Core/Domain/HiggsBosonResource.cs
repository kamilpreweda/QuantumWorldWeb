
namespace QuantumWorld.Core.Domain
{
    public class HiggsBosonResource : Resource
    {
        protected override float BaseValue => 100000;

        public HiggsBosonResource() : base()
        {

        }
        public HiggsBosonResource(float value) : base(value)
        {

        }
    }
}