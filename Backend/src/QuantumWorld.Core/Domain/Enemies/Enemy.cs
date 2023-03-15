using MongoDB.Bson.Serialization.Attributes;

namespace QuantumWorld.Core.Domain
{
    [BsonKnownTypes(typeof(PiratesEnemy), typeof(OutsidersEnemy), typeof(RebelsEnemy), typeof(ArmamentsEnemy), typeof(DistantsEnemy), typeof(AncientsEnemy))]
    public abstract class Enemy
    {
        public string Name { get; protected set; } = string.Empty;
        public EnemyType Type { get; protected set; }
        public abstract string BaseDescription { get; }
        public string Description { get; set; }
        public bool IsDefeated { get; protected set; } = false;
        public float TimeToAttackInSeconds { get; protected set; }
        protected abstract float BaseTimeToAttackInSeconds { get; }
        protected abstract float TimeMultiplier { get; }
        public abstract List<Resource> BaseRewards { get; }
        public List<Resource> Rewards { get; set; }
        public bool IsUnderAttack { get; protected set; }
        public DateTime? AttackStartDate { get; protected set; } = null;
        public abstract List<Ship> BaseShips { get; }
        public List<Ship> Ships { get; protected set; }
        public abstract List<Research> BaseRequirements { get; }
        public List<Research> Requirements { get; protected set; }

        public Enemy()
        {
            AutoSetBasicAttributes();
            Ships = GetEnemyBaseShips();
            Rewards = GetBaseRewards();
            Requirements = GetBaseRequirements();

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

        public List<Research> GetBaseRequirements()
        {
            return BaseRequirements;
        }

        public List<Ship> GetShips()
        {
            return Ships;
        }

        public bool Defeat()
        {
            IsDefeated = true;
            return IsDefeated;

        }
        public void SetAttackStartDate(DateTime date)
        {
            AttackStartDate = date;
        }
        public void SetTimeToAttackInSeconds(float seconds)
        {
            TimeToAttackInSeconds = seconds;
        }

        public void SetDefaultTimeToAttackInSeconds()
        {
            TimeToAttackInSeconds = BaseTimeToAttackInSeconds;
        }

        public void ClearAttackStartDate()
        {
            AttackStartDate = null;
        }

        public void IsEnemyUnderAttack(bool value)
        {
            IsUnderAttack = value;
        }
        private void SetTime()
        {
            TimeToAttackInSeconds = BaseTimeToAttackInSeconds;
        }
        private void SetNewTime()
        {
            TimeToAttackInSeconds = BaseTimeToAttackInSeconds * TimeMultiplier;
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
