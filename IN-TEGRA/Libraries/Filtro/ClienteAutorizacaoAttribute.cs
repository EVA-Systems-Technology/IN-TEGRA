using IN_TEGRA.Libraries.Login;
using IN_TEGRA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IN_TEGRA.Libraries.Filtro
{
    public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginCliente _loginCliente;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginCliente = (LoginCliente)context.HttpContext.RequestServices.GetService(typeof(LoginCliente));
            Cliente cliente = _loginCliente.GetCliente();

            if (cliente == null)
            {
                context.Result = new ContentResult()
                {
                    Content = "ERRO 401: Acesso Negado. Faça login para acessar esta pagina!!"
                };
            }
        }
    }
}
