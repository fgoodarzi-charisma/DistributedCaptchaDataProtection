using DistributedCaptcha.Models;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DistributedCaptcha.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateDNTCaptcha(ErrorMessage = "Please enter the security code as a number.")]
		public IActionResult TestCaptcha()
		{
			if (ModelState.IsValid)
			{
				return View(nameof(Index));
			}


            return View(nameof(Index));
        }
	}
}