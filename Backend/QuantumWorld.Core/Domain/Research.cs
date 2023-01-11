namespace QuantumWorld.Core.Domain
{
    public class Research
    {
        public ResearchType Type { get; protected set; }
        public int Lvl { get; protected set; }
        public string Description { get; protected set; }
        public TimeSpan TimeToBuild { get; protected set; }
        public float CostMultipier { get; protected set; }
        public IEnumerable<Resource> Cost { get; protected set; }
    }
}