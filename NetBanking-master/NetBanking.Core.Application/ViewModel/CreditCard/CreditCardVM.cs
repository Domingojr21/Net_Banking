using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.ViewModel.CreditCard
{
    public class CreditCardVM
    {
        public int Id { get; set; }
        public decimal Limit { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal Debt { get; set; }
        public string IdentifyingNumber { get; set; } = null!;
        public string UserName { get; set; }
        public string UserNameofOwner { get; set; } = null!;
    }
}
