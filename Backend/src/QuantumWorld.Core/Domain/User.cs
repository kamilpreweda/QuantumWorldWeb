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
            SetWelcomingMessage();
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
                CalculatePoints(building.Cost);
                building.UpgradeBuilding();
                IncreaseUsedSpace();
                ReduceTimes(type);
                IncreaseResourcesIncome(type);
                LastUpdated = DateTime.Now;
                if (CheckIfSpaceIsRunningOut())
                {
                    SendUsedSpaceWarningMessage();
                }
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
                CalculatePoints(research.Cost);
                research.UpgradeResearch();
                research.SetNewTime(GetLabolatoryLevel());
                IncreaseAvailibleSpace(research);
                LastUpdated = DateTime.Now;
            }
        }
        public void BuildShip(ShipType type)
        {
            var ship = Ships.SingleOrDefault(s => s.Type == type);

            if (ship == null)
            {
                throw new Exception("There is no such ship.");
            }
            if ((CanAfford(ship.Cost) && CheckSpaceshipFactoryLevel(ship)))
            {
                CalculateResourcesBasedOnTimeSpan();
                CalculatePoints(ship.Cost);
                ship.BuildShip();
                ship.SetNewTime(GetSpaceshipFactoryLevel());
                LastUpdated = DateTime.Now;
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
        public List<Resource> GetResources()
        {
            CalculateResourcesBasedOnTimeSpan();
            LastUpdated = DateTime.Now;
            return Resources;
        }
        public void SpendResources(List<Resource> resources, List<Resource> cost)
        {
            if (CanAfford(cost))
            {
                foreach (var costResource in cost)
                {
                    var currentPlayerResource = resources.Where(r => r.Type == costResource.Type).FirstOrDefault();
                    currentPlayerResource.Value -= costResource.Value;
                }
            }
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
                Points += (resource.Value / 1000);
            }
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
            for (int i = 0; i < seconds; i++)
            {
                foreach (var resource in Resources)
                {
                    resource.Value += resource.Income;
                }
            }
        }
        private int GetSpaceshipFactoryLevel()
        {
            var factoryLevel = Buildings.Where(b => b.Type == BuildingType.SpaceshipFactory).FirstOrDefault().Level;
            return factoryLevel;
        }
        private int GetLabolatoryLevel()
        {
            var labolatoryLevel = Buildings.Where(b => b.Type == BuildingType.Labolatory).FirstOrDefault().Level;
            return labolatoryLevel;
        }

        private bool CheckIfSpaceIsRunningOut()
        {
            if (AvailibleSpace - UsedSpace <= 5)
            {
                return true;
            }
            return false;
        }

        private void SetWelcomingMessage()
        {
            List<string> welcomingMessage = new();
            string messageContent = "Welcome to the Quantum World! You have just entered a universe full of wonders and possibilities, where technology has advanced beyond your wildest imagination. Carbon fiber, quantum glass, and the elusive Higgs Boson are the building blocks of this world, and with them, you can construct incredible spaceships, research advanced technologies, and explore new frontiers. Get ready to embark on an adventure that will take you to the far reaches of the cosmos and beyond. The future is now, and you're about to be a part of it. Enjoy your journey!";
            welcomingMessage.Add(messageContent);
            Messages.Add(new Message("Welcome", welcomingMessage));
        }

        private void SendUsedSpaceWarningMessage()
        {
            string messageContent = "Attention, Captain! Our civilization is expanding rapidly, and we're running out of space for our buildings. If we don't take action soon, we risk losing valuable resources and stunting our growth. It's recommended that you construct a laboratory and begin research on Terraforming to increase our available space. Don't wait too long, time is running out!";
            if (!Messages.Any(m => m.Content.Any(c => c == messageContent)))
            {
                List<string> usedSpaceMessage = new();
                usedSpaceMessage.Add(messageContent);
                Messages.Add(new Message("Houston, We Have a Space Problem", usedSpaceMessage));
            }
        }
    }
}