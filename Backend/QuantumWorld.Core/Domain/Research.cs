using MongoDB.Bson.Serialization.Attributes;

namespace QuantumWorld.Core.Domain
{
    [BsonKnownTypes(typeof(TheExpanseResearch), typeof(ArtOfWarResearch), typeof(HyperdriveResearch), typeof(TerraformingResearch))]
    public abstract class Research
    {
        public string Name { get; protected set; } = string.Empty;
        public ResearchType Type { get; protected set; }
        public abstract string Description { get; }
        public int Level { get; protected set; } = 0;
        public TimeSpan TimeToBuild { get; protected set; }
        protected abstract TimeSpan BaseTimeToBuild { get; }
        protected abstract float TimeMultiplier { get; }
        protected abstract float CostMultiplier { get; }
        protected abstract List<Resource> BaseCost { get; }
        public List<Resource> Cost { get; protected set; } = new();
        public bool IsUnderConstruction { get; protected set; }
        public DateTime FinishDate { get; protected set; }

        public Research()
        {
            AutoSetBasicAttributes();
        }
        public Research(int level)
        {
            AutoSetBasicAttributes();
            SetLevel(level);
        }
        private void IncreaseLevel()
        {
            Level++;
        }
        private void SetNewCost()
        {
            foreach (var cost in Cost)
            {
                cost.Value *= CostMultiplier;
            }
        }
        private void SetTime()
        {
            TimeToBuild = BaseTimeToBuild;
        }
        private void SetNewTime()
        {
            TimeToBuild = BaseTimeToBuild * TimeMultiplier * (Level + 1);
        }
        private void AutoSetBasicAttributes()
        {
            SetName();
            SetType();
            SetCost();
            SetTime();
        }
        private void SetName()
        {
            Name = this.GetType().Name;
        }
        private void SetType()
        {
            if (!Enum.IsDefined(typeof(ResearchType), Name))
            {
                throw new Exception("Building type not found.");
            }
            ResearchType researchType;
            Enum.TryParse(Name, out researchType);
            Type = researchType;
        }
        private void SetCost()
        {
            Cost = BaseCost;
        }
        private void SetLevel(int level)
        {
            Level = level;
        }
        public void UpgradeResearch()
        {
            SetNewTime();
            SetNewCost();
            IncreaseLevel();
        }
    }
}