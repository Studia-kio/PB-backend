using DoctorTrainer.Entity;

namespace DoctorTrainer.DTO;

public class ImageDataDto
{
    public long ImageId { get; set; }
    public Category Category { get; set; }
    public Dictionary<string, string> MedicalParams { get; set; }
    public List<Circle> MarkedRegions { get; set; }

    public static ImageDataDto EntityToDtoMapper(ImageData data)
    {
        Dictionary<string, string> medicalParams = new Dictionary<string, string>();
        List<Circle> markedRegions = new List<Circle>();

        foreach (var (key, value) in data.MedicalParams)
        {
            medicalParams.Add(key, value);
        }
        data.MarkedRegions.ForEach(r => markedRegions.Add(new Circle() { X = r.X, Y = r.Y, Radius = r.Radius }));

        return new ImageDataDto()
        {
            ImageId = data.ImgId,
            Category = new Category()
            {
                Type = data.Category.Type,
                Organ = data.Category.Organ,
                Index = data.Category.Index
            },
            MedicalParams = medicalParams,
            MarkedRegions = markedRegions,
        };
    }

    public static ImageData DtoToEntityMapper(ImageDataDto dto)
    {
        Dictionary<string, string> medicalParams = new Dictionary<string, string>();
        List<Circle> markedRegions = new List<Circle>();

        foreach (var (key, value) in dto.MedicalParams)
        {
            medicalParams.Add(key, value);
        }
        dto.MarkedRegions.ForEach(r => markedRegions.Add(new Circle() { X = r.X, Y = r.Y, Radius = r.Radius }));

        return new ImageData()
        {
            ImgId = dto.ImageId,
            Category = new Category()
            {
                Type = dto.Category.Type,
                Organ = dto.Category.Organ,
                Index = dto.Category.Index
            },
            MedicalParams = medicalParams,
            MarkedRegions = markedRegions,
        };
    }
    
    
}