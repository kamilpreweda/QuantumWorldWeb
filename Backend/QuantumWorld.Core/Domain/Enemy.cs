using MongoDB.Bson.Serialization.Attributes;

namespace QuantumWorld.Core.Domain
{
    [BsonKnownTypes(typeof(PiratesEnemy), typeof(OutsidersEnemy), typeof(RebelsEnemy), typeof(ArmamentsEnemy), typeof(DistantsEnemy), typeof(AncientsEnemy))]
    public abstract class Enemy
    {
        public string Name { get; protected set; } = string.Empty;
        public EnemyType Type { get; protected set; }
        public abstract string Description { get; }
        public bool IsDefeated { get; protected set; }
        public TimeSpan TimeToAttack { get; protected set; }
        protected abstract TimeSpan BaseTimeToAttack { get; }
        protected abstract float TimeMultiplier { get; }
        public abstract List<Resource> BaseRewards { get; }
        public List<Resource> Rewards { get; set; }
        public bool IsUnderAttack { get; protected set; }
        public DateTime FinishDate { get; protected set; }
        public abstract List<Ship> BaseShips { get; }
        public List<Ship> Ships { get; protected set; }

        public Enemy()
        {
            AutoSetBasicAttributes();
            Ships = GetEnemyBaseShips();
            Rewards = GetBaseRewards();

        }
        public List<Ship> GetEnemyBaseShips()
        {
            return BaseShips;
        }
        public int GetEnemyTotalHP()
        {
            int result = 0;
            foreach (var ship in Ships)
            {
                result += ship.GetTotalHP();
            }
            return result;
        }
        public int GetEnemyTotalAP()
        {
            int result = 0;
            foreach (var ship in Ships)
            {
                result += ship.GetTotalAP();
            }
            return result;
        }
        public List<Resource> GetBaseRewards()
        {
            return BaseRewards;
        }
        public List<Resource> GetRewards()
        {
            return Rewards;
        }

        public List<Ship> GetShips()
        {
            return Ships;
        }

        public bool Defeat()
        {
            IsDefeated = true;
            return true;
        }
        private void SetTime()
        {
            TimeToAttack = BaseTimeToAttack;
        }
        private void SetNewTime()
        {
            TimeToAttack = BaseTimeToAttack * TimeMultiplier;
        }
        private void AutoSetBasicAttributes()
        {
            SetName();
            SetType();
            SetTime();
        }
        private void SetName()
        {
            Name = this.GetType().Name;
        }
        private void SetType()
        {
            if (!Enum.IsDefined(typeof(EnemyType), Name))
            {
                throw new Exception("Enemy type not found.");
            }
            EnemyType enemyType;
            Enum.TryParse(Name, out enemyType);
            Type = enemyType;
        }
    }
}
