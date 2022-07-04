using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio;

namespace Persistencia
{
    public class PagosOnlineContext : DbContext
    {
        public PagosOnlineContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
            .HasOne(a => a.pago)
            .WithOne(a => a.Categoria)
            .HasForeignKey<Pago>(c => c.id_categoria);
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Documento> Documento { get; set; }  
        public DbSet<Pago> Pago { get; set; }


    }
}
