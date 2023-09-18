using Arla32.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Arla32.Models
{
    public class ColetaModel
    {
        public class ArlaConcentracao
        {
            [key]
            public int Id { get; set; }
            public string os { get; set; }
            public string orcamento { get; set; }
            public string? rev { get; set; }
            public DateTime data_ini { get; set; }
            public DateTime data_term { get; set; }
            public string lote_solucao { get; set; }
            public string codigo_curva { get; set; }
            public string fator_avaliacao { get; set; }
            public float indice_agua { get; set; }
            public float refracao_amostra1 { get; set; }
            public float refracao_amostra2 { get; set; }
            public float conc_biureto { get; set; }
            public float conc_ureia { get; set; }
            public string desc1_instrumento { get; set; }
            public string codigo1_instrumento { get; set; }
            public DateTime validade1_instrumento { get; set; }
            public string desc2_instrumento { get; set; }
            public string codigo2_instrumento { get; set; }
            public DateTime validade2_instrumento { get; set; }
            public string ee_equipamento { get; set; }
            public string de_equipamento { get; set; }
            public string obs { get; set; }
            public string executado_por { get; set; }
            public string auxiliado_por { get; set; }

        }
        public class ArlaAlcalinidade
        {
            [Key]
            public int Id { get; set; }
            public string os { get; set; }
            public string orcamento { get; set; }
            public string? rev { get; set; }
            public DateTime data_ini { get; set; }
            public DateTime data_term { get; set; }
            public string pre_massa_amostra { get; set; }
            public string pre_vol_titulado { get; set; }
            public string pre_resultado_final {get; set;}
            public string det_massa_amostra1 { get; set; }
            public string det_massa_amostra2 { get; set; }
            public string det_vol_titulado1 { get; set; }
            public string det_vol_titulado2 { get; set; }
            public string det_res_final { get; set;}
            public string mat_prima { get; set; }
            public string mat_lote { get; set; } 
            public DateTime mat_validade { get; set; }
            public string inst_desc1 { get; set; }
            public string inst_cod1 { get; set; }
            public DateTime inst_data1 { get; set; }
            public string inst_desc1_1 { get; set; }
            public string inst_cod1_1 { get; set; }
            public DateTime inst_data1_1 { get; set; }
            public string inst_desc2 { get; set; }
            public string inst_cod2 { get; set; }
            public DateTime inst_data2 { get; set; }
            public string inst_desc2_2 { get; set; }
            public string inst_cod2_2 { get; set; }
            public DateTime inst_data2_2 { get; set; }
            public string inst_desc3 { get; set; }
            public string inst_cod3 { get; set; } 
            public DateTime inst_data3 { get; set; }
            public string inst_desc3_3 { get; set; }
            public string inst_cod3_3 { get; set; }
            public DateTime inst_data3_3 { get; set; }
            public string inst_desc4 { get; set; }
            public string inst_cod4 { get; set; }
            public DateTime inst_data4 { get; set; }
            public string inst_desc4_4 { get; set; }
            public string inst_cod4_4 { get; set; }
            public DateTime inst_data4_4 { get; set; }
            public string equip_de { get; set; }
            public string equip_ee { get; set; }
            public string obs { get; set; }
            public string executado_por { get; set; }
            public string auxiliado_por { get; set;}
        }
        public class ArlaBiureto
        {
            [Key]
            public int Id { get; set; }
            public string os { get; set; }
            public string orcamento { get; set; }
            public string? rev { get; set; }
            public DateTime data_ini { get; set; }
            public DateTime data_term { get; set; }
            public string codigo_curva { get; set; }
            public float fator_calibracao { get; set; }
            public float fator_dia { get; set; }
            public float amostra1 { get; set; }
            public float amostra2 { get; set; }
            public float absorbancia_1 { get; set; }
            public float absorbancia_2 { get;  set; }
            public float absorbancia_3 { get;  set; } 
            public float result_ind_1 { get; set; }
            public float result_ind_2 { get; set; } 
            public string mat_prima_1 { get; set; }
            public string mat_lote_1 { get; set; }
            public DateTime mat_validade_1 { get; set; }
            public string mat_prima_2 { get; set; } 
            public string mat_lote_2 { get; set; } 
            public DateTime mat_validade_2 { get; set; }
            public string mat_prima_3 { get; set; }
            public string mat_lote_3 { get; set; } 
            public DateTime mat_validade_3 { get; set; }
            public string mat_prima_4 { get;  set; }
            public string mat_lote_4 { get; set; }
            public DateTime mat_validade_4 { get; set; }
            public string mat_prima_5 { get;  set; }
            public string mat_lote_5 { get; set; }
            public DateTime mat_validade_5 { get; set; } 
            public string inst_desc1 { get; set; }
            public string inst_codigo1 { get; set; }
            public DateTime inst_data1 { get; set; } 
            public string inst_desc1_1 { get; set; }
            public string inst_codigo1_1 { get; set; }
            public DateTime inst_data1_1 { get; set; }
            public string insta_desc2 { get; set; }
            public string inst_codigo2 { get; set; }
            public DateTime inst_data2 { get; set; }
            public string inst_desc2_2 { get; set; } 
            public string inst_codigo2_2 { get; set; }
            public DateTime inst_data2_2 { get; set; }
            public string inst_desc3 { get; set; }
            public string inst_codigo3 { get; set; }
            public DateTime inst_data3 { get; set; }
            public string inst_desc3_3 { get; set; }
            public string inst_codigo3_3 { get; set; }
            public DateTime inst_data3_3 { get; set; } 
            public string inst_desc4 { get; set; }
            public string inst_codigo4 { get;  set; } 
            public DateTime inst_data4 { get; set; }
            public string inst_desc4_4 { get; set; }
            public string inst_codigo4_4 { get; set; }
            public DateTime inst_data4_4 { get; set; }
            public string equip_de { get; set; }
            public string equip_ee { get; set; }
            public string obs { get; set; }
            public string executado_por { get; set; }
            public string auxiliado_por { get; set; }
        }
    }
}
