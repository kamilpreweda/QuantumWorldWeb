namespace QuantumWorld.Infrastructure.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public string Username { get; }
        public string Password { get; }

        public InvalidCredentialsException(string username, string password) : base("Invalid Credentials.")
        {
            Username = username;
            Password = password;
        }
    }
}