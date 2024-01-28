
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Interfaces.Services;

namespace NetBanking.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductServices _prodcutServices;

        public ClientController(IUserService userService, IProductServices prodcutServices)
        {
            _userService = userService;
            _prodcutServices = prodcutServices;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _prodcutServices.GetAllProducts();
            return View(product);
        }


    }
}
