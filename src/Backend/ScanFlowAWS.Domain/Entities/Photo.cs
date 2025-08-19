namespace ScanFlowAWS.Domain.Entities
{
    public class Photo : EntityBase
    {
        public int UserId { get; set;} 
        public string URL{ get; set; } = string.Empty;
    }
}
