using System.ComponentModel.DataAnnotations;

namespace Arla32.Models
{
    public class AcessModel
    {
        public class Usuario
        {
            [Key]
            public string Nome_Usuario { get; set; }
            public string Senha_Usuario { get; set; }
            public string nomecompleto { get; set; }
            public string cargo { get; set; }
            public string setor { get; set; }
            public string laboratorio { get; set; }
            public int ativo { get; set; }
        }

       
    }
}
