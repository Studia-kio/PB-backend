using DoctorTrainer.DTO;
using DoctorTrainer.Entity;
using DoctorTrainer.Service;

namespace DoctorTrainerTests.Service;

public class ImageAnalysisServiceTest
{
    [Test]
    public void IsInTraining_True()
    {
        ImageData data = new ImageData()
        {
            Id = 0,
            Filename = "qwerty1234.png",
            ImgId = "qwerty1234",
            Category = new Category()
            {
                Index = 0,
                Organ = Organ.Breast,
                Type = ImageType.Mri
            }
        };
        ImageAnalysisService service = new ImageAnalysisService(new List<ImageData>(){data});

        bool isInTraining = service.IsInTraining(data);

        Assert.IsTrue(isInTraining);
    }
    
    [Test]
    public void IsInTraining_False_ListEmpty()
    {
        ImageData data = new ImageData()
        {
            Id = 0,
            Filename = "qwerty1234.png",
            ImgId = "qwerty1234",
            Category = new Category()
            {
                Index = 0,
                Organ = Organ.Breast,
                Type = ImageType.Mri
            }
        };
        ImageAnalysisService service = new ImageAnalysisService(new List<ImageData>());

        bool isInTraining = service.IsInTraining(data);

        Assert.IsFalse(isInTraining);
    } 
    
    [Test]
    public void IsInTraining_False_ListWithoutTargetData()
    {
        ImageData data = new ImageData()
        {
            Id = 0,
            Filename = "qwerty1234.png",
            ImgId = "qwerty1234",
            Category = new Category()
            {
                Index = 0,
                Organ = Organ.Breast,
                Type = ImageType.Mri
            }
        };
        ImageData data2 = new ImageData()
        {
            Id = 1,
            Filename = "qwerty123456.jpg",
            ImgId = "qwerty123456",
            Category = new Category()
            {
                Index = 0,
                Organ = Organ.Breast,
                Type = ImageType.Mri
            }
        };
        ImageAnalysisService service = new ImageAnalysisService(new List<ImageData>(){data2});

        bool isInTraining = service.IsInTraining(data);

        Assert.IsFalse(isInTraining);
    }
    
}