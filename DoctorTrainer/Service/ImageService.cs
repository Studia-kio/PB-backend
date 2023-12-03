using DoctorTrainer.Entity;
using DoctorTrainer.Repository;

namespace DoctorTrainer.Service;

public class ImageService
{
    private readonly IConfiguration _configuration;
    private readonly ImageDataRepository _imageDataRepository;
    
    public ImageService(IConfiguration configuration, ImageDataRepository imageDataRepository)
    {
        _configuration = configuration;
        _imageDataRepository = imageDataRepository;
    }

    public async Task<string> AddImage(byte[] imageBytes, string filename)
    {
        string fileId = Guid.NewGuid().ToString();
        string extension = Path.GetExtension(filename);
        string imgPath = GetImagePath(extension);

        using (var stream = new FileStream(imgPath + fileId + extension, FileMode.Create))
        {
            await stream.WriteAsync(imageBytes);
        }

        return fileId;
    }

    public async Task<byte[]> GetImageBytes(string id)
    {
        ImageData data = _imageDataRepository.FindByImageId(id);
        string url = GetImagePath(Path.GetExtension(data.Filename)) + data.Filename;
        return File.ReadAllBytes(url);;
    }

    public bool RemoveImage(string id)
    {
        ImageData data = _imageDataRepository.FindByImageId(id);

        string path = GetFilePath(data);
        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }

        return false;
    }

    public string GetFilePath(ImageData metadata)
    {
        return GetImagePath(Path.GetExtension(metadata.Filename)) + "/" + metadata.Filename;
    }
    
    private string GetImagePath(string extension)
    {
        // switch (extension)
        // {
        //     case ".png":
        //         return _configuration["BaseFilePath"] + "/png";
        //     case ".jpg":
        //         return _configuration["BaseFilePath"] + "/jpg";
        //     case ".pgm":
        //         return _configuration["BaseFilePath"] + "/pgm";
        //     default:
        //         throw new ArgumentException($"Unsupported file format: {extension}");
        // }
        return _configuration["BaseFilePath"] + "/";
    }
    
}