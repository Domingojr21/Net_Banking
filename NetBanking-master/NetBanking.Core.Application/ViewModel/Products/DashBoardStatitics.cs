using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Application.ViewModel.Products
{
    public class DashBoardStatitics
    {
        public int TotalTransactions { get; set; }
        public int DayTransactions { get; set; }
        public int TotalPayments { get; set; }
        public int DayPayments { get; set;}
        public int TotalActiveClients { get; set; }
        public int TotalInactiveClients { get; set; }
        public int TotalCreditCards { get; set; }
        public int TotalSavingAccounts { get; set; }
        public int TotalLoans { get; set; }
        public int TotalProducts { get; set; }
    }
}
