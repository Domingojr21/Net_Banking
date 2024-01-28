using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Domain.Common
{
    public interface IsStatusEntity
    {
        public  int Id { get; set; }
        public bool Status { get; set; }
    }
}
