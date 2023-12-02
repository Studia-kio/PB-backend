using System.Collections.Generic;
using DoctorTrainer.DTO;

namespace DoctorTrainer.Entity;

public class ImageData
{
    public long ImgId { get; set; }
    
    // todo: path somewhere else
    public string FileName { get; set; }
    
    public Category Category { get; set; }
    public Dictionary<string, string> MedicalParams { get; set; }
    public List<Circle> MarkedRegions { get; set; }

    public ImageData(){}
    
    public ImageData(long imgId, Category category)
    {
        ImgId = imgId;
        Category = category;
        MedicalParams = new Dictionary<string, string>();
        MarkedRegions = new List<Circle>();
    }
    
    public ImageData(long imgId, Category category, Dictionary<string, string> medicalParams, List<Circle> markedRegions)
    {
        ImgId = imgId;
        Category = category;
        MedicalParams = medicalParams;
        MarkedRegions = markedRegions;
    }
    
}