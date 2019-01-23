using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicKnowledge.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BasicKnowledge
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        private readonly ILoggerFactory _loggerFactory;
        public Startup(
            IConfiguration configuration,
            IHostingEnvironment environment,
            ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _environment = environment;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();
            if (_environment.IsDevelopment())
            {
                logger.LogInformation("Development Environment");
            }
            else
            {
                logger.LogInformation($"Environment {_environment.EnvironmentName}");
            }
            logger.LogInformation($"ApplicationName {_environment.ApplicationName}");
            logger.LogInformation($"ContentRootPath {_environment.ContentRootPath }");
            logger.LogInformation($"WebRootPath {_environment.WebRootPath}");
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
/*
 * 请求管道中的每个中间件组件负责调用管道中的下一个组件，或在适当情况下使链发生短路。 
 * 如果中间件链中未发生短路，则每个中间件都有第二次机会在将请求发送到客户端前处理该请求。
*/
