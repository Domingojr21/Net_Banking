using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.ViewModels.User;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.NetBanking.Middlewares;
using NetBanking.Core.Application.Dtos.User;
using NetBanking.Core.Application.ViewModel.User;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.NetBanking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISavingAccountService _savingAccountService;

        public UserController(IUserService userService, ISavingAccountService savingAccountService)
        {
            _userService = userService;
            _savingAccountService = savingAccountService;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                if (userVm.Roles.Contains("Client"))
                {
                    return RedirectToRoute(new { controller = "Client", action = "Index" });
                }
                return RedirectToRoute(new { controller = "Admin", action = "Dashboard" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            var origin = Request.Headers["origin"];

            RegisterResponse response = new();

            if (ModelState.IsValid)
            {
                response = await _userService.RegisterAsync(vm, origin);
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }

            return View(vm);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordViewModel { Token = token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (vm.Email == null && vm.Password == null && vm.ConfirmPassword == null)
            {
                return View(vm);
            }
            
            ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

            public async Task<IActionResult> UpdateUser(string userId)
            {
                var user = await _userService.GetUserDTOAsync(userId);
                var editUser = new EditUserViewModel()
                {
                    Cedula = user.Cedula,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Username = user.UserName,
                };
            return View(editUser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(EditUserViewModel vm)
        {
            UserDTO value = new();


            value.Cedula = vm.Cedula;
            value.Phone = vm.Phone;
            value.Email = vm.Email;
            value.FirstName = vm.FirstName;
            value.LastName = vm.LastName;
            value.UserName = vm.Username;
            value.UserId = vm.UserId;

            await _userService.UpdateUserByUserId(value);
            return RedirectToRoute(new { controller = "Admin", action = "Dashboard" });
        }


        public async Task<IActionResult> UpdateClient(string userId)
            {
               var user = await _userService.GetUserDTOAsync(userId);
                var editUser = new EditUserViewModel()
                {
                    Cedula = user.Cedula,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Username = user.UserName,
                };
                return View(editUser);
            }

            [HttpPost]
            public async Task<IActionResult> UpdateClient(EditUserViewModel vm)
            {
                UserDTO value = new();
            
                value.Cedula= vm.Cedula;
                value.Phone= vm.Phone;
                value.Email= vm.Email;
                value.FirstName = vm.FirstName;
                value.LastName= vm.LastName;
                value.UserName = vm.Username;
                  value.UserId = vm.UserId;

            await _savingAccountService.AddAmountToSavingAccount(value.UserId, vm.InitialAmount);
              var user = await _userService.UpdateUserByUserId(value);

            return RedirectToRoute(new {controller = "Admin", action= "Dashboard" });
            }
    }
}

