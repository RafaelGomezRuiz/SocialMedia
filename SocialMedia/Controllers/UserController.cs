using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Application.Dtos.Account;
using SocialMedia.Core.Application.Enums;
using SocialMedia.Core.Application.Helpers;
using SocialMedia.Core.Application.Interfaces.Services;
using SocialMedia.Core.Application.ViewModels.Users;

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
        public async Task<IActionResult> Index(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }
            AuthenticationResponse responseLogin= await _userService.LoginAsync(loginVm);
            if (responseLogin!=null && responseLogin.HasError!=true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", responseLogin);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(loginVm);
        }
        public async Task<IActionResult> SignOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "index" });
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
            response.ProfilePhoto= UploadFile.Upload(saveUserViewModel.File,UploadFileTypes.PROFILEPHOTO, response.Id);
            //falta hacerle un update a la foto

            return RedirectToRoute(new { controller = "User", action = "index" });
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        public async Task<IActionResult> ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordVm)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordVm);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(forgotPasswordVm, origin);
            if (response.HasError)
            {
                forgotPasswordVm.HasError = true;
                forgotPasswordVm.ErrorDescription=response.ErrorDescription;
                return View(forgotPasswordVm);
            }
            //mandar mensaje de que se envio
            return RedirectToRoute(new { controller = "user", action = "index" });
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordVm)
        {
            ResetPasswordResponse response = await _userService.ResetPasswordAsync(resetPasswordVm);
            if (response.HasError)
            {
                resetPasswordVm.ErrorDescription = response.ErrorDescription;
                return RedirectToAction("ForgotPassword",resetPasswordVm);
            }
            return RedirectToAction("Index");
        }

    }

}
