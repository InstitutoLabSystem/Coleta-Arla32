using static Arla32.Models.ColetaModel;
using static Arla32.Models.HomeModel;

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

    }
}
