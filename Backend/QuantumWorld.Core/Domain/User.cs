using System.Text.RegularExpressions;

namespace QuantumWorld.Core.Domain
{
    public class User
    {
        private static readonly Regex NameRegex = new Regex(@"^\w+$");
        public Guid Id { get; protected set; }
        public string Email { get; protected set; } = string.Empty;
        public string Password { get; protected set; } = string.Empty;
        public string Salt { get; protected set; } = string.Empty;
        public string Username { get; protected set; } = string.Empty;
        public DateTime CreateDate { get; protected set; }
        public DateTime LastUpdated { get; protected set; }

        protected User()
        {

        }

        public User(string email, string password, string salt, string username)
        {
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Password = password;
            Salt = salt;
            Username = username;
            CreateDate = DateTime.UtcNow;
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
    }
}