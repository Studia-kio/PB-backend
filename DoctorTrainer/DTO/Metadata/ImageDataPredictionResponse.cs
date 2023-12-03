namespace DoctorTrainer.DTO.Metadata;

public class ImageDataPredictionResponse
{
    public string ImageId { get; set; }
    public string ImageName { get; set; }
    public Dictionary<string, string> MedicalParams { get; set; }
    public List<CircleDto> MarkedRegions { get; set; }
    
    public class CircleDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
    }
}