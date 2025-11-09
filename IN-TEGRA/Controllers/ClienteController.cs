using IN_TEGRA.Libraries.Sessao.Login;
using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;
        private LoginCliente _loginCliente;

        public ClienteController(IClienteRepository clienteRepository, LoginCliente logincliente)
        {
            _clienteRepository = clienteRepository;
            _loginCliente = logincliente;
        }
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro([FromForm] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.CadastrarCliente(cliente);
                TempData["Sucesso"] = "Cadastro realizado com sucesso.";
                return RedirectToAction("Login", "Cliente");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente clientedb = _clienteRepository.Login(cliente.EmailCliente, cliente.SenhaCliente);
            if (clientedb.EmailCliente != null && clientedb.SenhaCliente != null)
            {
                _loginCliente.Login(clientedb);
                return RedirectToAction("PainelCliente", "Cliente");
            }
            TempData["Falha"] = "Cliente ou senha inválidos.";
            return View();
        }

        public IActionResult PainelCliente()
        {
            ViewBag.nomeCliente = _loginCliente.GetCliente().NomeCliente;
            ViewBag.cpf = _loginCliente.GetCliente().CpfCliente;
            ViewBag.email = _loginCliente.GetCliente().EmailCliente;
            ViewBag.telefone = _loginCliente.GetCliente().TelefoneCliente;

            return View();

        }



        //[Admin]
        public IActionResult ListaClientes()
        {
            return View(_clienteRepository.ObterTodosClientes());
        }
        //[Admin]
        public IActionResult ExcluirCliente(int idCliente)
        {
            _clienteRepository.ExcluirCliente(idCliente);
            return RedirectToAction("ListaClientes");
        }
        //[Admin]
        [HttpGet]
        public IActionResult DetalhesCliente(int IdCliente)
        {
            return View(_clienteRepository.ObterClientePorId(IdCliente));
        }
        //[Admin]
        [HttpPost]
        public IActionResult DetalhesCliente(Cliente cliente)
        {

            return RedirectToAction("ListaClientes");
        }
        //[Admin]
        [HttpGet]
        public IActionResult AtualizarCLiente(int IdCliente)
        {
            return View(_clienteRepository.ObterClientePorId(IdCliente));
        }
        //[Admin]
        [HttpPost]
        public IActionResult AtualizarCLiente(Cliente cliente)
        {
            _clienteRepository.AtualizarCliente(cliente);

            return RedirectToAction("Index");
        }
    }
}
