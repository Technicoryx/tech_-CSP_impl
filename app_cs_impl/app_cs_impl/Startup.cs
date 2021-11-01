using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using csp

namespace app_cs_impl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "app_cs_impl", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "app_cs_impl v1"));
            } 

            //Hard code way to append the Content-Security-Policy with the mNet WebAPI
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add(
                    "Content-Security-Policy",
                    "script-src 'self'; " +
                    "style-src 'self'; " +
                    "img-src 'self'");

                await next();
            });
            app.UsecspMiddleware(builder =>
            {
                builder.Defaults
                       .AllowSelf();

                builder.Scripts
                       .AllowSelf()
                       .Allow("https://ajax.aspnetcdn.com");

                builder.Styles
                       .AllowSelf()
                       .Allow("https://ajax.aspnetcdn.com");

                builder.Fonts
                       .AllowSelf()
                       .Allow("https://ajax.aspnetcdn.com");

                builder.Images
                       .AllowSelf()
                       .Allow("https://media-www-asp.azureedge.net/");
            })

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
