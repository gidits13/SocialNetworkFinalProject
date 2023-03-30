using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetworkFinalProject.Models;
using SocialNetworkFinalProject.ViewModels;

namespace SocialNetworkFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> _signInManager;

        public HomeController(ILogger<HomeController>  logger,SignInManager<User> signInManager )
        {
            _logger = logger;
            _signInManager = signInManager;
        }
        [Route("")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("MyPage", "AccountManager");
            else
                return View(new MainViewModel());
        }
    }
}
