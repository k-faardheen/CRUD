using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Registration.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Registration.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly HttpClient httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient(); 
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create() {
            return View(); 
        }

        [HttpGet("Home/Edit/{name}")]
        public async Task<IActionResult> Edit(string name)
        {
            
            var response = await httpClient.GetAsync($"https://localhost:7051/api/data/EditPerson/{name}");

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Models.Person>(jsonContent);

                Console.WriteLine(data?.Name);
                return View(data); 
            }

            return View();

            //return Ok($"https://localhost:7051/api/data/EditPerson/{name}");
        }

        [HttpGet("Home/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var response = await httpClient.GetAsync($"https://localhost:7051/api/data/GetPersonById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Models.Person>(jsonContent);

                Console.WriteLine(data?.Name);
                return View(data);
            }

            return View();

        }


    }
}

