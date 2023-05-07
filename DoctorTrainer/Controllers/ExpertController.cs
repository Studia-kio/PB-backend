using System.Net;
using DoctorTrainer.DTO;
using DoctorTrainer.Entity;
using DoctorTrainer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrainer.Controllers;

[ApiController]
//[Authorize(Roles = "Admin,Expert")]
public class ExpertController : ControllerBase
{
    private readonly ImageDataService _imageDataService;

    public ExpertController(ImageDataService imageDataService)
    {
        _imageDataService = imageDataService;
    }

    [HttpGet]
    [Route("/api/expert/{imgId}")]
    public JsonResult GetImageData(long imgId)
    {
        ImageData? imageData = _imageDataService.FindImageData(imgId);
        if (imageData == null)
        {
            return new JsonResult(HttpStatusCode.NotFound);
        }

        ImageDataDto response = ImageDataDto.EntityToDtoMapper(imageData);
        return new JsonResult(response);
    }

    [HttpPost]
    [Route("/api/expert")]
    public JsonResult AddImageData(ImageDataDto request)
    {
        ImageData data = ImageDataDto.DtoToEntityMapper(request);
        _imageDataService.SaveImageData(data);
        return new JsonResult(HttpStatusCode.OK);
    }

    [HttpDelete]
    [Route("/api/expert/{imgId}")]
    public JsonResult DeleteImageData(long imgId)
    {
        ImageData? data = _imageDataService.FindImageData(imgId);
        if (data == null)
        {
            return new JsonResult(HttpStatusCode.NotFound);
        }

        _imageDataService.DeleteImageData(imgId);
        return new JsonResult(HttpStatusCode.OK);
    }
    
}