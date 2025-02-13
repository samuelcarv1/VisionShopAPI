﻿using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisionShopAPI.Models;

namespace VisionShopAPI.Data
{
    public class VisionShopContext : IdentityDbContext<Usuario>
    {
        public VisionShopContext(DbContextOptions<VisionShopContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relacionamento entre Venda e Cliente
            builder.Entity<Venda>()
                   .HasOne(v => v.Cliente)
                   .WithMany()
                   .HasForeignKey(v => v.ClienteId);

            // Relacionamento entre ItemVenda e Venda
            builder.Entity<ItemVenda>()
                   .HasOne(iv => iv.Venda)
                   .WithMany(v => v.Itens)
                   .HasForeignKey(iv => iv.VendaId);

            // Relacionamento entre ItemVenda e Oculos
            builder.Entity<ItemVenda>()
                   .HasOne(iv => iv.Oculos)
                   .WithMany()
                   .HasForeignKey(iv => iv.OculosId);

            //Relacionamento
            builder.Entity<MovimentacaoEstoque>()
                .HasOne(m => m.Oculos)
                .WithMany(o => o.MovimentacoesEstoque)
                .HasForeignKey(m => m.OculosId);
        }

        public DbSet<Oculos> Oculos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        public DbSet<MovimentacaoEstoque> MovimentacaoEstoques { get; set; }
    }
}
