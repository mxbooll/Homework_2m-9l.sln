using Microsoft.AspNetCore.Mvc;

namespace Homework_2m_9l.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok();
    }
}