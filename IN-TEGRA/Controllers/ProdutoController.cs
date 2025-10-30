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
                TempData["Sucesso"] = true;
                _produtoRepository.CadastrarProduto(produto);
            }
            else
            {
                TempData["Falha"] = true;
            }
                return RedirectToAction("Cadastro");
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
