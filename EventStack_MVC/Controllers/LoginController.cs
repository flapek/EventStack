using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventStack_MVC.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace EventStack_MVC.Controllers
{
    public class LoginController : Controller
    {
        private HttpClient client;

        public LoginController()
        {
            client = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromQuery]string email, [FromQuery]string password)
        {
            var organizationList = new List<Organization>();
            var url = @"https://localhost:44382/api/Organization";
            var httpRensponse = await client.GetAsync(url);

            if (httpRensponse.IsSuccessStatusCode)
                organizationList = JsonConvert.DeserializeObject<List<Organization>>(await httpRensponse.Content.ReadAsStringAsync());
            else
                return Error();

            var authenticatedUser = organizationList.FirstOrDefault(user => user.Email == email && user.Password == password);

            if(authenticatedUser != null)
            {
                // To do AuthenticationToken
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
