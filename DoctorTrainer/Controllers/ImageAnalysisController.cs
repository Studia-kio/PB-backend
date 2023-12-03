using DoctorTrainer.DTO.Metadata;
using DoctorTrainer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrainer.Controllers;

[ApiController]
public class ImageAnalysisController : ControllerBase
{
    private readonly ImageAnalysisService _imageAnalysisService;
    
    public ImageAnalysisController(ImageAnalysisService imageAnalysisService)
    {
        _imageAnalysisService = imageAnalysisService;
    }
    
    // analyzes given image, and returns marked regions
    [HttpPost]
    [Route("/api/analysis/predict/{id}")]
    [Authorize(Roles = "Expert,Admin")]
    public async Task<IActionResult> AnalyzeImage(string id)
    {
        ImageDataPredictionResponse? response = await _imageAnalysisService.Analyze(id);
        return Ok(response);
    }

    // triggers training of the AI model
    [HttpPost]
    [Route("/api/analysis/train")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> TrainModel()
    {
        _imageAnalysisService.ExportImageMetadata();
        await _imageAnalysisService.TrainModel();
        
        return Ok();
    }
    
}