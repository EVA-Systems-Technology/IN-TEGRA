using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IN_TEGRA.Libraries.Filtro
{
    public class ValidateHttpRefererAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string referer = context.HttpContext.Request.Headers["Referer"].ToString(); 
            if (string.IsNullOrEmpty(referer))
            {
                context.Result = new ContentResult()
                {
                    Content = "Acceso negado!",
                };
            }
            else
            {
                Uri uri = new Uri(referer);

                string hostReferer = uri.Host;
                string hostServidor = context.HttpContext.Request.Host.Host;

                if (hostReferer != hostServidor) 
                {
                    context.Result = new ContentResult()
                    {
                        Content = "Acceso Negado!",
                    };
                }



            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           // execução apos o controller
        }
    }
}
