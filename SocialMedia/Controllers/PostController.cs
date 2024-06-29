using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Application.Enums;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.ViewModels.Posts;

namespace WebApp.SocialMedia.Controllers
{
    public class PostController : Controller
    {
        protected readonly IPostService _postService;
        public PostController(IPostService _postService)
        {
            this._postService = _postService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Add()
        //{
        //    return View(new SavePostViewModel());
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SavePostViewModel postVm)
        {
            if (!ModelState.IsValid)
            {
                return View(postVm);
            }
            SavePostViewModel post =await _postService.AddAsync(postVm);
            if (post.VisualContentType== VisualContentType.PHOTO.ToString())
            {
                if (post != null)
                {
                    post.VisualContentPath = UploadFile.Upload(postVm.File, UploadFileTypes.POSTFILE, "", post.Id);
                    await _postService.Update(post, post.Id);
                }
            }
            return RedirectToRoute(new {controller="Home",action="Index"});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        { 
            await _postService.Delete(id);
            return RedirectToRoute(new {controller="Home",action="Index"});
        }
    }
}
