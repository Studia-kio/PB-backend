using DoctorTrainer.Entity;
using DoctorTrainer.Repository;

namespace DoctorTrainer.Service;

public class ImageDataService
{
    private readonly ImageDataMockRepository _repository;

    public ImageDataService()
    {
        // todo: temporary solution for testing purposes, remove later on
        _repository = new ImageDataMockRepository();
    }

    public ImageData? FindImageData(long id)
    {
        return _repository.FindById(id);
    }

    public void SaveImageData(ImageData imageData)
    {
        _repository.Save(imageData);
    }

    public void UpdateImageData(long id, ImageData imageData)
    {
        ImageData? data = _repository.FindById(id);
        if (data != null)
        {
            _repository.Delete(data);
        }
        _repository.Save(imageData);
    }

    public void DeleteImageData(long id)
    {
        ImageData? data = _repository.FindById(id);
        if (data != null)
        {
            _repository.Delete(data);
        }
    }
}