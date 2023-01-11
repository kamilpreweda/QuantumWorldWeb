namespace QuantumWorld.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; } = string.Empty;
        public string Password { get; protected set; } = string.Empty;
        public string Salt { get; protected set; } = string.Empty;
        public string Username { get; protected set; } = string.Empty;
        public DateTime CreateDate { get; protected set; }

        protected User()
        {

        }

        public User(string email, string password, string salt, string username)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            Salt = salt;
            Username = username;
            CreateDate = DateTime.UtcNow;
        }
    }

}