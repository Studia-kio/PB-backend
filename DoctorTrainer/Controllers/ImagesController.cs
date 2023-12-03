using DoctorTrainer.DTO;
using DoctorTrainer.Entity;
using DoctorTrainer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrainer.Controllers;

[ApiController]
public class ImagesController : ControllerBase
{
    private readonly ImageService _imageService;
    private readonly ImageAnalysisService _imageAnalysisService;
    private ImageDataService _imageDataService;

    public ImagesController(
        ImageService imageService,
        ImageAnalysisService imageAnalysisService,
        ImageDataService imageDataService)
    {
        _imageService = imageService;
        _imageAnalysisService = imageAnalysisService;
        _imageDataService = imageDataService;
    }

    [HttpGet]
    [Route("/api/images/{imgId}")]
    [Authorize]
    public async Task<IActionResult> GetImage(string imgId)
    {
        byte[] b = await _imageService.GetImageBytes(imgId);
        return File(b, "image/jpeg");
    }

    [HttpPost]
    [Route("/api/images")]
    [Authorize(Roles = "Expert,Admin")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Given file is not present.");
        }
        var memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);
        string imgId = await _imageService.AddImage(memoryStream.ToArray(), file.FileName);
        ImageData data = new ImageData()
        {
            ImgId = imgId,
            Filename = imgId + Path.GetExtension(file.FileName),
            MarkedRegions = new List<Circle>()
        };
        _imageDataService.SaveImageData(data);
        
        return Ok(imgId);
    }

    [HttpDelete]
    [Route("/api/images/{imgId}")]
    [Authorize(Roles = "Expert,Admin")]
    public IActionResult DeleteImage(string imgId)
    {
        if (_imageAnalysisService.IsInTraining(imgId))
        {
            return Conflict($"Image with {imgId} cannot be deleted - currently used in training.");
        }
        _imageService.RemoveImage(imgId);
        return Accepted();
    }
    
}
