namespace QuantumWorld.Core.Domain
{
    public class Message
    {
        public string Title {get; set;}
        public List<string> Content { get; set; }
        public string Date { get; set; }
        public int Id { get; set; }

        public Message(string title, List<string> content)
        {
            Title = title;
            Content = content;
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Id++;
        }
    }
}