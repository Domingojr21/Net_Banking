using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.ViewModel.SavingAccount
{
    public class UpdateSavingAccountVM
    {
        public string UserNameofOwner { get; set; } = null!;
        public bool IsPrincipal { get; set; }
        public decimal AmountToAdd { get; set; }
    }
}
