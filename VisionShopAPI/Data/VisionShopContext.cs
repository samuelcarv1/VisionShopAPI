using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VisionShopAPI.Models;

namespace VisionShopAPI.Data
{
    public class VisionShopContext : DbContext
    {
        public VisionShopContext(DbContextOptions<VisionShopContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
        }

        public DbSet<Oculos> Oculos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }
    }
}
