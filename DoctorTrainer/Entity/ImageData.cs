using System.ComponentModel.DataAnnotations.Schema;
using DoctorTrainer.DTO;
using Newtonsoft.Json;

namespace DoctorTrainer.Entity;

public class ImageData
{
    public long Id { get; set; }
    public string ImgId { get; set; }
    
    public string Filename { get; set; }

    [NotMapped]
    public Dictionary<string, string> MedicalParams
    {
        get => JsonConvert.DeserializeObject<Dictionary<string, string>>(MedicalParamsJson);
        set => MedicalParamsJson = JsonConvert.SerializeObject(value);
    }

    [Column("MedicalParams", TypeName = "TEXT")] // Use TEXT for storing JSON as a string
    public string MedicalParamsJson { get; set; }
    
    [NotMapped]
    public Category? Category
    {
        get => JsonConvert.DeserializeObject<Category>(CategoryJson);
        set => CategoryJson = JsonConvert.SerializeObject(value);
    }

    [Column("Category", TypeName = "TEXT")] // Use TEXT for storing JSON as a string
    public string CategoryJson { get; set; }

    [NotMapped]
    public List<Circle> MarkedRegions
    {
        get => JsonConvert.DeserializeObject<List<Circle>>(MarkedRegionsJson);
        set => MarkedRegionsJson = JsonConvert.SerializeObject(value);
    }

    [Column("MarkedRegions", TypeName = "TEXT")] // Use TEXT for storing JSON as a string
    public string MarkedRegionsJson { get; set; }
    
    public ImageData()
    {
        MedicalParams = new Dictionary<string, string>();
        MarkedRegions = new List<Circle>();
        Category = new Category()
        {
            Index = 0,
            Organ = Organ.Unknown,
            Type = ImageType.Unknown
        };
    }
    
    public ImageData(string imgId, string filename, Category category)
    {
        ImgId = imgId;
        Category = category;
        MedicalParams = new Dictionary<string, string>();
        MarkedRegions = new List<Circle>();
        CategoryJson = "";
    }
    
    public ImageData(string imgId, string filename, Category category, Dictionary<string, string> medicalParams, List<Circle> markedRegions)
    {
        ImgId = imgId;
        Category = category;
        MedicalParams = medicalParams;
        MarkedRegions = markedRegions;
        CategoryJson = "";
    }
    
}