using System.Linq;
using Arla32.Models;
using Microsoft.EntityFrameworkCore;

namespace Arla32.Data
{ 
    public class QuimicoContext : DbContext
    {
        public QuimicoContext(DbContextOptions<QuimicoContext> options) : base(options)
        {
        }

        public DbSet<HomeModel.IniciarColeta> regtro_amostra_painel_copy { get; set; }
        // Outros conjuntos de entidades relacionados ao banco de dados "quimico"
    }
}
