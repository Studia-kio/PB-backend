using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrainer.Controllers;

[ApiController]
public class ExpertController
{
    [HttpGet]
    [Route("/api/helloWorld")]
    [AllowAnonymous]
    public JsonResult HelloWorld()
    {
        return new JsonResult("Hello world!");
    }
}