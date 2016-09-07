using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace AspnetcoreAngularJsonRPC.MiddleWares
{
    public class AuditLoggingMiddleWare
    {
        private readonly RequestDelegate _next;
        public AuditLoggingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            //loging request
            var request = new StreamReader(context.Request.Body).ReadToEnd().ToString();

            await _next.Invoke(context).ContinueWith((comState) => {
                //logging response
                // var response = new StreamReader(context.Response.Body).ReadToEnd().ToString();
            });
        }
    }
    public static class AuditLoggingMiddleWareExtensions
    {
        public static IApplicationBuilder UseAuditLoggingMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditLoggingMiddleWare>();
        }
    }
}
