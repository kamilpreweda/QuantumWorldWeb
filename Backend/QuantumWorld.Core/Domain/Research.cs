using MongoDB.Bson.Serialization.Attributes;

namespace QuantumWorld.Core.Domain
{
    [BsonKnownTypes(typeof(TheExpanseResearch), typeof(ArtOfWarResearch), typeof(HyperdriveResearch), typeof(TerraformingResearch))]
    public abstract class Research
    {
        public string Name { get; protected set; } = string.Empty;
        public ResearchType Type { get; protected set; }
        public abstract string BaseDescription { get; }
        public string Description { get; set; }
        public int Level { get; protected set; } = 0;
        public float TimeToBuildInSeconds { get; protected set; }
        protected abstract float BaseTimeToBuildInSeconds { get; }
        protected abstract float TimeMultiplier { get; }
        protected abstract float CostMultiplier { get; }
        protected abstract List<Resource> BaseCost { get; }
        public List<Resource> Cost { get; protected set; } = new();
        public bool IsUnderConstruction { get; protected set; }
        public DateTime? ConstructionStartDate { get; protected set; } = null;
        protected abstract int BaseLabolatoryLevelRequirement { get; }
        public int LabolatoryLevelRequirement { get; protected set; }

        public Research()
        {
            AutoSetBasicAttributes();
        }
        public Research(int level)
        {
            AutoSetBasicAttributes();
            SetLevel(level);
        }
        public int GetLevel()
        {
            return Level;
        }
        public void CutTimeToBuildByHalf()
        {
            TimeToBuildInSeconds /= 2;
        }

        public int GetLablolatoryLevelRequirement()
        {
            return LabolatoryLevelRequirement;
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
            TimeToBuildInSeconds = BaseTimeToBuildInSeconds;
        }
        private void SetNewTime()
        {
            TimeToBuildInSeconds = BaseTimeToBuildInSeconds * TimeMultiplier * (Level + 1);
        }
        private void SetDescription()
        {
            Description = BaseDescription;
        }
        private void AutoSetBasicAttributes()
        {
            SetName();
            SetType();
            SetCost();
            SetTime();
            SetDescription();
            SetLevelRequirement();
        }
        private void SetName()
        {
            Name = this.GetType().Name;
        }

        private void SetLevelRequirement()
        {
            LabolatoryLevelRequirement = BaseLabolatoryLevelRequirement;
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

        public void IsResearchUnderConstruction(bool value)
        {
            IsUnderConstruction = value;
        }
    }
}