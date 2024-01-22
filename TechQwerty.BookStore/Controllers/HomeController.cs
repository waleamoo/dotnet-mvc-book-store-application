using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechQwerty.BookStore.Models;
using TechQwerty.BookStore.Service;

namespace TechQwerty.BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IEmailService emailService)
        {
            _logger = logger;
            _userService = userService;
            _emailService = emailService;
        }

        public async Task<ViewResult> Index()
        {
            // send test email
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { "test@test.com" },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{ UserName }}", "Olawale")
                }
            };
            await _emailService.SendTestEmail(options);

            var userId = _userService.GetUserId();
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
