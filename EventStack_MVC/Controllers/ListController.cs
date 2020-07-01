using System.Reflection.Metadata;
using System;
using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventStack_MVC.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EventStack_MVC.Controllers
{
    public class ListController : Controller
    {
        private HttpClient client;

        public ListController()
        {
            client = new HttpClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public async Task<IActionResult> Index()
        {
            var eventList = new List<Event>();

            var url = @"http://localhost:44382/api/Event/GetAll";
            var httpRensponse = await client.GetAsync(url);

            if (httpRensponse.IsSuccessStatusCode)
                eventList = JsonConvert.DeserializeObject<List<Event>>(await httpRensponse.Content.ReadAsStringAsync());

            return View(eventList);
        }

        public IActionResult GetPhoto(byte[] photo)
        {;
            if (photo == null) Error();
            return File(photo, "image/png");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
