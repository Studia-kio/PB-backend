using System.IO;
using System.Threading.Tasks;

namespace DoctorTrainer.Service;

public class ImageService
{
    private const string ImgPath = @"C:\Users\theKonfyrm\RiderProjects\DoctorTrainer\DoctorTrainer\ImagesTmp\"; // todo: temporary, for testing reasons
    private const string ImgUrl = "https://localhost:7171/ImagesTmp/"; // todo: also temporary
    private static long _currentId = 0; // todo: temporary, for testing reasons

    public ImageService()
    {
    }

    public async Task<long> AddImage(byte[] imageData)
    {
        _currentId++;
        using (var stream = new FileStream(ImgPath + _currentId + ".jpg", FileMode.Create))
        {
            await stream.WriteAsync(imageData);
        }

        return _currentId;
    }

    public async Task<byte[]> GetImageBytes(long id)
    {
        // todo: pomyslec jak bedzie mozna pzekazywac te obrazki jak juz AI bedzie, bo moze to byc tricky troche...
        string url = ImgUrl + id + ".jpg";
        return File.ReadAllBytes(url);;
    }

    public void RemoveImage(long id)
    {
        string path = ImgPath + id + ".jpg";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}