using IN_TEGRA.Libraries.Filtro;
using IN_TEGRA.Libraries.Login;
using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao]
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;
        private LoginCliente _loginCliente;

        public ClienteController(IClienteRepository clienteRepository, LoginCliente logincliente)
        {
            _clienteRepository = clienteRepository;
            _loginCliente = logincliente;
        }


        public IActionResult Index()
        {
            return View(_clienteRepository.ObterTodosClientes());
        }

        public IActionResult ExcluirCliente(int IdCli)
        {
            _clienteRepository.ExcluirCliente(IdCli);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DetalhesCliente(int IdCli)
        {
            return View(_clienteRepository.ObterClientePorId(IdCli));
        }

        [HttpPost]
        public IActionResult DetalhesCliente(Cliente cliente)
        {

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AtualizarCLiente(int IdCli)
        {
            return View(_clienteRepository.ObterClientePorId(IdCli));
        }

        [HttpPost]
        public IActionResult AtualizarCLiente(Cliente cliente)
        {
            _clienteRepository.AtualizarCliente(cliente);

            return RedirectToAction("Index");
        }

        public IActionResult Cadastrar()
        {
           return View();
        }

        public IActionResult Cadastrar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.CadastrarCliente(cliente);
                TempData["Sucesso"] = "Cadastro realizado com sucesso.";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
