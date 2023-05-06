namespace DoctorTrainer.Service;

public class ImageService
{
    private const string ImgPath = ""; // temporary, for testing reasons
    private static long _currentId = 0; // temporary, for testing reasons

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

    public string GetImageUrl(long id)
    {
        return ImgPath + id + ".jpg"; // todo: pomyslec jak bedzie mozna pzekazywac te obrazki jak juz AI bedzie, bo moze to byc tricky troche...
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