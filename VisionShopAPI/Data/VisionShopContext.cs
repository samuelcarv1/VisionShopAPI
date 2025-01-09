using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VisionShopAPI.Models;

namespace VisionShopAPI.Data
{
    public class VisionShopContext : DbContext
    {
        public VisionShopContext(DbContextOptions<VisionShopContext> options) : base(options) { }
        public DbSet<Oculos> Oculos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
