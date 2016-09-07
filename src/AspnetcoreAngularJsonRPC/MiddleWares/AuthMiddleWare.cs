using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspnetcoreAngularJsonRPC.MiddleWares
{
    public class OLBAuthMiddleWare
    {
        private readonly RequestDelegate _next;

        public OLBAuthMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
        }
    }

    public static class OLBAuthMiddleWareExtensions
    {
        public static IApplicationBuilder UseOLBAuthMiddleWare(this IApplicationBuilder builder)
        {
            return builder.MapWhen(reqContext => reqContext.Request.Path.ToString().Contains("Home"),
                appBrach => {
                    appBrach.UseMiddleware<OLBAuthMiddleWare>();
                });
        }
    }
}
