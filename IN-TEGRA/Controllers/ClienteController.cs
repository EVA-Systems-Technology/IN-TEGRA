using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.CadastrarCliente(cliente);
            }
                return View();
        }
    }
}
