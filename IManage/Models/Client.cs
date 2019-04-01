using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IManage.Models
{
    public class Client
    {
        public Client()
        {
            Investments = new HashSet<Investment>();
        }

        public int ClientId { get; set; }
        public string FIO { get; set; }
        public string passport { get; set; }
        public int Value { get; set; }

        public virtual ICollection<Investment> Investments { get; set; }
    }
}
