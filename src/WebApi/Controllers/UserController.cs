using Homework_2m_9l.Data.Repository.Users;
using Homework_2m_9l.Entities.Users;
using Microsoft.AspNetCore.Mvc;

namespace Homework_2m_9l.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await userRepository.AddAsync(user);
        return Ok(new {message = "User created"});
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<User>> GetUser(Guid userId)
    {
        var user = await userRepository.GetAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult> DeleteUser(Guid userId)
    {
        try
        {
            await userRepository.DeleteAsync(userId);
            return Ok(new { message = $"User {userId} deleted" });
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpPut("{userId:guid}")]
    public async Task<ActionResult> UpdateUser(Guid userId, [FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (userId != user.Id)
        {
            return BadRequest("User ID in route doesn't match body");
        }

        try
        {
            await userRepository.UpdateAsync(user);
            return Ok(new { message = $"User {userId} updated" });
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
}