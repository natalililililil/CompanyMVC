using CompanyMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        //передаем userManager и signInManager для того, чтобы управлять пользователями в БД
        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        //пост версия Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //все валидно => правильно ввели данные
            if (ModelState.IsValid)
            {
                //пытаемся найти по имени(логину) пользователя, который указан в моделе
                IdentityUser user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    //если пользователь найден, то в принудительном порядке делает выход(SignOutAsync)
                    await signInManager.SignOutAsync();
                    //пытаемся войти по паролю
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                        return Redirect(returnUrl ?? "/"); //возвращает в ту точку, с которой пыталмя войти в админку
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
