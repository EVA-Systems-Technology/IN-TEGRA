using IN_TEGRA.Libraries.Filtro;
using IN_TEGRA.Libraries.Login;
using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;
        private LoginCliente _loginCliente;
        private IPagamentoRepository _pagamentoRepository;
        private IItemPedidoRepository _itemPedidoRepository;
        private IPedidoRepository _pedidoRepository;
        private IProdutoRepository _produtoRepository;

        public ClienteController(IClienteRepository clienteRepository, LoginCliente logincliente, IPagamentoRepository pagamentoRepository, IItemPedidoRepository itempedidoRepository, IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _clienteRepository = clienteRepository;
            _loginCliente = logincliente;
            _pagamentoRepository = pagamentoRepository;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _itemPedidoRepository = itempedidoRepository;
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
        [ClienteAutorizacao]
        public IActionResult PainelCliente()
        {
            ViewBag.nomeCliente = _loginCliente.GetCliente().NomeCliente;
            ViewBag.cpf = _loginCliente.GetCliente().CpfCliente;
            ViewBag.email = _loginCliente.GetCliente().EmailCliente;
            ViewBag.telefone = _loginCliente.GetCliente().TelefoneCliente;
            ViewBag.DataNascimento = _loginCliente.GetCliente().DataNascimentoCliente;
            ViewBag.Senha = _loginCliente.GetCliente().SenhaCliente;

            return View();

        }

        public IActionResult Pedidos()
        {
            var IdCliente = _loginCliente.GetCliente().IdCliente;
            var pedidos = _pedidoRepository.ObterPedidosCliente(IdCliente);

            ViewBag.pedidos = pedidos;
            return View(pedidos);
        }

        public IActionResult DetalhesPedido(int IdPedido)
        {
            var itens = _pedidoRepository.ObterItensPedido(IdPedido);
            ViewBag.ItensPedido = itens;     
            
            return View(itens);
        }

        [HttpGet]
        public IActionResult AlterarDados(int IdCliente)
        {
            return View(_clienteRepository.ObterClientePorId(IdCliente));
        }
        [HttpPost]
        public IActionResult AlterarDados(Cliente cliente)
        {
            _clienteRepository.AtualizarCliente(cliente);

            return RedirectToAction("PainelCliente");
        }

    }
}
