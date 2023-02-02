using QuantumWorld.Core.Domain;

namespace QuantumWorld.Infrastructure.DTO
{
    public class UserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public List<Resource> Resources { get; set; }
        public List<Building> Buildings { get; set; }
        public List<Research> Research { get; set; }
        public List<Ship> Ships { get; set; }
        public List<Enemy> Enemies { get; set; }
    }
}