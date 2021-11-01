using csp_model;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csp_middle_ware
{
    public static class CspMiddlewareExtensions
    {
        public static IApplicationBuilder UseCsp(
            this IApplicationBuilder app, Action<CspOptionsBuilder> builder)
        {
            var newBuilder = new CspOptionsBuilder();
            builder(newBuilder);

            var options = newBuilder.Build();
            return app.UseMiddleware<CspMiddleware>(options);
        }
    }
}
