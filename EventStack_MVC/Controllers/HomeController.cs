using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventStack_MVC.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace EventStack_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(string city, string distance, string category)
        {
            var eventList = new List<Event>();

            var url = @"https://localhost:44382/api/Event/GetAll";
            var httpRensponse = await client.GetAsync(url);

            if (httpRensponse.IsSuccessStatusCode)
                eventList = JsonConvert.DeserializeObject<List<Event>>(await httpRensponse.Content.ReadAsStringAsync());

            return View(eventList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
