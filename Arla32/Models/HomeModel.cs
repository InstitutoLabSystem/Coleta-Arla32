using Arla32.Models;
using System.ComponentModel.DataAnnotations;

namespace Arla32.Models
{
    public class HomeModel
    {
        public class ProgramacaoLabEnsaios

        {
            [Key]
            public int pk { get; set; }
            public string OS { get; set; }
            public int NA { get; set; }
            public string Orcamento { get; set; }
            public string Item { get; set; }
           
        }


        public class OrdemServicoCotacao
        {
            [Key]
            public int Item { get; set; }
            public string CodigoEnsaio { get; set; }
            public string orcamento { get; set; }
        }



        public class WModDetProd
        {

            [Key]
            public int cod { get; set; }
            public string codmaster { get; set; }

            public string codigo { get; set; }

            public string descricao { get; set; }

        }


        public class Resposta
        {
            public string OS { get; set; }
            public int NA { get; set; }
            public string codmaster { get; set; }

            public string codigo { get; set; }

            public string descricao { get; set; }

        }


    }

}




