using DoctorTrainer.Entity;

namespace DoctorTrainer.Repository;

public class ImageDataMockRepository
{
    private static List<ImageData> _imageDataSet = new List<ImageData>();

    public ImageDataMockRepository()
    {
    }

    public ImageData? FindById(long imgId) =>
        _imageDataSet.FirstOrDefault(s => s.ImgId == imgId);

    public void Save(ImageData imageData)
    {
        _imageDataSet.Add(imageData);
    }

    public void Delete(ImageData imageData)
    {
        _imageDataSet.Remove(imageData);
    }
}