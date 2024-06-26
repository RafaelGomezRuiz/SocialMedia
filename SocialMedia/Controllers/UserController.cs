using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Application.Interfaces.Services;

namespace WebApp.SocialMedia.Controllers
{
    public class UserController : Controller
    {
        protected readonly IUserService _userService;

        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
