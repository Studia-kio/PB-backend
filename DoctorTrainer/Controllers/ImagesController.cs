using System.Net;
using DoctorTrainer.DTO;
using DoctorTrainer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DoctorTrainer.Controllers;

[ApiController]
public class ImagesController : ControllerBase
{
    private readonly ImageService _imageService;

    public ImagesController(ImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet]
    [Route("/api/images/{imgId}")]
    [Authorize]
    public JsonResult GetImage(long imgId)
    {
        string url = _imageService.GetImageUrl(imgId);
        if (url.IsNullOrEmpty())
        {
            return new JsonResult(HttpStatusCode.NotFound);
        }
        return new JsonResult(url);
    }

    [HttpPost]
    [Route("/api/images")]
    [Authorize(Roles = "Expert,Admin")]
    public async Task<JsonResult> AddImage(ImageDto request)
    {
        if (request == null || request.Bytes == null)
        {
            return new JsonResult(HttpStatusCode.BadRequest);
        }

        try
        {
            await _imageService.AddImage(request.Bytes);
            return new JsonResult(HttpStatusCode.OK);
        }
        catch
        {
            return new JsonResult(HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete]
    [Route("/api/images/{imgId}")]
    [Authorize(Roles = "Expert,Admin")]
    public JsonResult DeleteImage(long imgId)
    {
        if (_imageService.GetImageUrl(imgId).IsNullOrEmpty())
        {
            return new JsonResult(HttpStatusCode.NotFound);
        }
        _imageService.RemoveImage(imgId);
        return new JsonResult(HttpStatusCode.Accepted);
    }
}