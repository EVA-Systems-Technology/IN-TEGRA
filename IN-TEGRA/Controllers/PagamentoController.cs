using IN_TEGRA.Libraries.Filtro;
using IN_TEGRA.Libraries.Login;
using IN_TEGRA.Models;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Controllers
{
    [ClienteAutorizacao]
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
            var usuario = _loginCliente.GetCliente().IdCli;
                
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
                IdCli = usuario,
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
                    QtdItemPedido = item.QuantidadeProd,
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
                IdCli = pedido.IdCli,
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
                var produto = _produtoRepository.ObterProdutoPorId(item.IdProduto);
                item.NomeProduto = produto.NomeProduto;
            }
            // passando os dados para a visualização
            ViewBag.itens = itens;

            return View(pedido);
        }
    }
}
