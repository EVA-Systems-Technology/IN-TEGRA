using IN_TEGRA.Models;
using IN_TEGRA.Repository;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IProdutoRepository _produtoRepository;
        public CarrinhoController(ICarrinhoRepository carrinhoRepository, IProdutoRepository produtoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _produtoRepository = produtoRepository;
        }


        public IActionResult Index()
        {
            var cartItems = _carrinhoRepository.ItensCarrinho(HttpContext.Session);

            foreach (var item in cartItems)
            {
                item.Produto = _produtoRepository.ObterProdutoPorId(item.ProdId);

                if (item.Produto == null)
                {
                    TempData["Falha"] = true;
                }

            }
            ViewBag.TotalCarrinho = _carrinhoRepository.TotalCarrinho(HttpContext.Session);
            return View(cartItems);
        }
        [HttpPost]
        public IActionResult AddCarrinho(int ProdutoId, int Quantidade = 1)
        {
            var produto = _produtoRepository.ObterProdutoPorId(ProdutoId);
            if (produto != null)
            {
                _carrinhoRepository.AddCarrinho(HttpContext.Session, produto, Quantidade);
                TempData["Sucesso"] = true;
                return RedirectToAction("Index", "Carrinho");
            }
            else
            {
                TempData["Falha"] = true;
                return RedirectToAction("Index", "Carrinho");
            }
        }
        [HttpPost]
        public IActionResult AlterarQuantidadeItem(int ProdutoId, int NovaQuantidade)
        {
            _carrinhoRepository.AlterarQuantidadeItem(HttpContext.Session, ProdutoId, NovaQuantidade);
            return RedirectToAction("Index", "Carrinho");
        }
        [HttpPost]
        public IActionResult LimparCarrinho()
        {
            _carrinhoRepository.LimparCarrinho(HttpContext.Session);
            return RedirectToAction("Index", "Carrinho");
        }
        [HttpPost]
        public IActionResult RemoverItem(int ProdutoId)
        {
            _carrinhoRepository.RemoverItem(HttpContext.Session, ProdutoId);
            return RedirectToAction("Index", "Carrinho");
        }
    }
}
