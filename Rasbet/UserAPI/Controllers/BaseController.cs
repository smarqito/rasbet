using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
