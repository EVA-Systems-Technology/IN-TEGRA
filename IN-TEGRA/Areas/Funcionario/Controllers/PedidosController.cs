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

        public PedidosController(IPedidoRepository pedidoRepository, IEnderecoRepository enderecoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _enderecoRepository = enderecoRepository;
        }

        public IActionResult Index()
        {
            var pedidos = _pedidoRepository.ObterTodosPedidos();
            foreach(var pedido in pedidos)
            {
                var endereco = _enderecoRepository.ObterEnderecoPorId(pedido.IdEndereco);
                ViewBag.endereco = endereco;
            }
            return View(pedidos);
        }

        public IActionResult DetalhesPedidos(int IdPedido)
        {
            var pedido = _pedidoRepository.ObterPedidoPorId(IdPedido);
            ViewBag.Endereco = _enderecoRepository.ObterEnderecoPorId(pedido.IdEndereco);

            return View(pedido);
        }

        public IActionResult ExcluirPedido(int IdPedido)
        {
            _pedidoRepository.ExcluirPedido(IdPedido);
            return RedirectToAction("Index");
            
        }
    }
}
