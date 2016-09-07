
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MediatR;
using AspnetcoreAngularJsonRPC.Controllers;
using System.IO;

namespace AspnetcoreAngularJsonRPC
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddMediatR(typeof(Startup));
            //services.AddTransient<IMediator, Mediator>();
            services.AddTransient<IJsonRpc, JsonRpc>();
            services.AddTransient<IJsonProcessor, MediatedJsonProcessor>();
            //services.AddTransient<IMediator, Mediator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // app.UseExceptionWrapperMiddleWare();
            //  app.UseAuditLoggingMiddleWare();
            //app.UseOLBAuthMiddleWare();

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404
                    && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/Index.html";
                    await next();
                }
            });
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
