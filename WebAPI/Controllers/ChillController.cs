using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/chill")]
[ApiController]
public class ChillController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("adminText")]
    public string? AdminText()
    {
        return "This is Admin";
    }
}