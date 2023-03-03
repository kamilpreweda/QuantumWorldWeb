using MongoDB.Bson.Serialization.Attributes;

namespace QuantumWorld.Core.Domain
{
    [BsonKnownTypes(typeof(LightFighterShip), typeof(HeavyFighterShip), typeof(Battleship), typeof(Destroyer), typeof(Dreadnought), typeof(Mothership))]
    public abstract class Ship
    {
        public string Name { get; protected set; } = string.Empty;
        public ShipType Type { get; protected set; }
        public abstract string BaseDescription { get; }
        public string Description { get; set; }
        public int Count { get; protected set; }
        public float TimeToBuildInSeconds { get; protected set; }
        protected abstract float BaseTimeToBuildInSeconds { get; }
        protected abstract float TimeMultiplier { get; }
        protected abstract float CostMultiplier { get; }
        protected abstract List<Resource> BaseCost { get; }
        public List<Resource> Cost { get; protected set; } = new();
        public bool IsUnderConstruction { get; protected set; }
        public DateTime? ConstructionStartDate { get; protected set; } = null;
        protected abstract int BaseHealthPoints { get; }
        protected abstract int BaseAttackPower { get; }
        protected abstract int BaseSpaceshipFactoryLevelRequirement { get; }
        public int ShipsToBuild { get; protected set; }
        public int HealthPoints { get; protected set; }
        public int AttackPower { get; protected set; }
        public int SpaceshipFactoryLevelRequirement { get; protected set; }

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
            DecreaseShipsToBuidByOne();
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
        public void CutTimeToBuildByHalf()
        {
            TimeToBuildInSeconds /= 2;
            if (TimeToBuildInSeconds < 1)
            {
                TimeToBuildInSeconds = 1;
            }
        }
        public int GetSpaceshipLevelRequirement()
        {
            return SpaceshipFactoryLevelRequirement;
        }
        public void SetShipsToBuild(int count)
        {
            ShipsToBuild = count;
        }
        private void DecreaseShipsToBuidByOne()
        {
            ShipsToBuild -= 1;
        }
        private void SetTime()
        {
            TimeToBuildInSeconds = BaseTimeToBuildInSeconds;
        }
        private void SetNewTime()
        {
            TimeToBuildInSeconds = BaseTimeToBuildInSeconds * TimeMultiplier;
        }
        private void AutoSetBasicAttributes()
        {
            SetName();
            SetType();
            SetStats();
            SetCost();
            SetTime();
            SetDescription();
            SetLevelRequirement();
        }
        private void SetName()
        {
            Name = this.GetType().Name;
        }
        private void SetDescription()
        {
            Description = BaseDescription;
        }
        private void SetLevelRequirement()
        {
            SpaceshipFactoryLevelRequirement = BaseSpaceshipFactoryLevelRequirement;
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
        public void SetConstructionStartDate(DateTime date)
        {
            ConstructionStartDate = date;
        }
        public void SetTimeToBuildInSeconds(float seconds)
        {
            TimeToBuildInSeconds = seconds;
        }

        public void ClearConstructionStartDate()
        {
            ConstructionStartDate = null;
        }

        public void IsShipUnderConstruction(bool value)
        {
            IsUnderConstruction = value;
        }
    }
}
