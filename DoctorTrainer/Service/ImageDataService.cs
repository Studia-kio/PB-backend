using DoctorTrainer.Entity;
using DoctorTrainer.Repository;

namespace DoctorTrainer.Service;

public class ImageDataService
{
    private readonly ImageDataRepository _imageDataRepository;

    public ImageDataService(ImageDataRepository imageDataRepository)
    {
        _imageDataRepository = imageDataRepository;
    }

    public List<ImageData> FindAllImageData()
    {
        return _imageDataRepository.FindAll();
    }
    
    public ImageData? FindDataByImageId(string imageId)
    {
        return _imageDataRepository.FindByImageId(imageId);
    }

    public void SaveImageData(ImageData imageData)
    {
        _imageDataRepository.Save(imageData);
    }

    public void UpdateImageData(string imageId, ImageData imageData)
    {
        ImageData? data = _imageDataRepository.FindByImageId(imageId);
        if (data != null)
        {
            _imageDataRepository.Delete(data);
        }
        _imageDataRepository.Save(imageData);
    }

    public void DeleteImageData(string imageId)
    {
        ImageData? data = _imageDataRepository.FindByImageId(imageId);
        if (data != null)
        {
            _imageDataRepository.Delete(data);
        }
    }
    
}