using EmptyWebApplicationASP.NET.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.Routing;

namespace EmptyWebApplicationASP.NET
{
    public class AdminRoute : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public async Task RouteAsync(RouteContext context)
        {
            string url = context.HttpContext.Request.Path.Value.TrimEnd('/');
            if(url.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
            {
                context.Handler = async ctx =>
                {
                    ctx.Response.ContentType = "text/html; charset=utf-8";
                    await ctx.Response.WriteAsync("Hello ADMIN!");
                };
            }
            await Task.CompletedTask;
        }
    }
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.Routes.Add(new AdminRoute());

            routeBuilder.MapRoute("{controller}/{action}",
                async context =>
                {
                    await context.Response.WriteAsync("Hello my litle bro");
                });
            app.UseRouter(routeBuilder.Build());

            app.Run(async context => await context.Response.WriteAsync("Hello world"));
        }

        private async Task Handler (HttpContext context)
        {

        }
    }


}
