using IN_TEGRA.Libraries.Filtro;
using IN_TEGRA.Libraries.Login;
using IN_TEGRA.Models;
using IN_TEGRA.Repository;
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
        private IPedidoRepository _pedidoRepository;
        private IEnderecoRepository _enderecoRepository;
        private IProdutoRepository _produtoRepository;

        public ClienteController(IClienteRepository clienteRepository, LoginCliente logincliente, IPedidoRepository pedidoRepository, IEnderecoRepository enderecoRepository, IProdutoRepository produtoRepository)
        {
            _clienteRepository = clienteRepository;
            _loginCliente = logincliente;
            _pedidoRepository = pedidoRepository;
            _enderecoRepository = enderecoRepository;
            _produtoRepository = produtoRepository;
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
            var cliente = _clienteRepository.ObterClientePorId(IdCli);
            //implementar os pedidos no cliente
            var pedidos = _pedidoRepository.ObterPedidosCliente(IdCli);
            foreach(var pedido in pedidos)
            {
                //implementar parte endereço no cliente
                ViewBag.Endereco = _enderecoRepository.ObterEnderecoPorId(pedido.IdEndereco);
                
            }
            ViewBag.Pedidos = pedidos;

            return View(cliente);
        }
        [HttpGet]
        public IActionResult DetalhesItensPedidoCliente(int IdPedido)
        {
            var pedido = _pedidoRepository.ObterPedidoPorId(IdPedido);
            ViewBag.cliente = _clienteRepository.ObterClientePorId(pedido.IdCli);
            ViewBag.endereco = _enderecoRepository.ObterEnderecoPorId(pedido.IdEndereco);
            ViewBag.pedido = _pedidoRepository.ObterPedidoPorId(IdPedido);
            var itens = _pedidoRepository.ObterItensPedido(IdPedido);

            var produtos = new List<Produto>();
            foreach (var item in itens)
            {
                var produto = _produtoRepository.ObterProdutoPorId(item.IdProduto);
                item.NomeProduto = produto.NomeProduto;
                produtos.Add(produto);
            }

            ViewBag.produtos = produtos as List<IN_TEGRA.Models.Produto>;


            return View(itens);
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
