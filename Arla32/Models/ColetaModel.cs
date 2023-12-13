using Arla32.Models;
using Microsoft.Identity.Client;
using Microsoft.SqlServer.Server;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection.Metadata;

namespace Arla32.Models
{
    public class ColetaModel
    {

        public class ArlaInstrumentos
        {
            [Key]
            public int Id { get; set; }
            public string? codigo { get; set; }
            public string? descricao { get; set; }
            public DateOnly? validade { get; set; }
            public string? anexo { get; set; }
            public int? ativo { get; set; }
            public int? equipamento { get; set; }
            public int? DE {  get; set; }

        }
        public class UploadResult
        {
            public string WebViewLink { get; set; }
            public string ImgId { get; set; }
        }

        public class ArlaIdentidade
        {

            [Key]
            public int Id { get; set; }
            public string os { get; set; }
            public string orcamento { get; set; }
            public DateOnly data_ini { get; set; }
            public DateOnly data_term { get; set; }
            public string? np { get; set; }
            public string? descricao { get; set; }
            public string? avaliacao { get; set; }
            public string? mat_prima { get; set; }
            public string? mat_lote { get; set; }
            public DateOnly mat_validade { get; set; }
            public string? cod_prima { get; set; }
            public string? cod_lote { get; set; }
            public DateOnly cod_validade { get; set; }
            public string? observacoes { get; set; }
            public string? executado { get; set; }
            public string? auxiliado { get; set; }
            public string? img { get; set; }
            public string? imgId { get; set; }

        }


        public class ArlaConcentracao
        {
            [key]
            public int Id { get; set; }
            public string os { get; set; }
            public string orcamento { get; set; }
            public string? rev { get; set; }
            public DateOnly data_ini { get; set; }
            public DateOnly data_term { get; set; }
            public string? lote_solucao { get; set; }
            public string? codigo_curva { get; set; }
            public float fator_avaliacao { get; set; }
            public float indice_agua { get; set; }
            public float refracao_amostra1 { get; set; }
            public float refracao_amostra2 { get; set; }
            public string? conc_biureto { get; set; }
            public string? conc_ureia { get; set; }
            public string? desc1_instrumento { get; set; }
            public string? codigo1_instrumento { get; set; }
            public DateOnly validade1_instrumento { get; set; }
            public string? desc2_instrumento { get; set; }
            public string? codigo2_instrumento { get; set; }
            public DateOnly validade2_instrumento { get; set; }
            public string? ee_equipamento { get; set; }
            public string? de_equipamento { get; set; }
            public string? obs { get; set; }
            public string? executado_por { get; set; }
            public string? auxiliado_por { get; set; }

        }
        public class ArlaAlcalinidade
        {
            [Key]
            public int Id { get; set; }
            public string os { get; set; }
            public string orcamento { get; set; }
            public string? rev { get; set; }
            public DateOnly data_ini { get; set; }
            public DateOnly data_term { get; set; }
            public string? pre_massa_amostra { get; set; }
            public string? pre_vol_titulado { get; set; }
            public string? pre_resultado_final { get; set; }
            public string? det_massa_amostra1 { get; set; }
            public string? det_massa_amostra2 { get; set; }
            public string? det_vol_titulado1 { get; set; }
            public string? det_vol_titulado2 { get; set; }
            public string? det_res_final { get; set; }
            public string? mat_prima { get; set; }
            public string? mat_lote { get; set; }
            public DateOnly mat_validade { get; set; }
            public string? equip_de { get; set; }
            public string? equip_ee { get; set; }
            public string? obs { get; set; }
            public string? executado_por { get; set; }
            public string? auxiliado_por { get; set; }
        }
        public class ArlaBiureto
        {
            [Key]
            public int Id { get; set; }
            public string os { get; set; }
            public string orcamento { get; set; }
            public string? rev { get; set; }
            public DateOnly data_ini { get; set; }
            public DateOnly data_term { get; set; }
            public string? codigo_curva { get; set; }
            public float fator_calibracao { get; set; }
            public float fator_dia { get; set; }
            public float amostra1 { get; set; }
            public float amostra2 { get; set; }
            public float absorbancia_1 { get; set; }
            public float absorbancia_2 { get; set; }
            public float absorbancia_3 { get; set; }
            public string? result_ind_1 { get; set; }
            public string? result_ind_2 { get; set; }
            public string? result_media { get; set; }
            public string? mat_prima_1 { get; set; }
            public string? mat_lote_1 { get; set; }
            public DateOnly mat_validade_1 { get; set; }
            public string? mat_prima_2 { get; set; }
            public string? mat_lote_2 { get; set; }
            public DateOnly mat_validade_2 { get; set; }
            public string? mat_prima_3 { get; set; }
            public string? mat_lote_3 { get; set; }
            public DateOnly mat_validade_3 { get; set; }
            public string? mat_prima_4 { get; set; }
            public string? mat_lote_4 { get; set; }
            public DateOnly mat_validade_4 { get; set; }
            public string? mat_prima_5 { get; set; }
            public string? mat_lote_5 { get; set; }
            public DateOnly mat_validade_5 { get; set; }
            public string? equip_de { get; set; }
            public string? equip_ee { get; set; }
            public string? obs { get; set; }
            public string? executado_por { get; set; }
            public string? auxiliado_por { get; set; }
        }

        public class ArlaAldeidos
        {

            [Key]
            public int id { get; set; }
            public string os { get; set; }
            public string? norma { get; set; }
            public string? np { get; set; }
            public string? descricao { get; set; }
            public string? orcamento { get; set; }
            public string? rev { get; set; }
            public DateOnly data_ini { get; set; }
            public DateOnly data_term { get; set; }
            public string? lote_sol { get; set; }
            public string codigo_curva { get; set; }
            public float fator_calibracao { get; set; }
            public float massa_branco { get; set; }

            public float absorbancia_branco { get; set; }
            public float massa_amostra { get; set; }

            public float absorbancia_amostra { get; set; }
            public string maximo_permitido { get; set; }
            public float carta_absorbancia { get; set; }
            public float carta_concentracao { get; set; }
            public string? mat_prima1 { get; set; }
            public string? mat_lote1 { get; set; }
            public DateOnly mat_validade1 { get; set; }
            public string? mat_prima2 { get; set; }
            public string? mat_lote2 { get; set; }
            public DateOnly mat_validade2 { get; set; }
            public string? mat_prima3 { get; set; }
            public string? mat_lote3 { get; set; }
            public DateOnly mat_validade3 { get; set; }
            public string? mat_prima4 { get; set; }
            public string? mat_lote4 { get; set; }
            public DateOnly mat_validade4 { get; set; }
            public string? mat_prima5 { get; set; }
            public string? mat_lote5 { get; set; }
            public DateOnly mat_validade5 { get; set; }
            public string? equi_de { get; set; }
            public string? equi_ee { get; set; }
            public string? observacoes { get; set; }
            public string? executado { get; set; }
            public string? auxiliado { get; set; }


        }

        public class ArlaInsoluveis
        {
            [Key]
            public int id { get; set; }
            public string os { get; set; }
            public string orcamento { get; set; }
            public DateOnly data_ini { get; set; }
            public DateOnly data_term { get; set; }
            public string? norma { get; set; }
            public string? np { get; set; }
            public string? descricao { get; set; }
            public float preparacao_placa1 { get; set; }
            public float preparacao_filtro1 { get; set; }
            public float preparacao_massa1 { get; set; }
            public float preparacao_placa2 { get; set; }
            public float preparacao_filtro2 { get; set; }
            public float preparacao_massa2 { get; set; }
            public float pesagem_amostra1 { get; set; }
            public float pesagem_placa1 { get; set; }
            public float pesagem_filtro1 { get; set; }
            public float pesagem_resultado1 { get; set; }
            public float pesagem_amostra2 { get; set; }
            public float pesagem_placa2 { get; set; }
            public float pesagem_filtro2 { get; set; }
            public float pesagem_resultado2 { get; set; }
            public string? pesagem_media { get; set; }
            public float pesagem_resultfinal { get; set; }
            public string? mat_prima1 { get; set; }
            public string? mat_lote1 { get; set; }
            public DateOnly mat_validade1 { get; set; }
            public string? mat_prima2 { get; set; }
            public string? mat_lote2 { get; set; }
            public DateOnly mat_validade2 { get; set; }
            public string? equi_ee { get; set; }
            public string? equi_de { get; set; }
            public string? observacoes { get; set; }
            public string? executado { get; set; }
            public string? auxiliado { get; set; }


        }

        public class ArlaMetais
        {
            [Key]
            public int id { get; set; }
            public string os { get; set; }
            public string? rev { get; set; }
            public string orcamento { get; set; }
            public DateOnly data_ini { get; set; }
            public DateOnly data_term { get; set; }
            public string? norma { get; set; }
            public string? np { get; set; }
            public string? descricao { get; set; }
            public float ex_lq_al { get; set; }
            public float ex_lq_ca { get; set; }
            public float ex_lq_cr { get; set; }
            public float ex_lq_cu { get; set; }
            public float ex_lq_fe { get; set; }
            public float ex_lq_k { get; set; }
            public float ex_lq_mg { get; set; }
            public float ex_lq_na { get; set; }
            public float ex_lq_ni { get; set; }
            public float ex_lq_zn { get; set; }
            public float ex_lim_al { get; set; }
            public float ex_lim_ca { get; set; }
            public float ex_lim_cr { get; set; }
            public float ex_lim_cu { get; set; }
            public float ex_lim_fe { get; set; }
            public float ex_lim_k { get; set; }
            public float ex_lim_mg { get; set; }
            public float ex_lim_na { get; set; }
            public float ex_lim_ni { get; set; }
            public float ex_lim_zn { get; set; }
            public string ex_quant_al1 { get; set; }
            public string ex_quant_ca1 { get; set; }
            public string ex_quant_cr1 { get; set; }
            public string ex_quant_cu1 { get; set; }
            public string ex_quant_fe1 { get; set; }
            public string ex_quant_k1 { get; set; }
            public string ex_quant_mg1 { get; set; }
            public string ex_quant_na1 { get; set; }
            public string ex_quant_ni1 { get; set; }
            public string ex_quant_zn1 { get; set; }
            public string ex_quant_al2 { get; set; }
            public string ex_quant_ca2 { get; set; }
            public string ex_quant_cr2 { get; set; }
            public string ex_quant_cu2 { get; set; }
            public string ex_quant_fe2 { get; set; }
            public string ex_quant_k2 { get; set; }
            public string ex_quant_mg2 { get; set; }
            public string ex_quant_na2 { get; set; }
            public string ex_quant_ni2 { get; set; }
            public string ex_quant_zn2 { get; set; }
            public string ex_quant_al3 { get; set; }
            public string ex_quant_ca3 { get; set; }
            public string ex_quant_cr3 { get; set; }
            public string ex_quant_cu3 { get; set; }
            public string ex_quant_fe3 { get; set; }
            public string ex_quant_k3 { get; set; }
            public string ex_quant_mg3 { get; set; }
            public string ex_quant_na3 { get; set; }
            public string ex_quant_ni3 { get; set; }
            public string ex_quant_zn3 { get; set; }
            public string result_al { get; set; }
            public string result_ca { get; set; }
            public string result_cr { get; set; }
            public string result_cu { get; set; }
            public string result_fe { get; set; }
            public string result_k { get; set; }
            public string result_mg { get; set; }
            public string result_na { get; set; }
            public string result_ni { get; set; }
            public string result_zn { get; set; }
            public string? mat_prima1 { get; set; }
            public string? mat_lote1 { get; set; }
            public DateOnly mat_val1 { get; set; }
            public string? mat_prima2 { get; set; }
            public string? matt_lote2 { get; set; }
            public DateOnly mat_val2 { get; set; }
            public string? mat_prima3 { get; set; }
            public string? mat_lote3 { get; set; }
            public DateOnly mat_val3 { get; set; }
            public string? mat_prima4 { get; set; }
            public string? mat_lote4 { get; set; }
            public DateOnly mat_val4 { get; set; }
            public string? mat_prima5 { get; set; }
            public string? mat_lote5 { get; set; }
            public DateOnly mat_val5 { get; set; }
            public string? mat_prima6 { get; set; }
            public string? mat_lote6 { get; set; }
            public DateOnly mat_val6 { get; set; }
            public string? mat_prima7 { get; set; }
            public string? mat_lote7 { get; set; }
            public DateOnly mat_val7 { get; set; }
            public string? mat_prima8 { get; set; }
            public string? mat_lote8 { get; set; }
            public DateOnly mat_val8 { get; set; }
            public string? mat_prima9 { get; set; }
            public string? mat_lote9 { get; set; }
            public DateOnly mat_val9 { get; set; }
            public string? mat_prima10 { get; set; }
            public string? mat_lote10 { get; set; }
            public DateOnly mat_val10 { get; set; }
            public string? mat_prima11 { get; set; }
            public string? mat_lote11 { get; set; }
            public DateOnly mat_val11 { get; set; }
            public string? mat_prima12 { get; set; }
            public string? mat_lote12 { get; set; }
            public DateOnly mat_val12 { get; set; }
            public string? mat_prima13 { get; set; }

            public string? mat_lote13 { get; set; }
            public DateOnly mat_val13 { get; set; }
            public string? inst_desc1 { get; set; }
            public string? inst_cod1 { get; set; }
            public DateOnly inst_val1 { get; set; }
            public string? inst_desc2 { get; set; }
            public string? inst_cod2 { get; set; }
            public DateOnly inst_val2 { get; set; }
            public string? inst_desc3 { get; set; }
            public string? inst_cod3 { get; set; }
            public DateOnly inst_val3 { get; set; }
            public string? inst_desc4 { get; set; }
            public string? inst_cod4 { get; set; }
            public DateOnly inst_val4 { get; set; }
            public string? inst_desc5 { get; set; }
            public string? inst_cod5 { get; set; }
            public DateOnly inst_val5 { get; set; }
            public string? inst_desc6 { get; set; }
            public string? inst_cod6 { get; set; }
            public DateOnly inst_val6 { get; set; }
            public string? inst_desc7 { get; set; }
            public string? inst_cod7 { get; set; }
            public DateOnly inst_val7 { get; set; }
            public string? inst_desc8 { get; set; }
            public string? inst_cod8 { get; set; }
            public DateOnly inst_val8 { get; set; }
            public string? inst_desc9 { get; set; }
            public string? inst_cod9 { get; set; }
            public DateOnly inst_val9 { get; set; }
            public string? inst_desc10 { get; set; }
            public string? inst_cod10 { get; set; }
            public DateOnly inst_val10 { get; set; }
            public string? inst_desc11 { get; set; }
            public string? inst_cod11 { get; set; }
            public DateOnly inst_val11 { get; set; }
            public string? inst_dec12 { get; set; }
            public string? inst_cod12 { get; set; }
            public DateOnly inst_val12 { get; set; }
            public string? inst_desc13 { get; set; }
            public string? inst_cod13 { get; set; }
            public DateOnly inst_val13 { get; set; }
            public string? inst_desc14 { get; set; }
            public string? inst_cod14 { get; set; }
            public DateOnly inst_val14 { get; set; }
            public string? inst_desc15 { get; set; }
            public string? inst_cod15 { get; set; }
            public DateOnly inst_val15 { get; set; }
            public string? equi_ee { get; set; }
            public string? equi_de { get; set; }
            public string? observacoes { get; set; }
            public string? executado { get; set; }
            public string? auxiliado { get; set; }
        }

        public class MetaisTratamento
        {
            [Key]
            public int id { get; set; }
            public string os { get; set; }
            public string orcamento { get; set; }
            public string? rev { get; set; }
            public float branco_al { get; set; }
            public float branco_ca { get; set; }
            public float branco_cr { get; set; }
            public float branco_cu { get; set; }
            public float branco_fe { get; set; }
            public float branco_k { get; set; }
            public float branco_mg { get; set; }
            public float branco_na { get; set; }
            public float branco_ni { get; set; }
            public float branco_zn { get; set; }
            public float resul_ob_al1 { get; set; }
            public float resul_ob_ca1 { get; set; }
            public float resul_ob_cr1 { get; set; }
            public float resul_ob_cu1 { get; set; }
            public float resul_ob_fe1 { get; set; }
            public float resul_ob_k1 { get; set; }
            public float resul_ob_mg1 { get; set; }
            public float resul_ob_na1 { get; set; }
            public float resul_ob_ni1 { get; set; }
            public float resul_ob_zn1 { get; set; }
            public float resul_ob_al2 { get; set; }
            public float resul_ob_ca2 { get; set; }
            public float resul_ob_cr2 { get; set; }
            public float resul_ob_cu2 { get; set; }
            public float resul_ob_fe2 { get; set; }
            public float resul_ob_k2 { get; set; }
            public float resul_ob_mg2 { get; set; }
            public float resul_ob_na2 { get; set; }
            public float resul_ob_ni2 { get; set; }
            public float resul_ob_zn2 { get; set; }
            public float resul_ob_al3 { get; set; }
            public float resul_ob_ca3 { get; set; }
            public float resul_ob_cr3 { get; set; }
            public float resul_ob_cu3 { get; set; }
            public float resul_ob_fe3 { get; set; }
            public float resul_ob_k3 { get; set; }
            public float resul_ob_mg3 { get; set; }
            public float resul_ob_na3 { get; set; }
            public float resul_ob_ni3 { get; set; }
            public float resul_ob_zn3 { get; set; }
            public float resul_con_al1 { get; set; }
            public float resul_con_ca1 { get; set; }
            public float resul_con_cr1 { get; set; }
            public float resul_con_cu1 { get; set; }
            public float resul_con_fe1 { get; set; }
            public float resul_con_k1 { get; set; }
            public float resul_con_mg1 { get; set; }
            public float resul_con_na1 { get; set; }
            public float resul_con_ni1 { get; set; }
            public float resul_con_zn1 { get; set; }
            public float resul_con_al2 { get; set; }
            public float resul_con_ca2 { get; set; }
            public float resul_con_cr2 { get; set; }
            public float resul_con_cu2 { get; set; }
            public float resul_con_fe2 { get; set; }
            public float resul_con_k2 { get; set; }
            public float resul_con_mg2 { get; set; }
            public float resul_con_na2 { get; set; }
            public float resul_con_ni2 { get; set; }
            public float resul_con_zn2 { get; set; }
            public float resul_con_al3 { get; set; }
            public float resul_con_ca3 { get; set; }
            public float resul_con_cr3 { get; set; }
            public float resul_con_cu3 { get; set; }
            public float resul_con_fe3 { get; set; }
            public float resul_con_k3 { get; set; }
            public float resul_con_mg3 { get; set; }
            public float resul_con_na3 { get; set; }
            public float resul_con_ni3 { get; set; }
            public float resul_con_zn3 { get; set; }
            public string? observacoes { get; set; }
            public string? executado { get; set; }
            public string? auxiliado { get; set; }
        }

        public class ArlaFosfato
        {
            [Key]
            public int Id { get; set; }
            public string os { get; set; }
            public string? rev { get; set; }
            public string orcamento { get; set; }
            public DateOnly data_ini { get; set; }
            public DateOnly data_term { get; set; }
            public string? cod_curva { get; set; }
            public float fator_calibracao { get; set; }
            public float massa { get; set; }
            public float absorvancia1 { get; set; }
            public float absorvancia2 { get; set; }
            public string? result_final { get; set; }
            public float abs_branco { get; set; }
            public float abs_qc { get; set; }
            public string? concentracao_qc { get; set; }
            public string? mat_prima_1 { get; set; }
            public string? mat_lote_1 { get; set; }
            public DateOnly mat_validade_1 { get; set; }
            public string? mat_prima_2 { get; set; }
            public string? mat_lote_2 { get; set; }
            public DateOnly mat_validade_2 { get; set; }
            public string? mat_prima_3 { get; set; }
            public string? mat_lote_3 { get; set; }
            public DateOnly mat_validade_3 { get; set; }
            public string? mat_prima_4 { get; set; }
            public string? mat_lote_4 { get; set; }
            public DateOnly mat_validade_4 { get; set; }
            public string? mat_prima_5 { get; set; }
            public string? mat_lote_5 { get; set; }
            public DateOnly mat_validade_5 { get; set; }
            public string? mat_prima_6 { get; set; }
            public string? mat_lote_6 { get; set; }
            public DateOnly mat_validade_6 { get; set; }
            public string? mat_prima_7 { get; set; }
            public string? mat_lote_7 { get; set; }
            public DateOnly mat_validade_7 { get; set; }
            public string? mat_prima_8 { get; set; }
            public string? mat_lote_8 { get; set; }
            public DateOnly mat_validade_8 { get; set; }
            public string? equi_ee { get; set; }
            public string? equi_de { get; set; }
            public string? obs { get; set; }
            public string? executado_por { get; set; }
            public string? auxiliado_por { get; set; }
        }

        public class ArlaDensidade
        {
            [key]
            public int Id { get; set; }
            [Key]
            public string os { get; set; }
            public string? rev { get; set; }
            public string orcamento { get; set; }
            public DateOnly data_ini { get; set; }
            public DateOnly data_term { get; set; }
            public string? temp_inicial { get; set; }
            public string? densidade_enc { get; set; }
            public string? temp_final { get; set; }
            public string? densidade_ambiente { get; set; }
            public string? densidade_banho { get; set; }
            public string? conc_ensaio { get; set; }
            public string   ? mat_prima { get; set; }
            public string? mat_lote { get; set; }
            public DateOnly mat_validade { get; set; }
            public string? inst_desc1 { get; set; }
            public string? inst_cod1 { get; set; }
            public DateOnly inst_data1 { get; set; }
            public string? inst_desc2 { get; set; }
            public string? inst_cod2 { get; set; }
            public DateOnly inst_data2 { get; set; }
            public string? inst_desc3 { get; set; }
            public string   ? inst_cod3 { get; set; }
            public DateOnly inst_data3 { get; set; }
            public string? inst_desc4 { get; set; }
            public string   ? inst_cod4 { get; set; }
            public DateOnly inst_data4 { get; set; }
            public string? equi_de { get; set; }
            public string? equi_ee { get; set; }
            public string? obs { get; set; }
            public string? executado_por { get; set; }
            public string? auxiliado_por { get; set; }
            public string? exec_temp_ini { get; set; }
            public string? tem_amostra { get; set; }
            public string? exec_densi_encont { get; set; }
            public string? exec_temp_final { get; set; }
        }
    }
}
