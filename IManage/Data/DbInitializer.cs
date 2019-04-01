using IManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IManage.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BankContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Client.Any())
            {
                var clients = new Client[]
            {
                new Client{FIO="Stanislav Vasilievich Belkovsky", Value = 1234},
                new Client{FIO="Victor Vasilievich Orlov", Value = 34234},
            };
                foreach (Client b in clients)
                {
                    context.Client.Add(b);
                }
                context.SaveChanges();

            }
            if (!context.Prog.Any())
            {
                var progs = new Prog[]
            {
                new Prog { percent=6, months = 48, Name = "Невостребованный" },
                new Prog { percent=13, months = 60, Name = "Традиционный" },
            };
                foreach (Prog p in progs)
                {
                    context.Prog.Add(p);
                }
                context.SaveChanges();
            }
            if (!context.Investment.Any())
            {
                var invests = new Investment[]
            {
                new Investment { Balance=123, ClientId = 1, ProgId = 1, DateOpen = DateTime.Now },
                new Investment { Balance=2313, ClientId = 1, ProgId = 2, DateOpen = DateTime.Now },
            };
                foreach (Investment p in invests)
                {
                    context.Investment.Add(p);
                }
                context.SaveChanges();

            }
            
        }
    }
}
