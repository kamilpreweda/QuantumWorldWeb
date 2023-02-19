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
        public string Email { get; protected set; } = string.Empty;
        public string Password { get; protected set; } = string.Empty;
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

        protected User()
        {

        }

        public User(Guid id, string email, string password, byte[] salt, byte[] hash, string username)
        {
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Password = password;
            PasswordSalt = salt;
            PasswordHash = hash;
            Username = username;
            CreateDate = DateTime.UtcNow;
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
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email can not be empty.");
            }
            Email = email.ToLowerInvariant();
            LastUpdated = DateTime.UtcNow;
        }
        public void SetUsername(string username)
        {
            if (!NameRegex.IsMatch(username))
            {
                throw new Exception("Username is invalid.");
            }
            Username = username.ToLowerInvariant();
            LastUpdated = DateTime.UtcNow;
        }
        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password can not be empty.");
            }
            if (password.Length < 4)
            {
                throw new Exception("Password must contain at least 4 characters.");
            }
            if (password.Length > 100)
            {
                throw new Exception("Password can not contain more than 100 characters.");
            }
            Password = password;
            LastUpdated = DateTime.UtcNow;
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
                SpendResources(Resources, building.Cost);
                CalculatePoints(building.Cost);
                building.UpgradeBuilding();
                IncreaseUsedSpace();

            }
        }
        public void UpgradeResearch(ResearchType type)
        {
            var research = Research.SingleOrDefault(r => r.Type == type);

            if (research == null)
            {
                throw new Exception("There is no such research.");
            }

            if (CanAfford(research.Cost))
            {
                SpendResources(Resources, research.Cost);
                CalculatePoints(research.Cost);
                research.UpgradeResearch();
                IncreaseAvailibleSpace(research);
            }
        }
        public void BuildShip(ShipType type)
        {
            var ship = Ships.SingleOrDefault(s => s.Type == type);

            if (ship == null)
            {
                throw new Exception("There is no such ship.");
            }

            if (CanAfford(ship.Cost))
            {
                SpendResources(Resources, ship.Cost);
                CalculatePoints(ship.Cost);
                ship.BuildShip();
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
                _battle.StartBattle(Ships, Resources, enemy);
            }
            if (enemy.IsDefeated)
            {
                EnemiesDefeated++;
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
    }
}