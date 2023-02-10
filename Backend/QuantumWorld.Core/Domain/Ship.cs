using MongoDB.Bson.Serialization.Attributes;

namespace QuantumWorld.Core.Domain
{
    [BsonKnownTypes(typeof(LightFighterShip), typeof(HeavyFighterShip), typeof(Battleship), typeof(Destroyer), typeof(Dreadnought), typeof(Mothership))]
    public abstract class Ship
    {
        public string Name { get; protected set; } = string.Empty;
        public ShipType Type { get; protected set; }
        public abstract string Description { get; }
        public int Count { get; protected set; }
        public TimeSpan TimeToBuild { get; protected set; }
        protected abstract TimeSpan BaseTimeToBuild { get; }
        protected abstract float TimeMultiplier { get; }
        protected abstract float CostMultiplier { get; }
        protected abstract List<Resource> BaseCost { get; }
        public List<Resource> Cost { get; protected set; } = new();
        public bool IsUnderConstruction { get; protected set; }
        public DateTime FinishDate { get; protected set; }
        protected abstract int BaseHealthPoints { get; }
        protected abstract int BaseAttackPower { get; }
        public int HealthPoints { get; protected set; }
        public int AttackPower { get; protected set; }

        public Ship()
        {
            AutoSetBasicAttributes();
        }
        public Ship(int count)
        {
            AutoSetBasicAttributes();
            SetCount(count);
        }
        public int GetTotalHP()
        {
            var totalHP = Count * HealthPoints;
            return totalHP;
        }
        public int GetTotalAP()
        {
            var totalAP = Count * AttackPower;
            return totalAP;
        }
        public void BuildShip()
        {
            Count++;
        }
        public void SetCount(int count)
        {
            Count = count;
        }
        public int GetCount()
        {
            return Count;
        }
        public void CalculateCount(int count)
        {
            Count += count;
        }
        private void SetNewTime()
        {
            TimeToBuild = BaseTimeToBuild * TimeMultiplier;
        }
        private void AutoSetBasicAttributes()
        {
            SetName();
            SetType();
            SetStats();
            SetCost();
            SetNewTime();
        }
        private void SetName()
        {
            Name = this.GetType().Name;
        }
        private void SetType()
        {
            if (!Enum.IsDefined(typeof(ShipType), Name))
            {
                throw new Exception("Building type not found.");
            }
            ShipType shipType;
            Enum.TryParse(Name, out shipType);
            Type = shipType;
        }
        private void SetCost()
        {
            Cost = BaseCost;
        }
        private void SetStats()
        {
            HealthPoints = BaseHealthPoints;
            AttackPower = BaseAttackPower;
        }
    }
}
