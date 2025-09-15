namespace ScanFlowAWS.Domain.ValueObjects
{
    public class ImageFace
    {
        public string Type { get; set; } 
        public float Confidence { get; set; }   
        
        public ImageFace(string type, float confidence)
        {
            Type = type;
            Confidence = confidence;
        }
    }
}
