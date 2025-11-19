using IN_TEGRA.Libraries.Filtro;
using IN_TEGRA.Models;
using IN_TEGRA.Repository;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao]
    public class ProdutoController : Controller
    {
        private IProdutoRepository _IProdutoRepository;

        public ProdutoController(IProdutoRepository IprodutoRepository)
        {
            _IProdutoRepository = IprodutoRepository;
        }
        public IActionResult ListaProdutos()
        {
            return View(_IProdutoRepository.ObterTodosProdutos());
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
                TempData["MSG"] = "PRODUTO CADASTRADO COM SUCESSO!!";
                _IProdutoRepository.CadastrarProduto(produto);
            }
            else
            {
                TempData["MSG"] = "ERRO: PRODUTO NAO FOI CADASTRADO!!";
            }
            return RedirectToAction("Cadastro");
        }
        [HttpGet]
        public IActionResult AtualizarProduto(int IdProd)
        {
            return View(_IProdutoRepository.ObterProdutoPorId(IdProd));
        }
        [HttpPost]
        public IActionResult AtualizarProduto(Produto produto)
        {
            _IProdutoRepository.AtualizarProduto(produto);

            return RedirectToAction("ListaProdutos");
        }
        [HttpGet]
        public IActionResult DetalhesProduto(int IdProd)
        {
            return View(_IProdutoRepository.ObterProdutoPorId(IdProd));
        }
        [HttpPost]
        public IActionResult DetalhesProduto(Produto produto)
        {
            return RedirectToAction("ListaProdutos");
        }
        public IActionResult ExcluirProduto(int IdProd)
        {
            _IProdutoRepository.ExcluirProduto(IdProd);
            return RedirectToAction("ListaProdutos");
        }
    }
}
