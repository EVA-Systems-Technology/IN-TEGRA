using Microsoft.AspNetCore.Mvc;
using IN_TEGRA.Repository.Contract;
using IN_TEGRA.Models;

namespace IN_TEGRA.Controllers
{
    public class EnderecoController : Controller
    {
        private IEnderecoRepository _enderecoRepository;
        public EnderecoController(IEnderecoRepository enderecoRepository) 
        {
            _enderecoRepository = enderecoRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm] Endereco endereco)
        { 
            _enderecoRepository.CadastrarEndereco(endereco);
            return RedirectToAction("FinalizarCompra", "Pagamento", new { IdEndereco = endereco.IdEndereco });
        }
    }
}
