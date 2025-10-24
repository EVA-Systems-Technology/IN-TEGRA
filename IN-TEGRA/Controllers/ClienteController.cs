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

        public IActionResult ListaClientes()
        {
            return View(_clienteRepository.ObterTodosClientes());
        }

        public IActionResult ExcluirCliente(int idCliente)
        {
            _clienteRepository.ExcluirCliente(idCliente);
            return RedirectToAction("ListaClientes");
        }

        [HttpGet]
        public IActionResult DetalhesCliente(int IdCliente)
        {
            return View(_clienteRepository.ObterClientePorId(IdCliente));
        }
        [HttpPost]
        public IActionResult DetalhesCliente(Cliente cliente)
        {

            return RedirectToAction("ListaClientes");
        }
        [HttpGet]
        public IActionResult AtualizarCLiente(int IdCliente)
        {
            return View(_clienteRepository.ObterClientePorId(IdCliente));
        }
        [HttpPost]
        public IActionResult AtualizarCLiente(Cliente cliente)
        {
            _clienteRepository.AtualizarCliente(cliente);

            return RedirectToAction("ListaClientes");
        }
    }
}
