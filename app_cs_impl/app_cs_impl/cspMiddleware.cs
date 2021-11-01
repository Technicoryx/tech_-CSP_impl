using app_cs_impl;
using csp_model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_cs_impl
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class cspMiddleware
    {
        private const string HEADER = "Content-Security-Policy";
        private readonly RequestDelegate next;
        private readonly CspOptions options;

        public cspMiddleware(
            RequestDelegate next, CspOptions options)
        {
            this.next = next;
            this.options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add(HEADER, GetHeaderValue());
            await this.next(context);
        }

        private string GetHeaderValue()
        {
            var value = "";
            value += GetDirective("default-src", this.options.Defaults);
            value += GetDirective("script-src", this.options.Scripts);
            value += GetDirective("style-src", this.options.Styles);
            value += GetDirective("img-src", this.options.Images);
            value += GetDirective("font-src", this.options.Fonts);
            value += GetDirective("media-src", this.options.Media);
            return value;
        }

        private string GetDirective(string directive, List<string> sources)
            => sources.Count > 0 ? $"{directive} {string.Join(" ", sources)}; " : "";
    }
}

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class cspMiddlewareExtensions
    {
    public static IApplicationBuilder UsecspMiddleware(
      this IApplicationBuilder app, Action<CspOptionsBuilder> builder)
    {
        var newBuilder = new CspOptionsBuilder();
        builder(newBuilder);

        var options = newBuilder.Build();
        return app.UseMiddleware<cspMiddleware>(options);
    }
}

