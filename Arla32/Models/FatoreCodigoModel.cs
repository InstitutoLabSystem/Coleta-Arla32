using Arla32.Models;
using Microsoft.Identity.Client;
using Microsoft.SqlServer.Server;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection.Metadata;


namespace Arla32.Models
{
    public class FatoreCodigoModel
    {

        public class ArlaCodigo
        {
            [Key] public int id { get; set; }
            public float? fator_calibracao { get; set; }
            public float? codigo_curva { get; set; }
            public string? anexo { get; set; }

        }






    }
}
