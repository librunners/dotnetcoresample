using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BasicKnowledge.Models;
using Microsoft.Extensions.Options;
using BasicKnowledge.Middleware;

namespace BasicKnowledge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<AppOptions> _options;
        public HomeController(IOptions<AppOptions> options)
        {
            _options = options;
        }
        public IActionResult Index()
        {
            ViewBag.Option = _options.Value.Option;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
