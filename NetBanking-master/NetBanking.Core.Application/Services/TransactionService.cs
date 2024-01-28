using AutoMapper;
using NetBanking.Core.Application.Interfaces.Repositories;
using NetBanking.Core.Application.Interfaces.Services;
using NetBanking.Core.Application.ViewModel.CreditCard;
using NetBanking.Core.Application.ViewModel.Loan;
using NetBanking.Core.Application.ViewModel.SavingAccount;
using NetBanking.Core.Application.ViewModel.Transaction;
using NetBanking.Core.Domain.Entities;
using TransactionType = NetBanking.Core.Application.Enums.TransactionType;

namespace NetBanking.Core.Application.Services
{

    public class TransactionService : GenericService<SaveTransactionVM, TransactionVM, Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IUserService _userService;
        private readonly ILoanService _loanService;
        private readonly ICreditCardService _creditCardService;
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, ISavingAccountService savingAccountService, IUserService userService, ILoanService loanService, ICreditCardService creditCardService) : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _savingAccountService = savingAccountService;
            _userService = userService;
            _loanService = loanService;
            _creditCardService = creditCardService;
        }

        public async Task ConfirmExpressPayment(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);

            destinationAccount.Amount += svm.Amount;
            var destinyAccount = _mapper.Map<SaveSavingAccountVM>(destinationAccount);
            await _savingAccountService.Update(destinyAccount, destinyAccount.Id);

            originAccount.Amount -= svm.Amount;
            var accountOrigin = _mapper.Map<SaveSavingAccountVM>(originAccount);
            await _savingAccountService.Update(accountOrigin, accountOrigin.Id);

            var transaction = new SaveTransactionVM
            {
                Amount = svm.Amount,
                DestinationAccountNumber = svm.DestinationAccountNumber,
                OriginAccountNumber = svm.OriginAccountNumber,
                Description = svm.Description,
                TransactionTypeId = svm.TransactionTypeId,
                UserNameOfAccountHolder = svm.UserNameOfAccountHolder
            };

            await Add(transaction);
        }


        public async Task<SCPaymentExpressVM> AddExpressPayment(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);
            if(destinationAccount.UserNameofOwner == originAccount.UserNameofOwner)
            {
                svm.HasError = true;
                svm.ErrorMessage = "No puede realizar un pago Express a su misma cuenta";

                SCPaymentExpressVM confirmPayment = new()
                {
                    SaveTransactionVM = svm,

                };
                return confirmPayment;

            }
            if (destinationAccount != null)
            {
                if (originAccount.Amount >= svm.Amount)
                {
                    var user = await _userService.GetUserDTOAsync(destinationAccount.UserNameofOwner);
                    svm.UserNameOfAccountHolder = originAccount.UserNameofOwner;
                    SCPaymentExpressVM confirmPayment = new()
                    {
                        SaveTransactionVM = svm,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    };
                    return confirmPayment;

                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";
                    SCPaymentExpressVM confirmPayment = new()
                    {
                        SaveTransactionVM = svm,

                    };
                    return confirmPayment;
                }

            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "Este número de cuenta no Existe";

                SCPaymentExpressVM confirmPayment = new()
                {
                    SaveTransactionVM = svm,

                };
                return confirmPayment;

            }
        }


        public async Task<SaveLoanVM> AddLoanPayment(SaveTransactionVM svm)
        {
            var destinationAccount = await _loanService.GetByLoanINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);
            SaveLoanVM loan = new();

            if (destinationAccount.PaidQuantity != destinationAccount.LoanQuantity)
            {
                if (originAccount.Amount >= svm.Amount)
                {
                    var pay = destinationAccount.PaidQuantity + svm.Amount;

                    if (pay != destinationAccount.LoanQuantity)
                    {
                        var amount = 0m;
                        if (pay < destinationAccount.LoanQuantity)
                        {
                            pay = destinationAccount.PaidQuantity += svm.Amount;
                            originAccount.Amount -= svm.Amount;
                        }
                        else
                        {
                            amount = pay - destinationAccount.LoanQuantity;
                            destinationAccount.PaidQuantity += svm.Amount - amount;
                            originAccount.Amount -= svm.Amount - amount;
                        }


                        var accountOrigin = new SaveSavingAccountVM()
                        {
                            IdentifyingNumber = originAccount.IdentifyingNumber,
                            Amount = originAccount.Amount,
                            IsPrincipal = originAccount.IsPrincipal,
                            UserNameofOwner = originAccount.UserNameofOwner,
                            Id = originAccount.Id
                        };
                        await _savingAccountService.Update(accountOrigin, accountOrigin.Id);


                        var destinyAccount = new SaveLoanVM()
                        {
                            IdentifyingNumber = destinationAccount.IdentifyingNumber,
                            UserNameofOwner = destinationAccount.UserNameofOwner,
                            LoanQuantity = destinationAccount.LoanQuantity,
                            PaidQuantity = destinationAccount.PaidQuantity,
                            Id = destinationAccount.Id

                        };
                        await _loanService.Update(destinyAccount, destinyAccount.Id);

                        var transaction = new SaveTransactionVM
                        {
                            Amount = destinationAccount.PaidQuantity,
                            DestinationAccountNumber = svm.DestinationAccountNumber,
                            OriginAccountNumber = svm.OriginAccountNumber,
                            Description = svm.Description,
                            TransactionTypeId = (int)TransactionType.PagoExpreso,
                            UserNameOfAccountHolder = originAccount.UserNameofOwner
                        };

                        SaveLoanVM saveLoanVM = new()
                        {
                            PaidQuantity = pay,
                            LoanQuantity = amount
                        };

                        await Add(transaction);
                        saveLoanVM.SaveTransactionVM = new();
                        return saveLoanVM;
                    }
                    else
                    {
                        svm.HasError = true;
                        svm.ErrorMessage = "El prestamo ya ha sido saldado";
                        loan.SaveTransactionVM = svm;
                        return loan;
                    }


                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";
                    loan.SaveTransactionVM = svm;
                    return loan;
                }

            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "La deuda ha sido paga";
                loan.SaveTransactionVM = svm;
                return loan;
            }
        }

        public async Task<SaveCreditCardVM> AddCreditCard(SaveTransactionVM svm)
        {
            var destinationAccount = await _creditCardService.GetByCardIdentifyinNumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);
            var pay = 0.00m;
            var amount = 0m;
            SaveCreditCardVM card = new();

            if (originAccount.Amount >= svm.Amount)
            {
                if (destinationAccount.Debt > decimal.Zero)
                {
                    if (destinationAccount.Debt >= svm.Amount)
                    {
                        destinationAccount.Debt -= svm.Amount;
                        destinationAccount.CurrentAmount += svm.Amount;
                        originAccount.Amount -= svm.Amount;
                    }
                    else
                    {
                        pay = destinationAccount.Debt;
                        destinationAccount.Debt = decimal.Zero;
                        destinationAccount.CurrentAmount += pay;

                        if (pay != decimal.Zero)
                        {
                            originAccount.Amount -= pay;
                        }
                    }

                    var accountOrigin = new SaveSavingAccountVM()
                    {
                        IdentifyingNumber = originAccount.IdentifyingNumber,
                        Amount = originAccount.Amount,
                        IsPrincipal = originAccount.IsPrincipal,
                        UserNameofOwner = originAccount.UserNameofOwner,
                        Id = originAccount.Id,
                    };
                    await _savingAccountService.Update(accountOrigin, accountOrigin.Id);

                    var destinyAccount = new SaveCreditCardVM()
                    {
                        IdentifyingNumber = destinationAccount.IdentifyingNumber,
                        UserNameofOwner = destinationAccount.UserNameofOwner,
                        CurrentAmount = destinationAccount.CurrentAmount,
                        Limit = destinationAccount.Limit,
                        Id = destinationAccount.Id,
                        Debt = destinationAccount.Debt
                    };
                    await _creditCardService.Update(destinyAccount, destinyAccount.Id);

                    var transaction = new SaveTransactionVM
                    {
                        Amount = svm.Amount,
                        DestinationAccountNumber = svm.DestinationAccountNumber,
                        OriginAccountNumber = svm.OriginAccountNumber,
                        Description = svm.Description,
                        TransactionTypeId = (int)TransactionType.PagoExpreso,
                        UserNameOfAccountHolder = originAccount.UserNameofOwner
                    };

                    SaveCreditCardVM savecardVM = new()
                    {
                        IdentifyingNumber = destinationAccount.IdentifyingNumber
                    };

                    await Add(transaction);
                    savecardVM.SaveTransactionVM = new();
                    return savecardVM;
                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "La Deuda ha sido Saldada gracias por ser responsable";
                    card.SaveTransactionVM = svm;
                    return card;
                }
            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "No tiene el monto Suficiente para Realizar el pago";
                card.SaveTransactionVM = svm;
                return card;
            }
        }


        public async Task<SCPaymentExpressVM> PayToBeneficiaries(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);
      
                if (originAccount.Amount >= svm.Amount)
                {
                    var user = await _userService.GetUserDTOAsync(destinationAccount.UserNameofOwner);
                    svm.UserNameOfAccountHolder = originAccount.UserNameofOwner;
                    SCPaymentExpressVM confirmBeneficiaryPayment = new()
                    {
                        SaveTransactionVM = svm,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    };
                    return confirmBeneficiaryPayment;

                }

                svm.HasError = true;
                svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";
                SCPaymentExpressVM confirmPayment = new()
                {
                    SaveTransactionVM = svm,

                };
                return confirmPayment;
            }

        public async Task ConfirmBeneficiaryPayment(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);

            destinationAccount.Amount += svm.Amount;
            var destinyAccount = _mapper.Map<SaveSavingAccountVM>(destinationAccount);
            await _savingAccountService.Update(destinyAccount, destinyAccount.Id);

            originAccount.Amount -= svm.Amount;
            var accountOrigin = _mapper.Map<SaveSavingAccountVM>(originAccount);
            await _savingAccountService.Update(accountOrigin, accountOrigin.Id);

            var transaction = new SaveTransactionVM
            {
                Amount = svm.Amount,
                DestinationAccountNumber = svm.DestinationAccountNumber,
                OriginAccountNumber = svm.OriginAccountNumber,
                Description = svm.Description,
                UserNameOfAccountHolder = svm.UserNameOfAccountHolder,
                TransactionTypeId = svm.TransactionTypeId
            };

            await Add(transaction);
        }

        public async Task<SaveTransactionVM> AddTransactionBetween(SaveTransactionVM svm)
        {
            var destinationAccount = await _savingAccountService.GetByAccountINumber(svm.DestinationAccountNumber);
            var originAccount = await _savingAccountService.GetByAccountINumber(svm.OriginAccountNumber);

            if (originAccount.Id != destinationAccount.Id)
            {
                if (originAccount.Amount >= svm.Amount)
                {

                    destinationAccount.Amount += svm.Amount;

                    var destinyAccount = new SaveSavingAccountVM()
                    {
                        IdentifyingNumber = destinationAccount.IdentifyingNumber,
                        Amount = destinationAccount.Amount,
                        IsPrincipal = destinationAccount.IsPrincipal,
                        UserNameofOwner = destinationAccount.UserNameofOwner,
                        Id = destinationAccount.Id
                    };
                    await _savingAccountService.Update(destinyAccount, destinyAccount.Id);

                    originAccount.Amount -= svm.Amount;


                    var accountOrigin = new SaveSavingAccountVM()
                    {
                        IdentifyingNumber = originAccount.IdentifyingNumber,
                        Amount = originAccount.Amount,
                        IsPrincipal = originAccount.IsPrincipal,
                        UserNameofOwner = originAccount.UserNameofOwner,
                        Id = originAccount.Id
                    };
                    await _savingAccountService.Update(accountOrigin, accountOrigin.Id);

                    return svm;
                }
                else
                {
                    svm.HasError = true;
                    svm.ErrorMessage = "No tiene el monto Suficiente para Realizar la Trasacción";

                    return svm;
                }
            }
            else
            {
                svm.HasError = true;
                svm.ErrorMessage = "No puede realizar una transferencia a la misma cuenta";

                return svm;
            }

        }
    }
}
