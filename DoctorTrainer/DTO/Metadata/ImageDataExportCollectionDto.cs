using DoctorTrainer.Entity;

namespace DoctorTrainer.DTO.Metadata;

public class ImageDataExportCollectionDto
{
    
    public List<ImageDataExportDto> Metadata { get; set; }

    public static ImageDataExportCollectionDto EntityToDto(List<ImageData> dataList)
    {
        return new ImageDataExportCollectionDto()
        {
            Metadata = dataList.Select(d => ImageDataExportDto.EntityToDto(d)).ToList()
        };
    }
    
}