using IN_TEGRA.Libraries.Filtro;
using IN_TEGRA.Libraries.Login;
using IN_TEGRA.Models.Constant;
using IN_TEGRA.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace IN_TEGRA.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    public class HomeController : Controller
    {
        private IFuncionarioRepository _funcionarioRepository;
        private LoginFuncionario _loginFuncionario;

        public HomeController(IFuncionarioRepository funcionarioRepository, LoginFuncionario loginFuncionario)
        {
            _funcionarioRepository = funcionarioRepository;
            _loginFuncionario = loginFuncionario;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateHttpReferer]
        public IActionResult Login([FromForm] Models.Funcionario funcionario)
        {
            Models.Funcionario funcionariodb = _funcionarioRepository.Login(funcionario.EmailFuncionario, funcionario.SenhaFuncionario);

            if (funcionariodb.EmailFuncionario != null && funcionariodb.SenhaFuncionario != null && funcionariodb.TipoFunc != FuncionarioTipoConstant.Comum)
            {
                _loginFuncionario.Login(funcionariodb);

                return new RedirectResult(Url.Action(nameof(PainelGerente)));

            }
            if (funcionariodb.EmailFuncionario != null && funcionariodb.SenhaFuncionario != null && funcionariodb.TipoFunc != FuncionarioTipoConstant.Gerente)
            {
                _loginFuncionario.Login(funcionariodb);

                return new RedirectResult(Url.Action(nameof(PainelFuncionario)));

            }
            else
            {
                TempData["MSG"] = "ERRO: EMAIL OU SENHA INSERIDOS INCORRETAMENTE";
                return View();

            }
        }
        [FuncionarioAutorizacao]
        public IActionResult PainelFuncionario()
        {
            ViewBag.Nome = _loginFuncionario.GetFuncionario().NomeFuncionario;
            ViewBag.Tipo = _loginFuncionario.GetFuncionario().TipoFunc;
            ViewBag.Email = _loginFuncionario.GetFuncionario().EmailFuncionario;

            return View();
        }
        [FuncionarioAutorizacao]
        public IActionResult PainelGerente()
        {
            ViewBag.Nome = _loginFuncionario.GetFuncionario().NomeFuncionario;
            ViewBag.Tipo = _loginFuncionario.GetFuncionario().TipoFunc;
            ViewBag.Email = _loginFuncionario.GetFuncionario().EmailFuncionario;

            return View();
        }
       
        [FuncionarioAutorizacao]
        public IActionResult Logout()
        {
            _loginFuncionario.Logout();
            return new RedirectResult(Url.Action(nameof(Login)));
        }
    }
}
