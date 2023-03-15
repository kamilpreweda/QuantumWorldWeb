
namespace QuantumWorld.Core.Domain
{
    public class HiggsBosonResource : Resource
    {
        protected override float BaseValue => 10000;

        protected override float BaseIncome => 13;

        public HiggsBosonResource() : base()
        {

        }
        public HiggsBosonResource(float value) : base(value)
        {

        }
    }
}