using System.Text.RegularExpressions;

namespace QuantumWorld.Core.Domain
{
    public class User
    {
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

        public User(Guid userId, string email, string password, byte[] salt, byte[] hash, string username, List<Resource> resources, List<Building> buildings, List<Research> research, List<Ship> ships, List<Enemy> enemies, IBattle battle)
        {
            Id = userId;
            Email = email.ToLowerInvariant();
            Password = password;
            PasswordSalt = salt;
            PasswordHash = hash;
            Username = username;
            CreateDate = DateTime.UtcNow;
            Resources = resources;
            Buildings = buildings;
            Research = research;
            Ships = ships;
            Enemies = enemies;
            _battle = battle;
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