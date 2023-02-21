namespace QuantumWorld.Core.Domain
{
    public class Message
    {
        public List<string> Content { get; set; }
        public string Date { get; set; }
        public int Id { get; set; }

        public Message(List<string> content)
        {
            Content = content;
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Id++;
        }
    }
}