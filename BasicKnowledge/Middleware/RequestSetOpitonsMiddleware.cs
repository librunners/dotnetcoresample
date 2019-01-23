using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BasicKnowledge.Middleware
{
    public class RequestSetOpitonsMiddleware
    {
        private readonly RequestDelegate _next;
        private IOptions<AppOptions> _injectedOptions;

        public RequestSetOpitonsMiddleware(RequestDelegate next, IOptions<AppOptions> injectedOptions)
        {
            _next = next;
            _injectedOptions = injectedOptions;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("RequestSetOpitonsMiddleware Invoke");

            var option = httpContext.Request.Query["option"];
            if (!string.IsNullOrWhiteSpace(option))
            {
                _injectedOptions.Value.Option = WebUtility.HtmlDecode(option) + " RequestSetOpitonsMiddleware Invoke";
            }

            await _next(httpContext);
        }
    }


    public class AppOptions
    {
        public string Option { get; set; }
    }
}
