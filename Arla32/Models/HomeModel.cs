using System.ComponentModel.DataAnnotations;

namespace Arla32.Models
{
    public class HomeModel
    {

        public class BuscarOs
        {
            [Key]
            public string CodMaster { get; set; }
            public string Orcamento { get; set; }
            public string codigo { get; set; }
            public string Descricao { get; set; }
            public int Item { get; set; }
            public string NA { get; set; }
            public int PK { get; set; }
            public string OS { get; set; }
            public int acreditado { get; set; }
            public int CodigoEnsaio { get; set; }
           
        }

    }
}
