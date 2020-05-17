using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventStack_MVC.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

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

        public async Task<IActionResult> Index(string exampleDataFilter)
        {
            var eventList = new List<Event>();

            var url = "api/Event/GetAll";
            var httpRensponse = await client.GetAsync(url);

            if (httpRensponse.IsSuccessStatusCode)
            {
                var message = httpRensponse.Content.ReadAsStreamAsync().Result;
                eventList = await JsonSerializer.DeserializeAsync<List<Event>>(message);
            }

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
