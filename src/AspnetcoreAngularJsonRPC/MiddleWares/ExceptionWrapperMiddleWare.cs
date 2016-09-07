using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AspnetcoreAngularJsonRPC.MiddleWares
{
    public class ExceptionWrapperMiddleWare
    {
        private readonly RequestDelegate _next;
        public ExceptionWrapperMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {

                string s = e.InnerException.ToString();
            }
        }
    }
    public static class ExceptionWrapperMiddleWareExtensions
    {
        public static IApplicationBuilder UseExceptionWrapperMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionWrapperMiddleWare>();
        }
    }
}
