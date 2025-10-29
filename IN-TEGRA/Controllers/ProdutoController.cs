using IN_TEGRA.Models;
using IN_TEGRA.Repository;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Controllers
{
    public class ProdutoController : Controller
    {

        private IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.CadastrarProduto(produto);
            }
            return View();
        }
        public IActionResult Index()
        {
            return View(_produtoRepository.ObterTodosProdutos());
        }
        [HttpGet]
        public IActionResult AtualizarProduto(int IdProd)
        {
            return View(_produtoRepository.ObterProdutoPorId(IdProd));
        }
        [HttpPost]
        public IActionResult AtualizarProduto(Produto produto)
        {
            _produtoRepository.AtualizarProduto(produto);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DetalhesProduto(int IdProd)
        {
            return View(_produtoRepository.ObterProdutoPorId(IdProd));
        }
        [HttpPost]
        public IActionResult DetalhesProduto(Produto produto)
        {
            return RedirectToAction("Index");
        }
        public IActionResult ExcluirProduto(int IdProd)
        {
            _produtoRepository.ExcluirProduto(IdProd);
            return RedirectToAction("Index");
        }
    }
}
