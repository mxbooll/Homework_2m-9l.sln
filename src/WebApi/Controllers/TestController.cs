using Microsoft.AspNetCore.Mvc;

namespace Homework_2m_9l.Controllers;

[ApiController]
[Route("api")]
public class TestController : ControllerBase
{
    private readonly Random _random = new(42);

    [HttpGet("a")]
    public async Task<ActionResult<string>> Method1()
    {
        return await DoSomethingAsync("method1", 0, 300);
    }

    [HttpGet("b")]
    public async Task<ActionResult<string>> Method2()
    {
        return await DoSomethingAsync("method2", 200, 400);
    }
    
    [HttpGet("c")]
    public async Task<ActionResult<string>> Method3()
    {
        var timeout = _random.Next(0, 100);
        await Task.Delay(timeout);
        var error = _random.Next(10);
        throw new Exception($"Error {error}");
    }

    private async Task<string> DoSomethingAsync(string res, int from, int to)
    {
        var timeout = _random.Next(from, to);
        await Task.Delay(timeout);

        var error = _random.Next(10);
        if (error > 8)
            throw new Exception($"Error {error}");

        return $"{res}: {timeout}";
    }
}