using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zadatak.Models
{
    public class ZadatakContext : DbContext
    {
        public ZadatakContext(DbContextOptions<ZadatakContext> options) : base(options)
        {
        }

        public DbSet<Osoba> Osobas { get; set; }
        public DbSet<Kancelarija> Kancelarijas { get; set; }
        public DbSet<Uredjaj> Uredjajs { get; set; }
        public DbSet<OsobaUredjaj> OsobaUredjajs { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OsobaUredjaj>()
                .HasKey(ou => new {ou.OsobaId, ou.UredjajId});
            modelBuilder.Entity<OsobaUredjaj>()
                .HasOne(ou => ou.Osoba)
                .WithMany(o => o.OsobaUredjajs)
                .HasForeignKey(ou => ou.OsobaId);
            modelBuilder.Entity<OsobaUredjaj>()
                .HasKey(ou => ou.Uredjaj)
                .WithMany(u => u.OsobaUredjajs)
                .HasForeignKey(ou => ou.UredjajId);

        }
        */
    }
}
