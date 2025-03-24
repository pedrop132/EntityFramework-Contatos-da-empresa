using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiForEmpresa.Models;

namespace WebApiForEmpresa.Controllers
{
    public class ContatoController : Controller
    {
        private readonly HttpClient _client;

        public ContatoController(HttpClient httpClient)
        {
            _client = httpClient;
            _client.BaseAddress = new Uri("https://localhost:7158/api");
        }

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    List<ContatoViewModel> contatos = new List<ContatoViewModel>();

        //    HttpResponseMessage response = _client.GetAsync("ContatosEmpresa/GetAll").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        contatos = JsonConvert.DeserializeObject<List<ContatoViewModel>>(response.Content.ReadAsStringAsync().Result);
        //    }

        //    return View(contatos);
        //}
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ContatoViewModel> contatos = new List<ContatoViewModel>();

            HttpResponseMessage response = await _client.GetAsync("api/ContatosEmpresa/GetAll");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                contatos = JsonConvert.DeserializeObject<List<ContatoViewModel>>(jsonResponse);
            }
            else
            {
                Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                ModelState.AddModelError(string.Empty, "Erro ao carregar os contatos.");
            }

            return View(contatos);
        }

        //[HttpPost]


    }
}
