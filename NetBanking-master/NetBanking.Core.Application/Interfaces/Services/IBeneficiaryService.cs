using NetBanking.Core.Application.ViewModel.Beneficiary;
using NetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface IBeneficiaryService : IGenericService<SaveBeneficiaryVM, BeneficiaryVM, Beneficiary>
    {
        Task<bool> AddBeneficiary(string identifyingNumber);
        Task<List<BeneficiaryVM>> GetBeneficiryByUserId();
    }
}
