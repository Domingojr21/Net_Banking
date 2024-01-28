using AutoMapper;
using Microsoft.AspNetCore.Http;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.Products;

namespace NetBanking.Core.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ICreditCardService _creditCardService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ILoanService _loanService;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserService _userService;

        public ProductServices(ICreditCardService creditCardService, 
            ISavingAccountService savingAccountService, 
            ILoanService loanService, IMapper mapper, 
            IHttpContextAccessor httpContextAccessor,
             ITransactionRepository transactionRepository,
             IUserService userService)
        {
            _transactionRepository = transactionRepository;
            _creditCardService = creditCardService;
            _savingAccountService = savingAccountService;
            _loanService = loanService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
        }

        public async Task<DashBoardStatitics> GetDashBoard()
        {
            var credictCardList = await _creditCardService.GetAllViewModel();
            var savingAccountList = await _savingAccountService.GetAllViewModel();
            var loanList = await _loanService.GetAllViewModel();
            var transactionsList = await _transactionRepository.GetAllAsync();
            var dayTransactions = transactionsList.Where(x => x.Date == DateTime.Today).ToList();
            var paymentList = transactionsList.Where(x => x.TransactionTypeId == 1 || x.TransactionTypeId == 2 || x.TransactionTypeId == 3 || x.TransactionTypeId == 4).ToList();
            var dayPayment = paymentList.Where(x => x.Date == DateTime.Today).ToList();
            var userList = await _userService.GetAllUsers();
            var clientList = userList.Where(x => x.Roles.Contains("Client")).ToList();
            var activeClient = clientList.Where(x => x.IsActive == true).ToList();
            var inActiveClient = clientList.Where(x => x.IsActive == false).ToList();


            var dashBoardStatitics = new DashBoardStatitics
            {

                TotalTransactions = transactionsList.Count,
                DayTransactions = dayTransactions.Count,
                TotalPayments = paymentList.Count,
                DayPayments = dayPayment.Count,
                TotalActiveClients = activeClient.Count,
                TotalInactiveClients = inActiveClient.Count,
                TotalCreditCards = credictCardList.Count,
                TotalSavingAccounts = savingAccountList.Count,
                TotalLoans = loanList.Count,
                TotalProducts = credictCardList.Count + savingAccountList.Count + loanList.Count
           };

            return dashBoardStatitics;

        }
      
        public async Task<ProductVM> GetAllProducts()
        {
            var credictCardList = await _creditCardService.GetAllVMbyUserId();
            var savingAccountList = await _savingAccountService.GetAllVMbyUserId();
            var loanList = await _loanService.GetAllVMbyUserId();

            
            var product = new ProductVM
            {
                CreditCards = credictCardList,
                SavingAccounts = savingAccountList,
                LoanVMs = loanList
            };

            return product;
        }

    }
}
