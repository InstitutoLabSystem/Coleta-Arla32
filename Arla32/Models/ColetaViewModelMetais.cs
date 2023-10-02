using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static Arla32.Models.ColetaModel;
namespace Arla32.Models
{
    public class ColetaViewModelMetais
    {
        public ArlaMetais arlaMetais { get; set; }
        public MetaisTratamento metaisTratamento { get; set; }
    }
}
