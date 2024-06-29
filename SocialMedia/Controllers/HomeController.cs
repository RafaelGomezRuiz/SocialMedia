using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Application.Enums;
using SocialMedia.Core.Application.Interfaces.Repositories;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.ViewModels.Posts;
using SocialMedia.Models;
using System.Diagnostics;

namespace SocialMedia.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger, IPostService _postService)
        {
            _logger = logger;
            this._postService = _postService;
        }

        public async Task<IActionResult> Index()
        {
            PostViewModelFilter from = new()
            {
                FromUserOrFriends = PostsFrom.USER
            };
            IEnumerable<PostViewModel> postsList = await _postService.GetAllWithIncludeWithFilter(from);
            ViewBag.Posts = postsList;
            return View(new SavePostViewModel());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
