using AutoMapper;
using Microsoft.AspNetCore.Http;
using NetBanking.Application.Interfaces.Services;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Application.ViewModel.Products;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Core.Application.Services
{
    public class SavingAccountService : GenericService<SaveSavingAccountVM, SavingAccountVM, SavingAccount>, ISavingAccountService
    {
        private readonly ISavingAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountServices;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SavingAccountService(IHttpContextAccessor httpContextAccessor,
            ISavingAccountRepository repository,
            IMapper mapper, IAccountService accountServices) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _accountServices = accountServices;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public Task AddAmountToPrincipalSavingAccount(SaveSavingAccountVM vm)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAmountToSavingAccount(string userName, decimal amount)
        {
            var savingAccount = await _repository.GetSavingAccountByOwner(userName);

            if (savingAccount != null)
            {
                
                savingAccount.Amount += amount;
                savingAccount.UserNameofOwner = userName;
                await _repository.UpdateAsync(savingAccount, savingAccount.Id);
                return true;
            }

            return false; 
        }

      
        public override async Task<List<SavingAccountVM>> GetAllViewModel()
        {
            var list = await _repository.GetAllAsync();

            List<SavingAccountVM> result = new List<SavingAccountVM>();

            foreach (var item in list)
            {

                SavingAccountVM vm = new SavingAccountVM
                {
                    Id = item.Id,
                    UserNameofOwner = item.UserNameofOwner,
                    Amount = item.Amount,
                    IsPrincipal = item.IsPrincipal,
                    IdentifyingNumber = item.IdentifyingNumber,
                };

                var a = await _accountServices.GetUserById(item.UserNameofOwner);

                vm.UserName = a.UserName;
                result.Add(vm);

            }

            return result;
        }

        public async Task<SavingAccountVM> GetByAccountINumber(string identifyingNumber)
        {
            var list = await GetAllViewModel();

            return list.FirstOrDefault(x => x.IdentifyingNumber == identifyingNumber);
        }

        public async Task<List<SavingAccountVM>> GetAllVMbyUserId()
        {
            var savingAccountList = await _repository.GetAllAsync();
            savingAccountList = savingAccountList.Where(x => x.UserNameofOwner == userSession.Id).ToList();

            var SavingAccountsVM = _mapper.Map<List<SavingAccountVM>>(savingAccountList);
           
            return SavingAccountsVM;
        }

        public async Task<SaveSavingAccountVM> GetVmByAccountNumber(string identifyingNumber)
        {
            var list = await GetAllViewModel();

             var accout = list.FirstOrDefault(x => x.IdentifyingNumber == identifyingNumber);

            var save = new SaveSavingAccountVM()
            {
                Id = accout.Id,
                Amount = accout.Amount,
                IdentifyingNumber = identifyingNumber,
                IsPrincipal = accout.IsPrincipal,
                //UserName = accout.UserName,
                UserNameofOwner = accout.UserNameofOwner,
            };
            return save;

            
        }
    }
}
