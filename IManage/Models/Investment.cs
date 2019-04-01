using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IManage.Models
{
    public class Investment
    {
        public int InvestmentId { get; set; }
        public int ClientId { get; set; }
        public int ProgId { get; set; }
        public int Balance { get; set; }
        public DateTime DateOpen { get; set; }

        public virtual Client Client { get; set; }
        public virtual Prog Prog { get; set; }
    }
}
