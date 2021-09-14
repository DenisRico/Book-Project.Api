using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondProject.Api.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecondProject.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataService _dataService;
        public HomeController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("data")]
        public async Task<IActionResult> Index()
        {
            string auth = Request.Headers["Authorization"].ToString();
            var books = await _dataService.GetBooksAsync(auth);
            string id = HttpContext.User.FindFirstValue("id");
            string email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            string userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            return Ok(books);
            //return new JsonResult(new {
            //gdd="fdfd",
            //fdfd="dvdf"
            //});
        }
    }
}
