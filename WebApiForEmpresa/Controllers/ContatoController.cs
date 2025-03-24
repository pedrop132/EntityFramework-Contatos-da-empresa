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
        //[Route("")] set this route to default being the first one
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

        // Options Views: Create, Delete, Edit
        //Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
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

        // Delete
        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/ContatosEmpresa/DeleteById/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                return RedirectToAction("Index");
            }
        }

        // Edit
        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/ContatosEmpresa/GetById/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var contato = JsonConvert.DeserializeObject<ContatoViewModel>(jsonResponse);
                return View(contato);
            }
            else
            {
                Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(ContatoViewModel contato)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/ContatosEmpresa/UpdateById/{contato.Id}", contato);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                ModelState.AddModelError(string.Empty, "Erro ao editar o contato.");
                return View(contato);
            }
        }
        // Options Views: Create, Delete, Edit

    }
}
