namespace QuantumWorld.Core.Domain
{
    public class Building
    {
        public BuildingType Type { get; protected set; }
        public int Lvl { get; set; }
        public string Description { get; protected set; }
        public TimeSpan TimeToBuild { get; protected set; }
        public float CostMultiplier { get; protected set; }
        public IEnumerable<Resource> Cost { get; protected set; }
    }
}