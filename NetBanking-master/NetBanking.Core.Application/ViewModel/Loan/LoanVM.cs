using NetBanking.Core.Application.ViewModel.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.ViewModel.Loan
{
    public class LoanVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string IdentifyingNumber { get; set; } = null!;
        public string UserNameofOwner { get; set; } = null!;
        public decimal LoanQuantity { get; set; }
        public decimal PaidQuantity { get; set; }

    }
}
