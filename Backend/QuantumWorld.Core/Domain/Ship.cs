namespace QuantumWorld.Core.Domain
{
    public class Ship
    {
        public ShipType Type { get; protected set; }
        public int Count { get; protected set; }
        public string Description { get; protected set; }
        public TimeSpan TimeToBuild { get; protected set; }
        public float HealthPoints { get; protected set; }
        public float AttackPower { get; protected set; }
        public int ShipyardLevelRequirement { get; protected set; }
        public IEnumerable<Resource> Cost { get; protected set; }       
    }
}