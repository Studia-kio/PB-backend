using DoctorTrainer.DTO;
using DoctorTrainer.Entity;
using DoctorTrainer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrainer.Controllers;

[ApiController]
public class MetadataController : ControllerBase
{
    private readonly ImageDataService _imageDataService;
    private readonly ImageAnalysisService _imageAnalysisService;

    public MetadataController(ImageDataService imageDataService, ImageAnalysisService imageAnalysisService)
    {
        _imageDataService = imageDataService;
        _imageAnalysisService = imageAnalysisService;
    }

    [HttpGet]
    [Route("/api/metadata/{imgId}")]
    [Authorize(Roles = "Admin,Expert")]
    public IActionResult GetImageData(string imgId)
    {
        ImageData? imageData = _imageDataService.FindDataByImageId(imgId);
        if (imageData == null)
        {
            return NotFound($"Metadata for image with the given id {imgId} not found.");
        }

        ImageDataDto response = ImageDataDto.EntityToDtoMapper(imageData);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("/api/metadata")]
    [Authorize(Roles = "Admin,Expert")]
    public IActionResult GetImageData()
    {
        List<ImageData> imageData = _imageDataService.FindAllImageData();
        List<ImageDataDto> response = imageData.Select(d => ImageDataDto.EntityToDtoMapper(d)).ToList();
        return Ok(response);
    }

    // data is added alongside the image, so this won't be needed in your everyday use cases
    // [HttpPost]
    // [Route("/api/metadata")]
    // [Authorize(Roles = "Admin")]
    // public IActionResult AddImageData(ImageDataDto request)
    // {
    //     ImageData? dataByImageId = _imageDataService.FindDataByImageId(request.ImageId);
    //     if (dataByImageId != null)
    //     {
    //         return Conflict($"Image with id {request.ImageId} already has assigned metadata.");
    //     }
    //
    //     ImageData data = ImageDataDto.DtoToEntityMapper(request);
    //     _imageDataService.SaveImageData(data);
    //     return Ok();
    // }

    // todo: mage metadata deletion alongside file deletion
    // [HttpDelete]
    // [Route("/api/metadata/{imgId}")]
    // [Authorize(Roles = "Admin")]
    // public IActionResult DeleteImageData(long imgId)
    // {
    //     ImageData? data = _imageDataService.FindDataByImageId(imgId);
    //     if (data == null)
    //     {
    //         return NotFound($"Metadata for image with the given id {imgId} not found.");
    //     }
    //
    //     if (_imageAnalysisService.IsInTraining(imgId))
    //     {
    //         return Conflict($"Image with {imgId} cannot be deleted - currently used in training.");
    //     }
    //     
    //     _imageDataService.DeleteImageData(imgId);
    //     return Ok();
    // }

    [HttpPut]
    [Route("/api/metadata/{imgId}")]
    [Authorize(Roles = "Admin,Expert")]
    public IActionResult UpdateImageData(string imgId, ImageDataDto request)
    {
        request.ImageId = imgId;
        ImageData? data = _imageDataService.FindDataByImageId(imgId);
        if (data == null)
        {
            return NotFound($"Metadata for image with the given id {imgId} not found.");
        }

        if (_imageAnalysisService.IsInTraining(imgId))
        {
            return Conflict($"Image with {imgId} cannot be deleted - currently used in training.");
        }
        
        data = ImageDataDto.DtoToEntityMapper(request);

        _imageDataService.UpdateImageData(imgId, data);
        return Ok();
    }
    
}
