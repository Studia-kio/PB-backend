using System.Net;
using DoctorTrainer.DTO;
using DoctorTrainer.Entity;
using DoctorTrainer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrainer.Controllers;

[ApiController]
public class ExpertController : ControllerBase
{
    private readonly ImageDataService _imageDataService;

    public ExpertController(ImageDataService imageDataService)
    {
        _imageDataService = imageDataService;
    }

    [HttpGet]
    [Route("/api/expert/{imgId}")]
    [AllowAnonymous]
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
    [AllowAnonymous]
    public JsonResult AddImageData(ImageDataDto request)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Route("/api/expert/{imgId}")]
    [AllowAnonymous]
    public JsonResult UpdateImageData(long imgId, ImageDataDto request)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("/api/expert/{imgId}")]
    [AllowAnonymous]
    public JsonResult DeleteImageData(long imgId)
    {
        throw new NotImplementedException();
    }
}