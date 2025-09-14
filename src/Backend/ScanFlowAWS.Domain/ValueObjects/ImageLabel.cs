namespace ScanFlowAWS.Domain.ValueObjects
{
    public class ImageLabel
    {
        public string Name { get; set; }
        public float Confidence { get; set; } 

        public ImageLabel(string name, float confidence)
        {
            Name = name;
            Confidence = confidence;
        }

    }
}
