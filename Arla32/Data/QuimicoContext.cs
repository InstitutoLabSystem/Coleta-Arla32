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
        public DbSet<ColetaModel.ArlaConcentracao> arla_concentracao_indice { get; set; }
        public DbSet<ColetaModel.ArlaAlcalinidade> arla_alcalinidade { get; set; }
        public DbSet<ColetaModel.ArlaBiureto> arla_biureto { get; set; }

        public DbSet<ColetaModel.ArlaAldeidos> arla_aldeidos { get; set; }
        public DbSet<ColetaModel.ArlaInsoluveis> arla_insoluveis { get; set; }
        // Outros conjuntos de entidades relacionados ao banco de dados "quimico"
    }
}
