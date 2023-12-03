using DoctorTrainer.Entity;

namespace DoctorTrainer.DTO.Metadata;

public class ImageDataPredictionRequest
{
    public string ImageId { get; set; }
    public string ImageName { get; set; }

    public static ImageDataPredictionRequest EntityToDto(ImageData data)
    {
        return new ImageDataPredictionRequest()
        {
            ImageId = data.ImgId,
            ImageName = data.Filename
        };
    }
}