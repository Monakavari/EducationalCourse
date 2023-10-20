using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Infrastructure
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
