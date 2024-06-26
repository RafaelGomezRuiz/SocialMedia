using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Application.Dtos.Account;
using SocialMedia.Core.Application.Enums;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.ViewModels.Users;
using SocialMedia.Infraestructure.Identity.Entities;

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
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {

            }
            return View(new LoginViewModel());
        }
        public IActionResult SaveUser()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUser(SaveUserViewModel saveUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(saveUserViewModel);
            }
            var origin = Request.Headers["origin"];
            RegisterResponse response=await _userService.RegisterAsync(saveUserViewModel,origin);
            if (response.HasError)
            {
                saveUserViewModel.HasError = response.HasError;
                saveUserViewModel.ErrorDescription = response.ErrorDescription;
                return View(saveUserViewModel);
            }
            response.ProfilePhoto= UploadFile.Upload(saveUserViewModel.File,response.Id,UploadFileTypes.PROFILEPHOTO,false,response.ProfilePhoto);

            return RedirectToRoute(new { controller = "User", action = "index" });
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }
    }
}
