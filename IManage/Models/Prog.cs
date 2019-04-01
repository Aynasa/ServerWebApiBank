using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IManage.Models
{
    public class Prog
    {
        public Prog()
        {
            Investments = new HashSet<Investment>();
        }

        public int ProgId { get; set; }
        public int percent { get; set; }
        public int months { get; set; }
        public string Name { get; set; }


        public virtual ICollection<Investment> Investments { get; set; }
    }
}
