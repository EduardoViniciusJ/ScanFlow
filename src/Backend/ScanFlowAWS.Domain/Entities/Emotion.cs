namespace ScanFlowAWS.Domain.Entities
{
    public class Emotion : EntityBase
    {
        public int UserId { get; set; }
        public int PhotoId { get; set; }    

        public double Happy {  get; set; }
        public double Sad {  get; set; } 
        public double Angry {  get; set; }
        public double Surprised {  get; set; }
    }
}
