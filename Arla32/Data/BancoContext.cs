using Arla32.Models;
using Microsoft.EntityFrameworkCore;

namespace Arla32.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<AcessModel.Usuario> usuario_copy { get; set; }
    }
}
