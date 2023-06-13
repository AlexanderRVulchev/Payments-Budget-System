using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsBudgetSystem.Controllers
{
    using Core.Contracts;
    using Core.Models.User;
    using Data.Entities;

    using static Common.RoleNames;
    using static Common.ValidationErrors.General;

    public class UserController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private IUserService userService;

        public UserController(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            RoleManager<IdentityRole> _roleManager,
            IUserService _userService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            userService = _userService;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            var primaryUsersNames = await userService
                .GetPrimaryNamesAsync();

            RegisterViewModel model = new()
            {
                PrimaryInstitutionName = primaryUsersNames.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var primaryUsersIdsAndNames = await userService.GetPrimaryIdsAndNamesAsync();

            if (!ModelState.IsValid)
            {
                model.PrimaryInstitutionName = primaryUsersIdsAndNames
                    .Select(x => x.Value)
                    .ToList();

                return View(model);
            }


            if (model.InputForPrimary < 0 || model.InputForPrimary >= primaryUsersIdsAndNames.Count())
            {
                ModelState.AddModelError("", InvalidPrimaryNumberForRegister);

                var primaryUsersNames = await userService
                    .GetPrimaryNamesAsync();

                model.PrimaryInstitutionName = primaryUsersNames.ToList();

                return View(model);
            }

            User user;
            IdentityResult result;
            if (model.InputForPrimary == 0)
            {
                user = new()
                {
                    UserName = model.UserName,
                    IsPrimary = true,
                    Name = model.Name
                };
                
                result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, PrimaryRoleName);

                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                user = new()
                {
                    UserName = model.UserName,
                    IsPrimary = false,
                    Name = model.Name
                };

                result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SecondaryRoleName);

                    string primaryId = primaryUsersIdsAndNames
                        .Skip(model.InputForPrimary - 1)
                        .Select(p => p.Key)
                        .First();

                    await userService.RelateSecondaryToPrimaryUserAsync(primaryId, user.Id);

                    return RedirectToAction(nameof(Login));
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            model.PrimaryInstitutionName = primaryUsersIdsAndNames
                .Select(x => x.Value)
                .ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            LoginViewModel model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ModelState.AddModelError(string.Empty, "Login failed");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool UserIsLoggedIn()
            => User?.Identity?.IsAuthenticated ?? false;
    }
}
