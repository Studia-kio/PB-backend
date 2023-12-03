using DoctorTrainer.Entity;

namespace DoctorTrainer.DTO.Metadata;

public class ImageDataExportDto
{
    public string ImgId { get; set; }
    public string Filename { get; set; }
    public Dictionary<string, string> MedicalParams { get; set; }
    public List<Circle> MarkedRegions { get; set; }

    public static ImageDataExportDto EntityToDto(ImageData data)
    {
        return new ImageDataExportDto()
        {
            ImgId = data.ImgId,
            Filename = data.Filename,
            MedicalParams = data.MedicalParams,
            MarkedRegions = data.MarkedRegions
        };
    }
}