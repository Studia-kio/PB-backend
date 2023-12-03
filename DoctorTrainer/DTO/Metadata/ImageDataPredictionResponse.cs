namespace DoctorTrainer.DTO.Metadata;

public class ImageDataPredictionResponse
{
    public string ImageId { get; set; }
    public string ImageName { get; set; }
    public Dictionary<string, string> MedicalParams { get; set; }
    public List<Circle> MarkedRegions { get; set; }
}