using NetBanking.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Domain.Entities
{
    public class CreditCard : Product
    {
        public decimal Limit { get; set; } 
        public decimal CurrentAmount { get; set; }
        public decimal Debt { get; set; }


    }
}
