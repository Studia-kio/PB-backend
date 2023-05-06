using DoctorTrainer.Entity;

namespace DoctorTrainer.Repository;

public class ImageDataMockRepository
{
    private List<ImageData> _imageDataSet;

    public ImageDataMockRepository()
    {
        _imageDataSet = new List<ImageData>();
        // todo: hardcode some examples
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