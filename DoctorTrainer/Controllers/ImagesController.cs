using System.Net;
using DoctorTrainer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    //[Authorize]
    public async Task<IActionResult> GetImage(long imgId)
    {
        byte[] b = await _imageService.GetImageBytes(imgId);
        return File(b, "image/jpeg");
    }

    [HttpPost]
    [Route("/api/images")]
    //[Authorize(Roles = "Expert,Admin")]
    [Consumes("multipart/form-data")]
    public async Task<JsonResult> AddImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return new JsonResult(HttpStatusCode.BadRequest);
        }
        try
        {
            var memoryStream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(memoryStream);
            long id = await _imageService.AddImage(memoryStream.ToArray());
            return new JsonResult(id);
        }
        catch
        {
            return new JsonResult(HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete]
    [Route("/api/images/{imgId}")]
    //[Authorize(Roles = "Expert,Admin")]
    public JsonResult DeleteImage(long imgId)
    {
        _imageService.RemoveImage(imgId);
        return new JsonResult(HttpStatusCode.Accepted);
    }
}