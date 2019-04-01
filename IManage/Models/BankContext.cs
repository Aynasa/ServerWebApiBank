using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IManage.Models
{
    public class BankContext:DbContext
    {
        #region Constructor
        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        { }
        #endregion

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Investment> Investment { get; set; }
        public virtual DbSet<Prog> Prog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.FIO).IsRequired();
            });

            modelBuilder.Entity<Prog>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Investments)
                    .HasForeignKey(d => d.ClientId);
                entity.HasOne(o => o.Prog)
                .WithMany(p => p.Investments)
                    .HasForeignKey(d => d.ProgId);
            });
        }
    }
}