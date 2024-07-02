using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Application.Interfaces.Services;

namespace WebApp.SocialMedia.Controllers
{
    [Authorize]

    public class FriendController : Controller
    {
        protected readonly IFriendService _friendService;
        public FriendController(IFriendService _friendService)
        {
            this._friendService = _friendService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
