using Arla32.Models;
using Microsoft.Identity.Client;
using Microsoft.SqlServer.Server;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection.Metadata;


namespace Arla32.Models
{
    public class lotesModel
    {

        public class ArlaLotes
        {
            [key] public int id { get; set; }
            public string lote { get; set; }
            public string anexo { get; set; }
        }
    }
}
