using DoctorTrainer.Data;
using DoctorTrainer.Entity;

namespace DoctorTrainer.Repository;

public class ImageDataRepository
{
    private readonly ApplicationDbContext _context;

    public ImageDataRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<ImageData> FindAll() => 
        _context.ImagesData.ToList();

    public ImageData FindById(long id) => 
        _context.ImagesData.Where(d => d.Id == id).First();
    
    public ImageData FindByImageId(string imgId) => 
        _context.ImagesData.Where(d => d.ImgId.Equals(imgId)).First();
    
    public void Save(ImageData imageData)
    {
        _context.ImagesData.Add(imageData);
        _context.SaveChanges();
    }

    public void Delete(ImageData imageData)
    {
        _context.ImagesData.Remove(imageData);
        _context.SaveChanges();
    }
    
}