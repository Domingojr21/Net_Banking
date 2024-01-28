using AutoMapper;
using Microsoft.AspNetCore.Http;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Helpers;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.Beneficiary;
using NetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.Services
{
    public class BeneficiaryService : GenericService<SaveBeneficiaryVM, BeneficiaryVM, Beneficiary>, IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly ISavingAccountService _savingAccountService;
        private readonly AuthenticationResponse userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BeneficiaryService(IUserService userService,IBeneficiaryRepository beneficiaryRepository, IMapper mapper, ISavingAccountService savingAccountService, IHttpContextAccessor httpContextAccessor) : base(beneficiaryRepository, mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _mapper = mapper;
            _savingAccountService = savingAccountService;
            _httpContextAccessor = httpContextAccessor;
            userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
        }

        public async Task<bool> AddBeneficiary(string identifyingNumber)
        { 
            var addedSuccessfully = await _beneficiaryRepository.AddBeneficiary(identifyingNumber);
            return addedSuccessfully;
        }

       

        public override async Task<SaveBeneficiaryVM> Add(SaveBeneficiaryVM vm)
        {
            var beneficiary = new Beneficiary();

            var destinationAccount = await _savingAccountService.GetByAccountINumber(vm.IdentifyingNumberofProduct);

            var beneficiaries = await GetBeneficiryByUserId();
            var beneficiaryalreadyExits = beneficiaries.Exists(x => x.IdentifyingNumberofProduct == vm.IdentifyingNumberofProduct);

            var savingAccounts = await _savingAccountService.GetAllViewModel();


            if (userSession.Id != destinationAccount.UserNameofOwner && beneficiaryalreadyExits == false)
            {
                var matchingSavingAccount = savingAccounts.FirstOrDefault(x => x.IdentifyingNumber == vm.IdentifyingNumberofProduct);

                if (matchingSavingAccount != null)
                {
                    var userId = userSession?.Id;

                    if (userId != null)
                    {
                        beneficiary.IdentifyingNumberofProduct = matchingSavingAccount.IdentifyingNumber;
                        beneficiary.UserName = userSession.Id;

                        beneficiary.IdentifyingNumberofProduct = vm.IdentifyingNumberofProduct;
                        beneficiary.BeneficiaryUserName = matchingSavingAccount.UserNameofOwner;

                        var addedBeneficiary = await _beneficiaryRepository.AddAsync(beneficiary);

                        return _mapper.Map<SaveBeneficiaryVM>(addedBeneficiary);
                    }
                    else
                    {
                        throw new InvalidOperationException("No se pudo obtener el ID del usuario en sesión");
                    }
                }
                else
                {
                    throw new InvalidOperationException("El número de cuenta de ahorro no existe");
                }
            }
            vm.HasError = true;
            vm.ErrorMessage = "No se puede agregar a usted mismo como Beneficiario";
            return vm;
               
        }

        public async Task<List<BeneficiaryVM>> GetBeneficiryByUserId()
        {
            var beneficiaryList = await _beneficiaryRepository.GetAllAsync();
            List<BeneficiaryVM> listBeneficiaryVM = new List<BeneficiaryVM>();
            foreach (var list in beneficiaryList)
            {
                var userBeneficiary = await _userService.GetUserDTOAsync(list.BeneficiaryUserName);
                var beneficiary = new BeneficiaryVM
                {
                    Id = list.Id,
                    Name = userBeneficiary.FirstName,
                    IdentifyingNumberofProduct = list.IdentifyingNumberofProduct,
                    UserName = list.UserName,
                    BeneficiaryUserName = userBeneficiary.UserName,
                    LastName = userBeneficiary.LastName,
                };

                listBeneficiaryVM.Add(beneficiary);
            }

            return _mapper.Map<List<BeneficiaryVM>>(listBeneficiaryVM);
        }
    }
}
