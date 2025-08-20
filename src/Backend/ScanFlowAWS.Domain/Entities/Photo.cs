namespace ScanFlowAWS.Domain.Entities
{
    public class Photo : EntityBase
    {
        public int UserId { get; set;} 
        public string URL{ get; set; } = string.Empty;
        public User? User { get; set; }
        public ICollection<Emotion> Emotions { get; set; } = new List<Emotion>();
    }
}
