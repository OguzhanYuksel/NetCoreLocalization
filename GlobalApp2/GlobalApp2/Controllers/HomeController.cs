using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalApp2.Models;
using GlobalApp2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GlobalApp2.Controllers
{
    public class HomeController : Controller
    {
        IHomeService _homeService;
        IStringLocalizer _localizer;
        public HomeController(IHomeService homeService, IStringLocalizer localizer)
        {
            _homeService = homeService;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About(string ServiceName = "About")
        {
            AboutViewModel model = new AboutViewModel();
            _localizer = _homeService.DetectService(ServiceName);
            model.company = _localizer["company"];
            return View(model);
        }
    }
}