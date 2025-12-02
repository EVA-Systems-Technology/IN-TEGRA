using IN_TEGRA.Libraries.Filtro;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao]
    public class PedidosController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidosController(IPedidoRepository pedidoRepository, IEnderecoRepository enderecoRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _enderecoRepository = enderecoRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index()
        {
            var pedidos = _pedidoRepository.ObterTodosPedidos();
            var clientes = new List<String>();
            foreach(var pedido in pedidos)
            {
                var cliente = _clienteRepository.ObterClientePorId(pedido.IdCli);
                var endereco = _enderecoRepository.ObterEnderecoPorId(pedido.IdEndereco);
                ViewBag.endereco = endereco;
                clientes.Add(cliente.NomeCliente);
            }

            ViewBag.cliente = clientes;
            return View(pedidos);
        }
        [HttpGet]
        public IActionResult DetalhesPedidos(int IdPedido)
        {
            var pedido = _pedidoRepository.ObterPedidoPorId(IdPedido);
            var cliente = _clienteRepository.ObterClientePorId(pedido.IdCli);
            ViewBag.Cliente = cliente;
            ViewBag.Endereco = _enderecoRepository.ObterEnderecoPorId(pedido.IdEndereco);
            ViewBag.pedido = pedido;

            var itens = _pedidoRepository.ObterItensPedido(IdPedido);

            var imagens = new List<string>();

            foreach(var item in itens)
            {
                var produto = _produtoRepository.ObterProdutoPorId(item.IdProduto);
                item.NomeProduto = produto.NomeProduto;
                imagens.Add(produto.ImagemProduto);
            }
            ViewBag.Imagem = imagens;

            return View(itens);
        }

        public IActionResult ExcluirPedido(int IdPedido)
        {
            _pedidoRepository.ExcluirPedido(IdPedido);
            return RedirectToAction("Index");
            
        }
    }
}
