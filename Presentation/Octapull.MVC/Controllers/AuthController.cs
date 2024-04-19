using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Octapull.Domain.Identity;
using Octapull.MVC.ViewModels;

namespace Octapull.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            var loginViewModel = new AuthLoginViewModel();

            return View(loginViewModel);
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            var registerViewModel = new AuthRegisterViewModel();

            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AuthRegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                //var validator = new AuthRegisterViewModelValidator();
                //var validationResult = validator.Validate(registerViewModel);

                //foreach (var error in validationResult.Errors)
                //{
                //    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                //    _toastNotification.AddErrorToastMessage(error.ErrorMessage);
                //}

                return View(registerViewModel);
            }

            var userId = Guid.NewGuid();

            var user = new User()
            {
                Id = userId,
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.Name,
                SurName = registerViewModel.Surname,
                PhoneNumber = registerViewModel.Phone,
                BirthDate = registerViewModel.BirthDate.Value.ToUniversalTime(),
                CreatedOn = DateTimeOffset.UtcNow,
                CreatedByUserId = userId,
            };

            var identityResult = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                    //_toastNotification.AddErrorToastMessage(error.Description);
                }

                return View(registerViewModel);
            }

            //_toastNotification.AddSuccessToastMessage("You've successfully registered to the application.");

            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(AuthLoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                //var validator = new AuthLoginViewModelValidator();
                //var validationResult = validator.Validate(loginViewModel);

                //foreach (var error in validationResult.Errors)
                //{
                //    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                //    _toastNotification.AddErrorToastMessage(error.ErrorMessage);
                //}

                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user is null)
            {

                //_toastNotification.AddErrorToastMessage("Your email or password is incorrect.");


                return View(loginViewModel);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);

            if (!loginResult.Succeeded)
            {
                //_toastNotification.AddErrorToastMessage("Your email or password is incorrect.");

                return View(loginViewModel);
            }

            //_toastNotification.AddSuccessToastMessage($"Welcome {user.UserName}!");

            return RedirectToAction("Index", controllerName: "Home");
        }

        [HttpGet]
        public async Task<IActionResult> SignOutAsync()
        {
            await _signInManager.SignOutAsync();

            //_toastNotification.AddSuccessToastMessage("Successfully signed out!");

            return RedirectToAction("Login", controllerName: "Auth");
        }


    }
}
