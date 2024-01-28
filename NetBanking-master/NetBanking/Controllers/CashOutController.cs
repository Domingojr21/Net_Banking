using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.AvancedeEfectivo;
using NetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace NetBanking.Controllers
{
    [Authorize(Roles = "Client")]
    public class CashOutController : Controller
    {
        private readonly ICashOutService _avancedeEfectivo;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? user;

        public CashOutController(ICashOutService avancedeEfectivo, ISavingAccountService savingAccountService, ICreditCardService creditCardService, IHttpContextAccessor httpContextAccessor)
        {
            _avancedeEfectivo = avancedeEfectivo;
            _savingAccountService = savingAccountService;
            _creditCardService = creditCardService;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task< IActionResult> Index()
        {
            ViewBag.CreditCard = await _creditCardService.GetAllVMbyUserId();
            ViewBag.Account = await _savingAccountService.GetAllVMbyUserId();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SaveCashOutViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.CreditCard = await _creditCardService.GetAllVMbyUserId();
                ViewBag.Account = await _savingAccountService.GetAllVMbyUserId();
                return View(model);
            }
            var avance = await _avancedeEfectivo.MakeAvance(model);
            if (avance.HasError)
            {
                ViewBag.CreditCard = await _creditCardService.GetAllVMbyUserId();
                ViewBag.Account = await _savingAccountService.GetAllVMbyUserId();
                return View(model);
            }
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }
    }
}
