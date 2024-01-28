using AutoMapper;
using Microsoft.AspNetCore.Http;
using NetBanking.Application.Interfaces.Services;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Core.Application.Services
{
    public class CreditCardService : GenericService<SaveCreditCardVM, CreditCardVM, CreditCard>, ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IAccountService _accountServices;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CreditCardService(IHttpContextAccessor httpContextAccessor,
            ICreditCardRepository creditCardRepository, 
            IMapper mapper, IAccountService accountServices) : base(creditCardRepository, mapper)
        {
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
            _accountServices = accountServices;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public async Task<List<CreditCardVM>> GetAllVMbyUserId()
        {
            var credictCardList = await _creditCardRepository.GetAllAsync();
          
            credictCardList = credictCardList.Where(x => x.UserNameofOwner == userSession.Id).ToList();

            var CreditCardsVMList = _mapper.Map<List<CreditCardVM>>(credictCardList);
            
            return CreditCardsVMList;
        }

        public override async Task<List<CreditCardVM>> GetAllViewModel()
        {
            var list = await _creditCardRepository.GetAllAsync();

            List<CreditCardVM> result = new List<CreditCardVM>();

            foreach (var item in list)
            {

                CreditCardVM vm = new CreditCardVM
                {
                    Id = item.Id,
                    UserNameofOwner = item.UserNameofOwner,
                    Limit = item.Limit,
                    CurrentAmount = item.CurrentAmount,
                    IdentifyingNumber = item.IdentifyingNumber,
                    Debt = item.Debt
                    
                };

                var a = await _accountServices.GetUserById(item.UserNameofOwner);

                vm.UserName = a.UserName;
                result.Add(vm);

            }

            return result;
        }

        public async Task<SaveCreditCardVM> GetByCardNumber(string cardNumber)
        {
            return _mapper.Map<SaveCreditCardVM>(await _creditCardRepository.GetByCardNumber(cardNumber));
        }


        public async Task<CreditCardVM> GetByCardIdentifyinNumber(string identifyingNumber)
        {
            var list = await GetAllViewModel();

            return list.FirstOrDefault(x => x.IdentifyingNumber == identifyingNumber);
        }
    }
}
