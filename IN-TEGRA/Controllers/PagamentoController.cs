using IN_TEGRA.Libraries.Login;
using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Controllers
{
    public class PagamentoController : Controller
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IPagamentoRepository _pagamentoRepository;
        private LoginCliente _loginCliente;
        private IItemPedidoRepository _itemPedidoRepository;
        private IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PagamentoController(
            ICarrinhoRepository carrinhoRepository,
            IPagamentoRepository pagamentoRepository,
            IItemPedidoRepository itemPedidoRepository,
            IPedidoRepository pedidoRepository,
            IProdutoRepository produtoRepository,
            LoginCliente logincliente)
        {
            _carrinhoRepository = carrinhoRepository;
            _pagamentoRepository = pagamentoRepository;
            _itemPedidoRepository = itemPedidoRepository;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _loginCliente = logincliente;
        }

        [HttpPost]
        public IActionResult FinalizarCompra()
        {
            var usuario = _loginCliente.GetCliente().IdCliente;
            var itensCarrinho = _carrinhoRepository.ItensCarrinho(HttpContext.Session);


            // verifica se o carrinho esta vazio
            if (itensCarrinho == null) 
            {
                TempData["MSG"] = "Seu carrinho está vazio!!!";
                return RedirectToAction("Index", "Carrinho");
            }


            // criando novo pedido no sistema
            var pedido = new Pedido
            {
                IdCliente = usuario,
                FretePedido = 0,
                ValorPedido = (double)_carrinhoRepository.TotalCarrinho(HttpContext.Session),
                ConfirmacaoPedido = false

            };
            // cadastra o pedido e retorna o id do pedido cadastrado
            int IdPedido = _pedidoRepository.CadastrarPedido(pedido);

            // cadastrando os itens do pedido
            foreach (var item in itensCarrinho)
            {
                var itemPedido = new ItemPedido
                {
                    IdPedido = IdPedido,
                    IdProduto = item.ProdId,
                    QuantidadeItemPedido = item.QuantidadeProd,
                    ValorItemPedido = (double)item.PrecoProd
                };
                _itemPedidoRepository.CadastrarItemPedido(IdPedido, itemPedido);
            }

            // limpando o carrinho apos finalizar a compra
            _carrinhoRepository.LimparCarrinho(HttpContext.Session);


            return RedirectToAction("Pagamento", new { IdPedido });
        }


        [HttpGet]
        public IActionResult Pagamento(int IdPedido)
        {
            // pegando o pedido para ver na pagina de pagamento
            var pedido = _pedidoRepository.ObterPedidoPorId(IdPedido);
            // pegando os itens do pedido
            var itenspedido = _pedidoRepository.ObterItensPedido(IdPedido);

            foreach (var item in itenspedido) 
            {
                item.NomeProduto = item.NomeProduto;
                item.IdProduto = item.IdProduto;
                item.IdPedido = item.IdPedido;
                item.QuantidadeItemPedido = item.QuantidadeItemPedido;
                item.ValorItemPedido = item.ValorItemPedido;
            }

            ViewBag.itens = itenspedido;
            return View(pedido);

        }
        [HttpPost]

        public IActionResult FinalizarPagamento(int IdPedido, string TipoPagamento)
        {
            var pedido = _pedidoRepository.ObterPedidoPorId(IdPedido);
            // verificando se o pedido é nulo
            if (pedido == null)
            {
                return NotFound();
            }
            // criando novo pagamento no sistema
            var pagamento = new Pagamento
            {
                IdPedido = IdPedido,
                IdCliente = pedido.IdCliente,
                TipoPagamento = TipoPagamento,
                DataHoraPagamento = DateTime.Now,
                ValorPagamento = pedido.ValorPedido
            };
            // mandando os dados para o banco de dados
            _pagamentoRepository.RegistrarPagamento(pagamento);

            TempData["MSG"] = "Pagamento realizado com sucesso!!!";
            
            return RedirectToAction("DetalhesPagamento", new { idPedido = IdPedido });

        }
        [HttpGet]
        public IActionResult DetalhesPagamento(int IdPedido)
        {
            // pegando os dados do pedido pelo id
            var pedido = _pedidoRepository.ObterPedidoPorId(IdPedido);
            if (pedido == null)
            {
                return NotFound();
            }
            // pegando os itens do pedido pelo id do pedido
            var itens = _pedidoRepository.ObterItensPedido(IdPedido);
            // convertendo os dados para a visualização
            foreach(var item in itens) 
            {
                item.IdProduto = item.IdProduto;
                item.NomeProduto = item.NomeProduto;
                item.ValorItemPedido = item.ValorItemPedido;
                item.QuantidadeItemPedido = item.QuantidadeItemPedido;
            
            }
            // passando os dados para a visualização
            ViewBag.itens = itens;

            return View(pedido);
        }
    }
}
