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

        [HttpGet] // Route Contatos that connects to the API "api/ContatosEmpresa/GetAll"
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

        [HttpPost]
        public async Task<IActionResult> Create(ContatoViewModel contato)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/ContatosEmpresa/AddContato", contato);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                ModelState.AddModelError(string.Empty, "Erro ao criar o contato.");
                return View(contato);
            }
        }

        // Options Views: Create, Delete, Details, Edit
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        // Options Views: Create, Delete, Details, Edit

    }
}
