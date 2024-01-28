using AutoMapper;
using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.ViewModel.Beneficiary;
using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Application.ViewModel.Transaction;
using NetBanking.Core.Application.ViewModels.User;
using NetBanking.Core.Domain.Entities;

namespace NetBanking.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region UserProfile

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #region Users

            #region Transaction
            CreateMap<Transaction, SaveTransactionVM>()
                .ForMember(x => x.ErrorMessage, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            #endregion

            #region SavingAccount

            CreateMap<SavingAccount, SaveSavingAccountVM>()
                 .ForMember(x => x.users, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<SavingAccountVM, SaveSavingAccountVM>()
                .ForMember(x => x.users, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.UserName, opt => opt.Ignore());

            CreateMap<SavingAccount, SavingAccountVM>()
            .ReverseMap()
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore());


           

            #endregion

            #region Loans

            CreateMap<Loan, LoanVM>()
                .ForMember(x => x.UserName, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<Loan, SaveLoanVM>()
                .ReverseMap()
                 .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            #endregion

            #region Credit Cards

            CreateMap<CreditCard, CreditCardVM>()
                .ForMember(x => x.UserName, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<CreditCard, SaveCreditCardVM>()
              .ReverseMap()
              .ForMember(x => x.LastModified, opt => opt.Ignore())
          .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
          .ForMember(x => x.Created, opt => opt.Ignore())
          .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            #endregion

            #region Beneficiary
            CreateMap<Beneficiary, BeneficiaryVM>()
                    .ReverseMap()
                    .ForMember(x => x.LastModified, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                    .ForMember(x => x.Created, opt => opt.Ignore())
                    .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<Beneficiary, SaveBeneficiaryVM>()
                        .ReverseMap()
                        .ForMember(x => x.LastModified, opt => opt.Ignore())
                        .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                        .ForMember(x => x.Created, opt => opt.Ignore())
                        .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            #endregion
            #endregion
        }
    }
}
