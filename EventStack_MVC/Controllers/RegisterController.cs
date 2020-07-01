using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventStack_MVC.Models;
using System.Threading.Tasks;

namespace EventStack_MVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;

        public RegisterController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        public Task<IActionResult> CreateAccount([FromQuery]string email, [FromQuery]string password)
        {
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
