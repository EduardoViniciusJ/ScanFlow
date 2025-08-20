namespace ScanFlowAWS.Domain.Entities
{
    public class User : EntityBase
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public ICollection<Emotion> Emotions { get; set; } = new List<Emotion>();
    }
}
