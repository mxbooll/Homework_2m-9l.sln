using Microsoft.AspNetCore.Mvc;

namespace Homework_2m_9l.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public ActionResult CreateUser()
    {
        return Ok(new { message = "User created" });
    }

    [HttpGet("{userId}")]
    public ActionResult GetUser(int userId)
    {
        return Ok(new { userId = userId, name = "John Doe" });
    }

    [HttpDelete("{userId}")]
    public ActionResult DeleteUser(int userId)
    {
        return Ok(new { message = $"User {userId} deleted" });
    }

    [HttpPut("{userId}")]
    public ActionResult UpdateUser(int userId)
    {
        return Ok(new { message = $"User {userId} updated" });
    }
}