using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("✅ API is running on port 5000!");
    }
}

