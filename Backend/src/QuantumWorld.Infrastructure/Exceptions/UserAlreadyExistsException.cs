namespace QuantumWorld.Infrastructure.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public string Username { get; }

        public UserAlreadyExistsException(string username)
            : base($"User with username '{username}' already exists.")
        {
            Username = username;
        }
    }
}