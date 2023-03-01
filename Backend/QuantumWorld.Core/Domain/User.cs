using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;



namespace QuantumWorld.Core.Domain
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        private static readonly Regex NameRegex = new Regex(@"^\w+$");
        private IBattle _battle;
        public Guid Id { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
        public byte[] PasswordSalt { get; protected set; }
        public string Username { get; protected set; } = string.Empty;
        public DateTime CreateDate { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        public List<Resource> Resources { get; set; }
        public List<Building> Buildings { get; set; }
        public int AvailibleSpace { get; set; }
        public int UsedSpace { get; set; }
        public List<Research> Research { get; set; }
        public List<Ship> Ships { get; set; }
        public List<Enemy> Enemies { get; set; }
        public int EnemiesDefeated { get; set; }
        public double Points { get; set; }
        public List<string> BattleRaport { get; set; }
        public List<Message> Messages { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        protected User()
        {

        }

        public User(Guid id, byte[] salt, byte[] hash, string username)
        {
            Id = Guid.NewGuid();
            PasswordSalt = salt;
            PasswordHash = hash;
            Username = username;
            CreateDate = DateTime.Now;
            LastUpdated = DateTime.Now;
            Resources = new List<Resource>()
            {
                new CarbonFiberResource(),
                new QuantumGlassResource(),
                new HiggsBosonResource(),
            };
            Buildings = new List<Building>()
            {
                new CarbonFiberFactory(),
                new QuantumGlassFactory(),
                new HiggsBosonDetector(),
                new Labolatory(),
                new SpaceshipFactory(),
            };
            Research = new List<Research>()
            {
                new TheExpanseResearch(),
                new ArtOfWarResearch(),
                new HyperdriveResearch(),
                new TerraformingResearch(),
            };
            Ships = new List<Ship>()
            {
                new LightFighterShip(),
                new HeavyFighterShip(),
                new Battleship(),
                new Destroyer(),
                new Dreadnought(),
                new Mothership(),
            };
            Enemies = new List<Enemy>()
            {
                new PiratesEnemy(),
                new OutsidersEnemy(),
                new RebelsEnemy(),
                new ArmamentsEnemy(),
                new DistantsEnemy(),
                new AncientsEnemy()
            };
            EnemiesDefeated = 0;
            AvailibleSpace = 15;
            UsedSpace = 0;
            Points = 0;
            Messages = new();
        }
        public void SetUsername(string username)
        {
            if (!NameRegex.IsMatch(username))
            {
                throw new Exception("Username is invalid.");
            }
            Username = username.ToLowerInvariant();
            LastUpdated = DateTime.Now;
        }
        public void UpgradeBuilding(BuildingType type)

        {
            var building = Buildings.SingleOrDefault(b => b.Type == type);

            if (building == null)
            {
                throw new Exception("There is no such building.");
            }

            if (CanAfford(building.Cost) && HasEnoughSpace())
            {
                CalculateResourcesBasedOnTimeSpan();
                SpendResources(Resources, building.Cost);
                CalculatePoints(building.Cost);
                building.UpgradeBuilding();
                IncreaseUsedSpace();
                ReduceTimes(type);
                IncreaseResourcesIncome(type);   
                LastUpdated = DateTime.Now;
            }
        }
        public void UpgradeResearch(ResearchType type)
        {
            var research = Research.SingleOrDefault(r => r.Type == type);

            if (research == null)
            {
                throw new Exception("There is no such research.");
            }

            if ((CanAfford(research.Cost)) && (CheckLabolatoryLevel(research)))
            {
                CalculateResourcesBasedOnTimeSpan();
                SpendResources(Resources, research.Cost);
                CalculatePoints(research.Cost);
                research.UpgradeResearch();
                IncreaseAvailibleSpace(research);
                LastUpdated = DateTime.Now;
            }
        }
        public void BuildShip(ShipType type, int count)
        {
            var ship = Ships.SingleOrDefault(s => s.Type == type);

            if (ship == null)
            {
                throw new Exception("There is no such ship.");
            }

            for (int i = 0; i < count; i++)
            {

                if ((CanAfford(ship.Cost) && CheckSpaceshipFactoryLevel(ship)))
                {
                    CalculateResourcesBasedOnTimeSpan();
                    SpendResources(Resources, ship.Cost);
                    CalculatePoints(ship.Cost);
                    ship.BuildShip();
                    LastUpdated = DateTime.Now;
                }
            }
        }
        public void StartBattle(EnemyType type)
        {
            _battle = new Battle();

            var enemy = Enemies.FirstOrDefault(e => e.Type == type);
            if (enemy == null)
            {
                throw new Exception("There is no such enemy");
            }
            if (CheckRequirements(enemy))
            {
                CalculateResourcesBasedOnTimeSpan();
                _battle.StartBattle(Ships, Resources, enemy);
            }
            if (enemy.IsDefeated)
            {
                EnemiesDefeated++;
            }

            BattleRaport = _battle.GetRaport();
            Messages.Add(new Message("Battle Raport", BattleRaport));
            LastUpdated = DateTime.Now;
        }
        public void DeleteMessage(int id)
        {
            var message = Messages.FirstOrDefault(m => m.Id == id);
            Messages.Remove(message);
        }
        public List<Resource> GetResources(){
            CalculateResourcesBasedOnTimeSpan();
            LastUpdated = DateTime.Now;
            return Resources;
        }
        private bool CanAfford(List<Resource> cost)
        {
            foreach (var costResource in cost)
            {
                var currentPlayerResource = Resources.FirstOrDefault(r => r.Type == costResource.Type);
                if (currentPlayerResource == null)
                {
                    throw new Exception("There is no such resource.");
                }
                if (currentPlayerResource.Value < costResource.Value)
                {
                    return false;
                }
            }
            return true;
        }
        private bool HasEnoughSpace()
        {
            if (AvailibleSpace > UsedSpace)
            {
                return true;
            }
            else
                throw new Exception("Not enough space!");
        }
        private void SpendResources(List<Resource> resources, List<Resource> cost)
        {
            foreach (var costResource in cost)
            {
                var currentPlayerResource = resources.Where(r => r.Type == costResource.Type).FirstOrDefault();
                currentPlayerResource.Value -= costResource.Value;
            }
        }
        private void IncreaseUsedSpace()
        {
            UsedSpace++;
        }
        private void IncreaseAvailibleSpace(Research research)
        {
            if (research.Name == "TerraformingResearch")
            {
                AvailibleSpace += 15;
            }
        }
        private void CalculatePoints(List<Resource> spentResources)
        {
            foreach (var resource in spentResources)
            {
                Points += resource.Value;
            }
            Points /= 1000;
            Points = Math.Round(Points, 2);
        }
        private bool CheckRequirements(Enemy enemy)
        {
            foreach (var requirement in enemy.Requirements)
            {
                var currentPlayerResearch = Research.Where(r => r.Type == requirement.Type).FirstOrDefault();
                if (currentPlayerResearch.Level < requirement.Level)
                {
                    return false;
                    throw new Exception("Requirements not met!");
                }
            }
            return true;
        }
        private bool CheckSpaceshipFactoryLevel(Ship ship)
        {
            if (ship.GetSpaceshipLevelRequirement() > Buildings.Where(b => b.Type == BuildingType.SpaceshipFactory).FirstOrDefault().Level)
            {
                return false;
                throw new Exception("Spaceship Factory level is too low!");
            }
            return true;
        }
        private bool CheckLabolatoryLevel(Research research)
        {
            if (research.GetLablolatoryLevelRequirement() > Buildings.Where(b => b.Type == BuildingType.Labolatory).FirstOrDefault().Level)
            {
                return false;
                throw new Exception("Labolatory level is too low!");
            }
            return true;
        }
        private void ReduceTimes(BuildingType type)
        {
            if (type == BuildingType.Labolatory)
            {
                foreach (var research in Research)
                {
                    research.CutTimeToBuildByHalf();
                }
            }

            if (type == BuildingType.SpaceshipFactory)
            {
                foreach (var ship in Ships)
                {
                    ship.CutTimeToBuildByHalf();
                }
            }
        }
        private void IncreaseResourcesIncome(BuildingType type)
        {
            var carbonFiber = Resources.SingleOrDefault(r => r.Type == ResourceType.CarbonFiberResource);
            var quantumGlass = Resources.SingleOrDefault(r => r.Type == ResourceType.QuantumGlassResource);
            var higgsBoson = Resources.SingleOrDefault(r => r.Type == ResourceType.HiggsBosonResource);

            if (type == null)
            {
                throw new Exception("There is no such building.");
            }
            if (type == BuildingType.CarbonFiberFactory)
            {
                carbonFiber.IncreaseIncome(2);
            }
            if (type == BuildingType.QuantumGlassFactory)
            {
                quantumGlass.IncreaseIncome(1.75F);
            }
            if (type == BuildingType.HiggsBosonDetector)
            {
                higgsBoson.IncreaseIncome(1.5F);
            }
            else
            {
                return;
            }
        }
        private void CalculateResourcesBasedOnTimeSpan()
        {
            TimeSpan timeSpan = DateTime.Now - LastUpdated;
            int seconds = timeSpan.Seconds;
            for (int i = 0; i < seconds; i++){
                foreach (var resource in Resources){
                    resource.Value += resource.Income;
                }
            }
        }
    }
}