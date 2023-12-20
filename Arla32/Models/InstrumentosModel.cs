using static Arla32.Models.ColetaModel;
using static Arla32.Models.HomeModel;
using static Arla32.Models.lotesModel;
using static Arla32.Models.FatoreCodigoModel;

namespace Arla32.Models
{
    public class InstrumentosModel
    {
        public List<ArlaInstrumentos> instrumentos { get; set; }
        public ArlaAlcalinidade alcalinidade { get; set; }
        public ArlaAldeidos aldeidos { get; set; }
        public ArlaBiureto biureto { get; set; }
        public ArlaFosfato fosfato { get; set; }
        public ArlaInsoluveis insoluveis { get; set; }
        public ArlaMetais metais { get; set; }
        public MetaisTratamento metaisTratamento { get; set; }
        public ArlaDensidade densidade { get; set; }
        public ArlaConcentracao concentracao { get; set; }
        public ArlaIdentidade identidade { get; set; }
        public ArlaInfo info { get; set; }
        public ArlaCodigo codigo { get; set; }
        public List<ArlaLotes> lotes { get; set; }

    }
}
