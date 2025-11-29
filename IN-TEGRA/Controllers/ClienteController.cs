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
        private IEnderecoRepository _EnderecoRepository;
        private ILoginRepository _loginRepository;

        public ClienteController(IClienteRepository clienteRepository, LoginCliente logincliente, IPagamentoRepository pagamentoRepository, IItemPedidoRepository itempedidoRepository, IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository, ILoginRepository loginRepository, IEnderecoRepository enderecoRepository)
        {
            _clienteRepository = clienteRepository;
            _loginCliente = logincliente;
            _pagamentoRepository = pagamentoRepository;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _itemPedidoRepository = itempedidoRepository;
            _loginRepository = loginRepository;
            _EnderecoRepository = enderecoRepository;
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
            Cliente clientedb = _loginRepository.LoginComum(cliente.EmailCliente, cliente.SenhaCliente);
            if (clientedb.EmailCliente != null && clientedb.SenhaCliente != null)
            {
                _loginCliente.Login(clientedb);
                return RedirectToAction("Index", "Produto");
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
        [ClienteAutorizacao]
        public IActionResult Pedidos()
        {
            var IdCli = _loginCliente.GetCliente().IdCli;
            var pedidos = _pedidoRepository.ObterPedidosCliente(IdCli);

            ViewBag.NomeCli = _loginCliente.GetCliente().NomeCliente;
            ViewBag.pedidos = pedidos;
            return View(pedidos);
        }
        [ClienteAutorizacao]
        [HttpGet]
        public IActionResult DetalhesPedido(int IdPedido)
        {
            var pedido = _pedidoRepository.ObterPedidoPorId(IdPedido);
            ViewBag.endereco = _EnderecoRepository.ObterEnderecoPorId(pedido.IdEndereco);
            ViewBag.pedido = _pedidoRepository.ObterPedidoPorId(IdPedido);
            ViewBag.NomeCli = _loginCliente.GetCliente().NomeCliente;
            var itens = _pedidoRepository.ObterItensPedido(IdPedido);

            var imagens = new List<string>();
            foreach (var item in itens)
            {
                var produto = _produtoRepository.ObterProdutoPorId(item.IdProduto);
                item.NomeProduto = produto.NomeProduto;
                imagens.Add(produto.ImagemProduto);
            }
            ViewBag.Imagem = imagens;

            return View(itens);

        }
        [ClienteAutorizacao]
        [HttpGet]
        public IActionResult AlterarDados()
        {
            var cliente = _loginCliente.GetCliente().IdCli;
            var identificacaocliente = _clienteRepository.ObterClientePorId(cliente);
            return View(identificacaocliente);
        }
        [ClienteAutorizacao]
        [HttpPost]
        public IActionResult AlterarDados(Cliente cliente)
        {
            _clienteRepository.AtualizarCliente(cliente);
            _loginCliente.Logout();
            return RedirectToAction("Login");
        }
        [ClienteAutorizacao]
        public IActionResult Logout() 
        {
            _loginCliente.Logout();
            return RedirectToAction("index", "Produto");
        }

        public IActionResult RecuperaSenha()
        {
            return View();
        }

        public IActionResult ExcluirPedido(int IdPedido)
        {
            _pedidoRepository.ExcluirPedido(IdPedido);
            return RedirectToAction("DetalhesPedido");

        }

    }
}
