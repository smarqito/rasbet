using Microsoft.AspNetCore.Mvc;


namespace BetAPI.Controllers;

public class BetController : Controller
{
    [HttpGet("{userId}")]
    public Task<IActionResult> Get()
    {
        throw new NotImplementedException();
    }

    [HttpPost("bet")]
    public Task<IActionResult> createBet()
    {
        throw new NotImplementedException();
    }

}
