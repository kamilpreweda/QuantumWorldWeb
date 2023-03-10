using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.DTO
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Password {get; set;}
        public Guid Id {get; set;}
        public List<Resource> Resources { get; set; }
        public List<Building> Buildings { get; set; }
        public List<Research> Research { get; set; }
        public List<Ship> Ships { get; set; }
        public List<Enemy> Enemies { get; set; }
        public int AvailibleSpace { get; set; }
        public int UsedSpace { get; set; }
        public int EnemiesDefeated { get; set; }
        public int Points { get; set; }
        public List<Message> Messages { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}