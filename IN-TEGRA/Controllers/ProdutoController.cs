using IN_TEGRA.Libraries.Login;
using IN_TEGRA.Models;
using IN_TEGRA.Repository;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Controllers
{
    public class ProdutoController : Controller
    {

        private IProdutoRepository _produtoRepository;
        private LoginCliente _loginCliente;

        public ProdutoController(IProdutoRepository produtoRepository, LoginCliente loginCliente)
        {
            _produtoRepository = produtoRepository;
            _loginCliente = loginCliente;
        }

        
        [HttpGet]
        public IActionResult Pesquisar(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return Json(new List<object>());
            }
            var produtos = _produtoRepository.PesquisarProduto(searchTerm);
            
            var resultado = produtos.Select(p => new {
                id = p.IdProd,
                nome = p.NomeProduto,
                preco = p.PrecoProduto.ToString("C2", new System.Globalization.CultureInfo("pt-BR")) 
            });
            
            return Json(resultado);
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
            var produtos = _produtoRepository.ObterTodosProdutos();
            var cliente = _loginCliente.GetCliente();

            if(cliente != null)
            {
                ViewBag.Cliente = cliente;
            }
            else
            {
                ViewBag.Cliente = null;
            }
            ViewBag.Categorias = produtos.Select(produto => produto.CategoriaProduto).Distinct().ToList();
            return View(produtos);
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
