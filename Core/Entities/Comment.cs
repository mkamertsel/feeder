namespace Core.Entities
{
    public class Comment
    {
        public string CommenterEmail { get; set; }
        public string CommenterName { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
    }
}