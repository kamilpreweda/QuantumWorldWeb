namespace QuantumWorld.Core.Domain
{
    public class Player
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; set; }
        public IEnumerable<Resource> Resources { get; protected set; }
        public IEnumerable<Building> Buildings { get; protected set; }
        public IEnumerable<Research> Research { get; protected set; }
        public IEnumerable<Ship> Ships { get; protected set; }
        public IEnumerable<Enemy> Enemies { get; protected set; }
    }
}