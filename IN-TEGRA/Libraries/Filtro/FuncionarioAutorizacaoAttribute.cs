using IN_TEGRA.Libraries.Login;
using IN_TEGRA.Models;
using IN_TEGRA.Models.Constant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.InteropServices;

namespace IN_TEGRA.Libraries.Filtro
{
    public class FuncionarioAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private string _tipoFuncAutorizado;
        public FuncionarioAutorizacaoAttribute(string tipoFuncAutorizado = FuncionarioTipoConstant.Comum)
        {
            _tipoFuncAutorizado = tipoFuncAutorizado;
        }

        LoginFuncionario _loginFuncionario;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginFuncionario = (LoginFuncionario)context.HttpContext.RequestServices.GetService(typeof(LoginFuncionario));

            Funcionario funcionario = _loginFuncionario.GetFuncionario();

            if(funcionario == null) 
            {
                context.Result = new ContentResult()
                {
                    Content = "ERRO 401: Acesso Negado."
                };
            }
            else
            {
                if (funcionario.TipoFunc == FuncionarioTipoConstant.Comum && _tipoFuncAutorizado == FuncionarioTipoConstant.Gerente) 
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
