using AutoMapper;
using Microsoft.AspNetCore.Http;
using NetBanking.Application.Interfaces.Services;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Core.Application.Services
{
    public class LoanService : GenericService<SaveLoanVM, LoanVM, Loan>, ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountServices;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISavingAccountService _savingAccountService;

        public LoanService(ISavingAccountService savingAccountService,IHttpContextAccessor httpContextAccessor,ILoanRepository loanRepository, IMapper mapper, IAccountService accountServices) : base(loanRepository, mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _accountServices = accountServices;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _savingAccountService = savingAccountService;
        }

        public override async Task<List<LoanVM>> GetAllViewModel()
        {
            var list = await _loanRepository.GetAllAsync();

            List<LoanVM> result = new List<LoanVM>();

            foreach (var item in list)
            {

                LoanVM vm = new LoanVM
                {
                    Id = item.Id,
                    UserNameofOwner = item.UserNameofOwner,
                    LoanQuantity = item.LoanQuantity,
                    PaidQuantity = item.PaidQuantity,
                    IdentifyingNumber = item.IdentifyingNumber,
                };

                var a = await _accountServices.GetUserById(item.UserNameofOwner);

                vm.UserName = a.UserName;
                result.Add(vm);

            }

            return result;
        }

        public async Task<List<LoanVM>> GetAllVMbyUserId()
        {
            var loanList = await _loanRepository.GetAllAsync();

             loanList = loanList.Where(x => x.UserNameofOwner == userSession.Id).ToList();


            var LoanVMs = _mapper.Map<List<LoanVM>>(loanList);
            return LoanVMs;
        }


        public async Task<LoanVM> GetByLoanINumber(string identifyingNumber)
        {
            var list = await GetAllViewModel();

            return list.FirstOrDefault(x => x.IdentifyingNumber == identifyingNumber);
        }

    }
}
