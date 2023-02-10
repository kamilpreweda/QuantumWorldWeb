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
        private readonly IBattle _battle;
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
        public List<Research> Research { get; set; }
        public List<Ship> Ships { get; set; }
        public List<Enemy> Enemies { get; set; }

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
            _battle = new Battle();
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

            if (CanAfford(building.Cost))
            {
                SpendResources(Resources, building.Cost);
                building.UpgradeBuilding();
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
                research.UpgradeResearch();
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
                ship.BuildShip();
            }
        }
        public void StartBattle(EnemyType type)
        {

            var enemy = Enemies.FirstOrDefault(e => e.Type == type);
            if (enemy == null)
            {
                throw new Exception("There is no such enemy");
            }
            _battle.StartBattle(Ships, Resources, enemy);
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
        private void SpendResources(List<Resource> resources, List<Resource> cost)
        {
            foreach (var costResource in cost)
            {
                var currentPlayerResource = resources.Where(r => r.Type == costResource.Type).FirstOrDefault();
                currentPlayerResource.Value -= costResource.Value;
            }
        }
    }
}