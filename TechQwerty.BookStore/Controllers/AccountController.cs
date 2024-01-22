using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.CodeDom;
using TechQwerty.BookStore.Models;
using TechQwerty.BookStore.Repository;

namespace TechQwerty.BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Route("signup")]
        public IActionResult SignUp()
        {
            ViewBag.IsSuccess = false;
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public IActionResult SignUp(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = _accountRepository.CreateUserAsync(userModel);
                //if (!result.Status.Equals("Faulted"))
                if (!result.Result.Succeeded)
                {
                    foreach (var errorMessage in result.Result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    ViewBag.IsSuccess = false;
                    return View(userModel);
                }
                ModelState.Clear();
            }
            ModelState.Clear();
            ViewBag.IsSuccess = true;
            return View(userModel);
        }

        [Route("login")]
        public ViewResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signInModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = _accountRepository.PasswordSignInAsync(signInModel);
                if (result.Result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "If an account exists, credentials does not match");
            }
            return View(signInModel);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                // if there are error 
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


    }
}
