using Arla32.Data;
using Arla32.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using NuGet.Versioning;
using System.Data.Entity;
using System.Security.Cryptography.Xml;
using static Arla32.Models.ColetaModel;

namespace Arla32.Controllers
{
    [Authorize]
    public class ColetaController : Controller
    {
        private readonly ILogger<ColetaController> _logger;
        private readonly QuimicoContext _qcontext;


        public ColetaController(ILogger<ColetaController> logger, QuimicoContext qcontext)
        {
            _logger = logger;
            _qcontext = qcontext;

        }
        public IActionResult EnsaioConcentracao(string OS, string orcamento)
        {
            var dados = _qcontext.arla_concentracao_indice.Where(x => x.os == OS).FirstOrDefault();
            if (dados == null)
            {
                ViewBag.OS = OS;
                ViewBag.orcamento = orcamento;


                return View();
            }
            else
            {
                ViewBag.OS = OS;
                ViewBag.orcamento = orcamento;
                return View(dados);
            }
        }
        public IActionResult EnsaioAldeidos(string OS, string orcamento)
        {
            ViewBag.OS = OS;
            ViewBag.orcamento = orcamento;

            return View();
        }
        public IActionResult EnsaioDensidade(string OS, string orcamento)
        {
            var dados = _qcontext.arla_densidade.Where(x => x.os == OS && x.orcamento == orcamento).FirstOrDefault();
            if (dados == null)
            {
                ViewBag.OS = OS;
                ViewBag.orcamento = orcamento;

                return View();
            }
            else
            {
                ViewBag.OS = OS;
                ViewBag.orcamento = orcamento;
                return View(dados);
            }
            
        }
        public IActionResult EnsaioAlcalinidade(string OS, string orcamento)
        {
            ViewBag.OS = OS;
            ViewBag.orcamento = orcamento;

            return View();
        }
        public IActionResult EnsaioInsoluveis(string OS, string orcamento)
        {
            ViewBag.OS = OS;
            ViewBag.orcamento = orcamento;

            return View();
        }
        public IActionResult EnsaioFosfato(string OS, string orcamento)
        {
            ViewBag.OS = OS;
            ViewBag.orcamento = orcamento;

            return View();
        }
        public IActionResult EnsaioIdentidade(string OS, string orcamento)
        {
            ViewBag.OS = OS;
            ViewBag.orcamento = orcamento;

            return View();
        }
        public IActionResult EnsaioBiureto(string OS, string orcamento)
        {
            var dados = _qcontext.arla_biureto.Where(x => x.os == OS && x.orcamento == orcamento).FirstOrDefault();
            if (dados == null)
            {
                ViewBag.OS = OS;
                ViewBag.orcamento = orcamento;
                return View();
            }
            else
            {
                ViewBag.OS = OS;
                ViewBag.orcamento = orcamento;
                return View(dados);
            }
        }
        public IActionResult EnsaioMetais(string OS, string orcamento)
        {
            ViewBag.OS = OS;
            ViewBag.orcamento = orcamento;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SalvarConcentracao(string OS, string orcamento, string osConcentracao, [Bind("data_ini,data_term,lote_solucao,codigo_curva,fator_avaliacao,indice_agua,refracao_amostra1,refracao_amostra2,conc_ureia,desc1_instrumento,codigo1_instrumento,validade1_instrumento,desc2_instrumento,codigo2_instrumento,validade2_instrumento,ee_equipamento,de_equipamento,obs,executado_por,auxiliado_por")] ColetaModel.ArlaConcentracao salvarDados)
        {
            //pegando os valores dos inputs no html.
            DateTime data_ini = salvarDados.data_ini;
            DateTime data_term = salvarDados.data_term;
            string lote_solucao = salvarDados.lote_solucao;
            string codigo_curva = salvarDados.codigo_curva;
            float fator_avaliacao = salvarDados.fator_avaliacao;
            float indice_agua = salvarDados.indice_agua;
            float refracao_amostra1 = salvarDados.refracao_amostra1;
            float refracao_amostra2 = salvarDados.refracao_amostra2;
            string desc1_instrumento = salvarDados.desc1_instrumento;
            string codigo1_instrumento = salvarDados.codigo1_instrumento;
            DateTime validade1_instrumento = salvarDados.validade1_instrumento;
            string desc2_instrumento = salvarDados.desc2_instrumento;
            string codigo2_instrumento = salvarDados.codigo2_instrumento;
            DateTime validade2_instrumento = salvarDados.validade2_instrumento;
            string ee_equipamento = salvarDados.ee_equipamento;
            string de_equipamento = salvarDados.de_equipamento;
            string obs = salvarDados.obs;
            string executado_por = salvarDados.executado_por;
            string auxiliado_por = salvarDados.auxiliado_por;

            //Contas
            //pegar o concentracao de Birueto do anexo E

            osConcentracao = OS;
            var pegarValores = _qcontext.arla_biureto
                    .Where(os => os.os == osConcentracao)
                     .Select(os => new
                     {
                         os.result_media

                     })
                     .FirstOrDefault();

            //connta do concentracao de ureia 
            var valor = pegarValores.result_media.TrimEnd('%').Trim();
            float concentracao_biureto = float.Parse(valor);
            float concentracao_ureia = (((((refracao_amostra1 + refracao_amostra2) / 2) - indice_agua) * fator_avaliacao) - concentracao_biureto);
            double concentracao_ureia_arredendodado = Math.Round(concentracao_ureia, 1);

            //salvando no banco de dados.
            var guardarDadosTabela = new ColetaModel.ArlaConcentracao
            {
                os = OS,
                orcamento = orcamento,
                data_ini = data_ini,
                data_term = data_term,
                lote_solucao = lote_solucao,
                codigo_curva = codigo_curva,
                fator_avaliacao = fator_avaliacao,
                indice_agua = indice_agua,
                refracao_amostra1 = refracao_amostra1,
                refracao_amostra2 = refracao_amostra2,
                conc_biureto = pegarValores.result_media,
                conc_ureia = concentracao_ureia.ToString() + " %",
                desc1_instrumento = desc1_instrumento,
                codigo1_instrumento = codigo1_instrumento,
                validade1_instrumento = validade1_instrumento,
                desc2_instrumento = desc2_instrumento,
                codigo2_instrumento = codigo2_instrumento,
                validade2_instrumento = validade2_instrumento,
                ee_equipamento = ee_equipamento,
                de_equipamento = de_equipamento,
                obs = obs,
                executado_por = executado_por,
                auxiliado_por = auxiliado_por,
            };

            //_qcontext.Add(guardarDadosTabela);
            //await _qcontext.SaveChangesAsync();
            TempData["Mensagem"] = "salvo com Sucesso.";

            return RedirectToAction(nameof(EnsaioConcentracao), new { OS, orcamento });

        }

        [HttpPost]
        public async Task<IActionResult> SalvarAlcalinidade(string OS, string orcamento, [Bind("data_ini,data_term,pre_massa_amostra,pre_vol_titulado,det_massa_amostra1,det_massa_amostra2,det_vol_titulado1,det_vol_titulado2,mat_prima,mat_lote,mat_validade,inst_desc1,inst_cod1,inst_data1,inst_desc1_1,inst_cod1_1,inst_data1_1,inst_desc2,inst_cod2,inst_data2,inst_desc2_2,inst_cod2_2,inst_data2_2,inst_desc3,inst_cod3,inst_data3,inst_desc3_3,inst_cod3_3,inst_data3_3,inst_desc4,inst_cod4,inst_data4,inst_desc4_4,inst_cod4_4,inst_data4_4,equip_de,equip_ee,obs,executado_por,auxiliado_por")] ColetaModel.ArlaAlcalinidade salvarDados)
        {
            try
            {
                //pegando os valores vindo dos inputs no html.
                DateTime data_ini = salvarDados.data_ini;
                DateTime data_term = salvarDados.data_term;
                string pre_massa_amostra = salvarDados.pre_massa_amostra;
                string pre_vol_titulado = salvarDados.pre_vol_titulado;
                string det_massa_amostra1 = salvarDados.det_massa_amostra1;
                string det_massa_amostra2 = salvarDados.det_massa_amostra2;
                string det_vol_titulado1 = salvarDados.det_vol_titulado1;
                string det_vol_titulado2 = salvarDados.det_vol_titulado2;
                string mat_prima = salvarDados.mat_prima;
                string mat_lote = salvarDados.mat_lote;
                DateTime mat_validade = salvarDados.mat_validade;
                string inst_desc1 = salvarDados.inst_desc1;
                string inst_cod1 = salvarDados.inst_cod1;
                DateTime inst_data1 = salvarDados.inst_data1;
                string inst_desc1_1 = salvarDados.inst_desc1_1;
                string inst_cod1_1 = salvarDados.inst_cod1_1;
                DateTime inst_data1_1 = salvarDados.inst_data1_1;
                string inst_desc2 = salvarDados.inst_desc2;
                string inst_cod2 = salvarDados.inst_cod2;
                DateTime inst_data2 = salvarDados.inst_data2;
                string inst_desc2_2 = salvarDados.inst_desc2_2;
                string inst_cod2_2 = salvarDados.inst_cod2_2;
                DateTime inst_data2_2 = salvarDados.inst_data2_2;
                string inst_desc3 = salvarDados.inst_desc3;
                string inst_cod3 = salvarDados.inst_cod3;
                DateTime inst_data3 = salvarDados.inst_data3;
                string inst_desc3_3 = salvarDados.inst_desc3_3;
                string inst_cod3_3 = salvarDados.inst_cod3_3;
                DateTime inst_data3_3 = salvarDados.inst_data3_3;
                string inst_desc4 = salvarDados.inst_desc4;
                string inst_cod4 = salvarDados.inst_cod4;
                DateTime inst_data4 = salvarDados.inst_data4;
                string inst_desc4_4 = salvarDados.inst_desc4_4;
                string inst_cod4_4 = salvarDados.inst_cod4_4;
                DateTime inst_data4_4 = salvarDados.inst_data4_4;
                string equip_de = salvarDados.equip_de;
                string equip_ee = salvarDados.equip_ee;
                string obs = salvarDados.obs;
                string executado_por = salvarDados.executado_por;
                string auxiliado_por = salvarDados.auxiliado_por;

                // conta do Resultado Final - Tabela Procedimento Ensaio preliminar

                double pre_vol = double.Parse(pre_vol_titulado);
                double pre_massa = double.Parse(pre_massa_amostra);
                var pre_resultado_final = ((pre_vol * 0.017) / pre_massa);
                double pre_resultado_arredondado = Math.Round(pre_resultado_final, 2);

                // conta do Resultado Final - Tabela Procedimento Ensaio determinação

                double det_massa = double.Parse(det_massa_amostra2);
                double de_vol = double.Parse(det_vol_titulado2);
                var det_res_final = ((de_vol * 0.017) / det_massa);
                double det_res_arredondado = Math.Round(det_res_final, 2);


                // salvando no banco de dados.
                var guardarDadosTabela = new ArlaAlcalinidade
                {
                    os = OS,
                    orcamento = orcamento,
                    data_ini = data_ini,
                    data_term = data_term,
                    pre_massa_amostra = pre_massa_amostra + " g",
                    pre_vol_titulado = pre_vol_titulado + " mL",
                    pre_resultado_final = pre_resultado_arredondado.ToString() + " %",
                    det_massa_amostra1 = det_massa_amostra1 + " g",
                    det_massa_amostra2 = det_massa_amostra2 + " g",
                    det_vol_titulado1 = det_vol_titulado1 + " mL",
                    det_vol_titulado2 = det_vol_titulado2,
                    det_res_final = det_res_arredondado.ToString("") + " %",
                    mat_prima = mat_prima,
                    mat_lote = mat_lote,
                    mat_validade = mat_validade,
                    inst_desc1 = inst_desc1,
                    inst_cod1 = inst_cod1,
                    inst_data1 = inst_data1,
                    inst_desc1_1 = inst_desc1_1,
                    inst_cod1_1 = inst_cod1_1,
                    inst_data1_1 = inst_data1_1,
                    inst_desc2 = inst_desc2,
                    inst_cod2 = inst_cod2,
                    inst_data2 = inst_data2,
                    inst_desc2_2 = inst_desc2_2,
                    inst_cod2_2 = inst_cod2_2,
                    inst_data2_2 = inst_data2_2,
                    inst_desc3 = inst_desc3,
                    inst_cod3 = inst_cod3,
                    inst_data3 = inst_data3,
                    inst_desc3_3 = inst_desc3_3,
                    inst_cod3_3 = inst_cod3_3,
                    inst_data3_3 = inst_data3_3,
                    inst_desc4 = inst_desc4,
                    inst_cod4 = inst_cod4,
                    inst_data4 = inst_data4,
                    inst_desc4_4 = inst_desc4_4,
                    inst_cod4_4 = inst_cod4_4,
                    inst_data4_4 = inst_data4_4,
                    equip_de = equip_de,
                    equip_ee = equip_ee,
                    obs = obs,
                    executado_por = executado_por,
                    auxiliado_por = auxiliado_por,
                };

                _qcontext.Add(guardarDadosTabela);
                await _qcontext.SaveChangesAsync();
                TempData["Mensagem"] = "salvo com Sucesso.";
                return RedirectToAction(nameof(EnsaioAlcalinidade), new { OS, orcamento });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SalvarBirueto(string OS, string orcamento, [Bind("data_ini,data_term,codigo_curva,fator_calibracao,fator_dia,amostra1,amostra2,absorbancia_1,absorbancia_2,absorbancia_3,mat_prima_1,mat_lote_1,mat_validade_1,mat_prima_2,mat_lote_2,mat_validade_2,mat_prima_3,mat_lote_3,mat_validade_3,mat_prima_4,mat_lote_4,mat_validade_4,mat_prima_5,mat_lote_5,mat_validade_5,inst_desc1,inst_codigo1,inst_data1,inst_desc1_1,inst_codigo1_1,inst_data1_1,insta_desc2,inst_codigo2,inst_data2,inst_desc2_2,inst_codigo2_2,inst_data2_2,inst_desc3,inst_codigo3,inst_data3,inst_desc3_3,inst_codigo3_3,inst_data3_3,inst_desc4,inst_codigo4,inst_data4,inst_desc4_4,inst_codigo4_4,inst_data4_4,equip_de,equip_ee,obs,executado_por,auxiliado_por")] ColetaModel.ArlaBiureto salvarDados)
        {
            try
            {

                if ((OS != null && OS != "0") || (orcamento != null && orcamento != "0"))
                {
                    //pegando os valores que recebe nos inputs do html.
                    DateTime data_ini = salvarDados.data_ini;
                    DateTime data_term = salvarDados.data_term;
                    string codigo_curva = salvarDados.codigo_curva;
                    float fator_calibracao = salvarDados.fator_calibracao;
                    float fator_dia = salvarDados.fator_dia;
                    float amostra1 = salvarDados.amostra1;
                    float amostra2 = salvarDados.amostra2;
                    float absorbancia_1 = salvarDados.absorbancia_1;
                    float absorbancia_2 = salvarDados.absorbancia_2;
                    float absorbancia_3 = salvarDados.absorbancia_3;
                    string mat_prima_1 = salvarDados.mat_prima_1;
                    string mat_lote_1 = salvarDados.mat_lote_1;
                    DateTime mat_validade_1 = salvarDados.mat_validade_1;
                    string mat_prima_2 = salvarDados.mat_prima_2;
                    string mat_lote_2 = salvarDados.mat_lote_2;
                    DateTime mat_validade_2 = salvarDados.mat_validade_2;
                    string mat_prima_3 = salvarDados.mat_prima_3;
                    string mat_lote_3 = salvarDados.mat_lote_3;
                    DateTime mat_validade_3 = salvarDados.mat_validade_3;
                    string mat_prima_4 = salvarDados.mat_prima_4;
                    string mat_lote_4 = salvarDados.mat_lote_4;
                    DateTime mat_validade_4 = salvarDados.mat_validade_4;
                    string mat_prima_5 = salvarDados.mat_prima_5;
                    string mat_lote_5 = salvarDados.mat_lote_5;
                    DateTime mat_validade_5 = salvarDados.mat_validade_5;
                    string inst_desc1 = salvarDados.inst_desc1;
                    string inst_codigo1 = salvarDados.inst_codigo1;
                    DateTime inst_data1 = salvarDados.inst_data1;
                    string inst_desc1_1 = salvarDados.inst_desc1_1;
                    string inst_codigo1_1 = salvarDados.inst_codigo1_1;
                    DateTime inst_data1_1 = salvarDados.inst_data1_1;
                    string insta_desc2 = salvarDados.insta_desc2;
                    string inst_codigo2 = salvarDados.inst_codigo2;
                    DateTime inst_data2 = salvarDados.inst_data2;
                    string inst_desc2_2 = salvarDados.inst_desc2_2;
                    string inst_codigo2_2 = salvarDados.inst_codigo2_2;
                    DateTime inst_data2_2 = salvarDados.inst_data2_2;
                    string inst_desc3 = salvarDados.inst_desc3;
                    string inst_codigo3 = salvarDados.inst_codigo3;
                    DateTime inst_data3 = salvarDados.inst_data3;
                    string inst_desc3_3 = salvarDados.inst_desc3_3;
                    string inst_codigo3_3 = salvarDados.inst_codigo3_3;
                    DateTime inst_data3_3 = salvarDados.inst_data3_3;
                    string inst_desc4 = salvarDados.inst_desc4;
                    string inst_codigo4 = salvarDados.inst_codigo4;
                    DateTime inst_data4 = salvarDados.inst_data4;
                    string inst_desc4_4 = salvarDados.inst_desc4_4;
                    string inst_codigo4_4 = salvarDados.inst_codigo4_4;
                    DateTime inst_data4_4 = salvarDados.inst_data4_4;
                    string equip_de = salvarDados.equip_de;
                    string equip_ee = salvarDados.equip_ee;
                    string obs = salvarDados.obs;
                    string auxiliado_por = salvarDados.auxiliado_por;
                    string executado_por = salvarDados.executado_por;

                    //if (string.IsNullOrEmpty(data_ini.ToString()) || string.IsNullOrEmpty(data_term.ToString()) || string.IsNullOrEmpty(codigo_curva) || fator_calibracao == 0 || fator_dia == 0 || amostra1 == 0 || amostra2 == 0 || absorbancia_1 == 0 || absorbancia_2 == 0 || absorbancia_3 == 0 || string.IsNullOrEmpty(mat_prima_1) || string.IsNullOrEmpty(mat_lote_1) || string.IsNullOrEmpty(mat_validade_1.ToString()) || string.IsNullOrEmpty(mat_prima_2) || string.IsNullOrEmpty(mat_lote_2) || string.IsNullOrEmpty(mat_validade_2.ToString()) || string.IsNullOrEmpty(mat_prima_3) || string.IsNullOrEmpty(mat_prima_3) || string.IsNullOrEmpty(mat_lote_3) || string.IsNullOrEmpty(mat_validade_3.ToString()) ||
                    //    string.IsNullOrEmpty(mat_prima_4) || string.IsNullOrEmpty(mat_lote_4) || string.IsNullOrEmpty(mat_validade_4.ToString()) || string.IsNullOrEmpty(mat_prima_5) || string.IsNullOrEmpty(mat_lote_5) || string.IsNullOrEmpty(mat_validade_5.ToString()) || string.IsNullOrEmpty(inst_desc1) || string.IsNullOrEmpty(inst_codigo1) || string.IsNullOrEmpty(inst_data1.ToString()) || string.IsNullOrEmpty(inst_desc1_1) || string.IsNullOrEmpty(inst_codigo1_1) || string.IsNullOrEmpty(inst_data1_1.ToString()) || string.IsNullOrEmpty(insta_desc2) || string.IsNullOrEmpty(inst_codigo2) || string.IsNullOrEmpty(inst_data2.ToString()) || string.IsNullOrEmpty(inst_desc2_2) || string.IsNullOrEmpty(inst_codigo2_2) || string.IsNullOrEmpty(inst_data2_2.ToString()) ||
                    //    string.IsNullOrEmpty(inst_desc3) || string.IsNullOrEmpty(inst_codigo3) || string.IsNullOrEmpty(inst_data3.ToString()) || string.IsNullOrEmpty(inst_desc3_3) || string.IsNullOrEmpty(inst_codigo3_3) || string.IsNullOrEmpty(inst_data3_3.ToString()) || string.IsNullOrEmpty(inst_desc4) || string.IsNullOrEmpty(inst_codigo4) || string.IsNullOrEmpty(inst_data4.ToString()) || string.IsNullOrEmpty(inst_desc4_4) || string.IsNullOrEmpty(inst_codigo4_4) || string.IsNullOrEmpty(inst_data4_4.ToString()))
                    //{
                    //    TempData["Mensagem"] = "Preencha Todos Os Campos.";
                    //    return RedirectToAction(nameof(EnsaioBiureto), new { OS, orcamento });
                    //}



                    var result_ind_1 = ((((absorbancia_2 - absorbancia_1) * fator_dia * 250) / (amostra1 * 10 * 1000)) * 100);
                    var result_ind_2 = ((((absorbancia_3 - absorbancia_1) * fator_dia * 250) / (amostra2 * 10 * 1000)) * 100);
                    var result_media = ((result_ind_1 + result_ind_2) / 2);

                    double result_ind_1_arredondado = Math.Round(result_ind_1, 2);
                    double result_ind_2_arredondado = Math.Round(result_ind_2, 2);
                    double result_media_2_arredondado = Math.Round(result_media, 2);

                    //passando os valores salvos nas variaveis para a tabela..
                    var salvarDadosTabela = new ColetaModel.ArlaBiureto
                    {
                        os = OS,
                        orcamento = orcamento,
                        data_ini = data_ini,
                        data_term = data_term,
                        codigo_curva = codigo_curva,
                        fator_calibracao = fator_calibracao,
                        fator_dia = fator_dia,
                        amostra1 = amostra1,
                        amostra2 = amostra2,
                        absorbancia_1 = absorbancia_1,
                        absorbancia_2 = absorbancia_2,
                        absorbancia_3 = absorbancia_3,
                        result_ind_1 = result_ind_1_arredondado.ToString() + " %",
                        result_ind_2 = result_ind_2_arredondado.ToString() + " %",
                        result_media = result_media_2_arredondado.ToString() + " %",
                        mat_prima_1 = mat_prima_1,
                        mat_lote_1 = mat_lote_1,
                        mat_validade_1 = mat_validade_1,
                        mat_prima_2 = mat_prima_2,
                        mat_lote_2 = mat_lote_2,
                        mat_validade_2 = mat_validade_2,
                        mat_prima_3 = mat_prima_3,
                        mat_lote_3 = mat_lote_3,
                        mat_validade_3 = mat_validade_3,
                        mat_prima_4 = mat_prima_4,
                        mat_lote_4 = mat_lote_4,
                        mat_validade_4 = mat_validade_4,
                        mat_prima_5 = mat_prima_5,
                        mat_lote_5 = mat_lote_5,
                        mat_validade_5 = mat_validade_5,
                        inst_desc1 = inst_desc1,
                        inst_codigo1 = inst_codigo1,
                        inst_data1 = inst_data1,
                        inst_desc1_1 = inst_desc1_1,
                        inst_codigo1_1 = inst_codigo1_1,
                        inst_data1_1 = inst_data1_1,
                        insta_desc2 = insta_desc2,
                        inst_codigo2 = inst_codigo2,
                        inst_data2 = inst_data2,
                        inst_desc2_2 = inst_desc2_2,
                        inst_codigo2_2 = inst_codigo2_2,
                        inst_data2_2 = inst_data2_2,
                        inst_desc3 = inst_desc3,
                        inst_codigo3 = inst_codigo3,
                        inst_data3 = inst_data3,
                        inst_desc3_3 = inst_desc3_3,
                        inst_codigo3_3 = inst_codigo3_3,
                        inst_data3_3 = inst_data3_3,
                        inst_desc4 = inst_desc4,
                        inst_codigo4 = inst_codigo4,
                        inst_data4 = inst_data4,
                        inst_desc4_4 = inst_desc4_4,
                        inst_codigo4_4 = inst_codigo4_4,
                        inst_data4_4 = inst_data4_4,
                        equip_de = equip_de,
                        equip_ee = equip_ee,
                        obs = obs,
                        auxiliado_por = auxiliado_por,
                        executado_por = executado_por,
                    };
                    //salvando no banco.
                    _qcontext.Add(salvarDadosTabela);
                    await _qcontext.SaveChangesAsync();

                    TempData["Mensagem"] = "Salvo Com Sucesso";
                    return RedirectToAction(nameof(EnsaioBiureto), new { OS, orcamento });
                }
                else
                {
                    TempData["Mensagem"] = "Desculpe, verifique a os e orcamento pela url se estão corretas.";
                    return RedirectToAction(nameof(EnsaioBiureto), new { OS, orcamento });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SalvarAldeidos(string OS, string orcamento, [Bind("data_ini,data_term,os, norma, np, descricao,col_norma,col_np,col_desc,lote_sol," +
            "codigo_curva,fator_calibracao,massa_branco,absorbancia_branco,maximo_permitido,massa_amostra, absorbancia_amostra, carta_absorbancia ,carta_concentracao,mat_prima1,mat_lote1,mat_validade1,mat_prima2, mat_lote2,mat_validade2,mat_prima3,mat_lote3,mat_validade3,mat_prima4,mat_lote4,mat_validade4," +
            "mat_prima5, mat_lote5,mat_validade5, inst_desc1,inst_validade1,inst_cod1, inst_desc2,inst_cod2,inst_validade2,inst_desc1_1,inst_cod1_1,inst_validade1_1,inst_desc2_2,inst_cod2_2,inst_validade2_2, equi_de, equi_ee, observacoes, executado, auxiliado "
              )] ColetaModel.ArlaAldeidos aldeidos)
        {
            try
            {
                //pegando os valores dos inputs no html.
                DateTime data_ini = aldeidos.data_ini;
                DateTime data_term = aldeidos.data_term;
                string rev = aldeidos.rev;
                string lote_sol = aldeidos.lote_sol;
                string codigo_curva = aldeidos.codigo_curva;
                float fator_calibracao = aldeidos.fator_calibracao;
                float massa_branco = aldeidos.massa_branco;
                float absorbancia_branco = aldeidos.absorbancia_branco;
                float massa_amostra = aldeidos.massa_amostra;
                float absorbancia_amostra = aldeidos.absorbancia_amostra;
                float carta_absorbancia = aldeidos.carta_absorbancia;
                string mat_prima1 = aldeidos.mat_prima1;
                string mat_lote1 = aldeidos.mat_lote1;
                string mat_validade1 = aldeidos.mat_validade1;
                string mat_prima2 = aldeidos.mat_prima2;
                string mat_lote2 = aldeidos.mat_lote2;
                string mat_validade2 = aldeidos.mat_validade2;
                string mat_prima3 = aldeidos.mat_prima3;
                string mat_lote3 = aldeidos.mat_lote3;
                string mat_validade3 = aldeidos.mat_validade3;
                string mat_prima4 = aldeidos.mat_prima4;
                string mat_lote4 = aldeidos.mat_lote4;
                string mat_validade4 = aldeidos.mat_validade4;
                string mat_prima5 = aldeidos.mat_prima5;
                string mat_lote5 = aldeidos.mat_lote5;
                string mat_validade5 = aldeidos.mat_validade5;
                string inst_desc1 = aldeidos.inst_desc1;
                string inst_cod1 = aldeidos.inst_cod1;
                string inst_validade1 = aldeidos.inst_validade1;
                string inst_desc2 = aldeidos.inst_desc2;
                string inst_cod2 = aldeidos.inst_cod2;
                string inst_validade2 = aldeidos.inst_validade2;
                string inst_desc1_1 = aldeidos.inst_desc1_1;
                string inst_cod1_1 = aldeidos.inst_cod1_1;
                string inst_validade1_1 = aldeidos.inst_validade1_1;
                string inst_desc2_2 = aldeidos.inst_desc2_2;
                string inst_cod2_2 = aldeidos.inst_cod2_2;
                string inst_validade2_2 = aldeidos.inst_validade2_2;
                string equi_de = aldeidos.equi_de;
                string equi_ee = aldeidos.equi_ee;
                string observacoes = aldeidos.observacoes;
                string executado = aldeidos.executado;
                string auxiliado = aldeidos.auxiliado;
                string norma = aldeidos.norma;
                string np = aldeidos.np;
                string descricao = aldeidos.descricao;


                if ( /*string.IsNullOrEmpty(data_ini.ToString()) ||*/
                    /* string.IsNullOrEmpty(data_term.ToString()) |*/
                    //string.IsNullOrEmpty(codigo_curva) ||
                    //string.IsNullOrEmpty(lote_sol) ||
                    //fator_calibracao == 0 ||
                    //massa_branco == 0 ||
                    //absorbancia_branco == 0 ||
                    //massa_amostra == 0 ||
                    //absorbancia_amostra == 0 ||
                    //carta_absorbancia == 0 ||
                    string.IsNullOrEmpty(mat_prima1) ||
                    string.IsNullOrEmpty(mat_prima2) ||
                    string.IsNullOrEmpty(mat_prima3) ||
                    string.IsNullOrEmpty(mat_prima4) ||
                    string.IsNullOrEmpty(mat_prima5) ||
                    string.IsNullOrEmpty(mat_lote1) ||
                    string.IsNullOrEmpty(mat_lote2) ||
                    string.IsNullOrEmpty(mat_lote3)
                    || string.IsNullOrEmpty(mat_lote4)
                    || string.IsNullOrEmpty(mat_lote5)
                    || string.IsNullOrEmpty(mat_validade1)
                    || string.IsNullOrEmpty(mat_validade2)
                    || string.IsNullOrEmpty(mat_validade3)
                    || string.IsNullOrEmpty(mat_validade4)
                    || string.IsNullOrEmpty(mat_validade5)
                    || string.IsNullOrEmpty(inst_cod1)
                    || string.IsNullOrEmpty(inst_cod2)
                    || string.IsNullOrEmpty(inst_desc1)
                    || string.IsNullOrEmpty(inst_desc2)
                    || string.IsNullOrEmpty(inst_validade1)
                    || string.IsNullOrEmpty(inst_validade2)
                    || string.IsNullOrEmpty(inst_desc1_1)
                    || string.IsNullOrEmpty(inst_desc2_2)
                    || string.IsNullOrEmpty(inst_validade1_1)
                    || string.IsNullOrEmpty(inst_validade2_2)
                    || string.IsNullOrEmpty(equi_ee)
                    || string.IsNullOrEmpty(equi_de)
                    || string.IsNullOrEmpty(observacoes)
                    || string.IsNullOrEmpty(executado)
                    || string.IsNullOrEmpty(auxiliado))

                {
                    TempData["Mensagem"] = "Preencha Todos Os Campos.";
                    return RedirectToAction(nameof(EnsaioAldeidos), new { OS, orcamento });
                }
                else
                {

                    //CONTA MÁXIMO PERMITIDO
                    var conta_max = (((absorbancia_amostra - absorbancia_branco) * fator_calibracao) / massa_amostra);
                    double conta_max_arre = Math.Round(conta_max);

                    //CONTA CONCENTRACAO QC

                    float carta_concentracao = (((carta_absorbancia - absorbancia_branco) * fator_calibracao) / massa_amostra);


                    var salvardados = new ColetaModel.ArlaAldeidos
                    {
                        os = OS,
                        orcamento = orcamento,
                        data_ini = data_ini,
                        data_term = data_term,
                        norma = norma,
                        np = np,
                        descricao = descricao,
                        rev = rev,
                        lote_sol = lote_sol,
                        codigo_curva = codigo_curva,
                        fator_calibracao = fator_calibracao,
                        massa_branco = massa_branco,
                        absorbancia_branco = absorbancia_branco,
                        massa_amostra = massa_amostra,
                        absorbancia_amostra = absorbancia_amostra,
                        maximo_permitido = conta_max_arre.ToString() + " mg/kg",
                        carta_absorbancia = carta_absorbancia,
                        carta_concentracao = carta_concentracao,
                        mat_prima1 = mat_prima1,
                        mat_lote1 = mat_lote1,
                        mat_validade1 = mat_validade1,
                        mat_prima2 = mat_prima2,
                        mat_lote2 = mat_lote2,
                        mat_validade2 = mat_validade2,
                        mat_prima3 = mat_prima3,
                        mat_lote3 = mat_lote3,
                        mat_validade3 = mat_validade3,
                        mat_prima4 = mat_prima4,
                        mat_lote4 = mat_lote4,
                        mat_validade4 = mat_validade4,
                        mat_prima5 = mat_prima5,
                        mat_lote5 = mat_lote5,
                        mat_validade5 = mat_validade5,
                        inst_desc1 = inst_desc1,
                        inst_cod1 = inst_cod1,
                        inst_validade1 = inst_validade1,
                        inst_desc2 = inst_desc2,
                        inst_cod2 = inst_cod2,
                        inst_validade2 = inst_validade2,
                        inst_desc1_1 = inst_desc1_1,
                        inst_cod1_1 = inst_cod1_1,
                        inst_validade1_1 = inst_validade1_1,
                        inst_desc2_2 = inst_desc2_2,
                        inst_cod2_2 = inst_cod2_2,
                        inst_validade2_2 = inst_validade2_2,
                        equi_de = equi_de,
                        equi_ee = equi_ee,
                        observacoes = observacoes,
                        executado = executado,
                        auxiliado = auxiliado,

                    };

                    //salvando no banco.
                    //_qcontext.Add(salvardados);
                    //await _qcontext.SaveChangesAsync();
                    TempData["Mensagem"] = "Salvo Com Sucesso";
                    return RedirectToAction(nameof(EnsaioAldeidos), new { OS, orcamento });

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SalvarInsoluveis(string OS, string orcamento, [Bind("data_ini, data_term, norma, np, descricao, preparacao_placa1,preparacao_filtro1,preparacao_massa1," +
            "preparacao_placa2,preparacao_filtro2, preparacao_massa2, pesagem_amostra1, pesagem_placa1,pesagem_filtro1, pesagem_resultado1," +
            "pesagem_amostra2,pesagem_placa2,pesagem_filtro2, pesagem_resultado2, pesagem_media,pesagem_resultfinal," +
            "mat_prima1,mat_lote1,mat_validade1,mat_prima2,mat_lote2,mat_validade2,inst_desc1,inst_cod1,inst_validade1," +
            "inst_desc2,inst_cod2,inst_validade2,inst_desc3,inst_cod3,inst_validade3,inst_desc4,inst_cod4,inst_validade4,inst_desc5," +
            "inst_cod5,inst_validade5,inst_desc1_1,inst_cod1_1,inst_validade1_1,inst_desc2_2,inst_cod2_2,inst_validade2_2," +
            "inst_desc3_3,inst_cod3_3,inst_validade3_3,inst_desc4_4,inst_cod4_4,inst_validade4_4,inst_desc5_5," +
            "inst_cod5_5,inst_validade5_5,equi_ee,equi_de,observacoes,executado,auxiliado ")] ColetaModel.ArlaInsoluveis insoluveis)
        {

            try
            {
                //pegando dados do html
                DateTime data_ini = insoluveis.data_ini; DateTime data_term = insoluveis.data_term; string norma = insoluveis.norma;
                string np = insoluveis.np; string descricao = insoluveis.descricao; float preparacao_placa1 = insoluveis.preparacao_placa1;
                float preparacao_filtro1 = insoluveis.preparacao_filtro1;
                float preparacao_placa2 = insoluveis.preparacao_placa2; float preparacao_filtro2 = insoluveis.preparacao_filtro2;
                float pesagem_amostra1 = insoluveis.pesagem_amostra1;
                float pesagem_placa1 = insoluveis.pesagem_placa1;
                float pesagem_resultado1 = insoluveis.pesagem_resultado1; float pesagem_amostra2 = insoluveis.pesagem_amostra2;
                float pesagem_placa2 = insoluveis.pesagem_placa2; float pesagem_resultado2 = insoluveis.pesagem_resultado2;
                string mat_prima1 = insoluveis.mat_prima1; string mat_lote1 = insoluveis.mat_lote1; string mat_validade1 = insoluveis.mat_validade1;
                string mat_prima2 = insoluveis.mat_prima2; string mat_lote2 = insoluveis.mat_lote2; string mat_validade2 = insoluveis.mat_validade2;
                string inst_desc1 = insoluveis.inst_desc1; string inst_cod1 = insoluveis.inst_cod1; string inst_validade1 = insoluveis.inst_validade1;
                string inst_desc2 = insoluveis.inst_desc2; string inst_cod2 = insoluveis.inst_cod2; string inst_validade2 = insoluveis.inst_validade2;
                string inst_desc3 = insoluveis.inst_desc3; string inst_cod3 = insoluveis.inst_cod3; string inst_validade3 = insoluveis.inst_validade3;
                string inst_desc4 = insoluveis.inst_desc4; string inst_cod4 = insoluveis.inst_cod4; string inst_validade4 = insoluveis.inst_validade4;
                string inst_desc5 = insoluveis.inst_desc5; string inst_cod5 = insoluveis.inst_cod5; string inst_validade5 = insoluveis.inst_validade5;
                string inst_desc1_1 = insoluveis.inst_desc1_1; string inst_cod1_1 = insoluveis.inst_cod1_1; string inst_validade1_1 = insoluveis.inst_validade1_1;
                string inst_desc2_2 = insoluveis.inst_desc2_2; string inst_cod2_2 = insoluveis.inst_cod2_2; string inst_validade2_2 = insoluveis.inst_validade2_2;
                string inst_desc3_3 = insoluveis.inst_desc3_3; string inst_cod3_3 = insoluveis.inst_cod3_3; string inst_validade3_3 = insoluveis.inst_validade3_3;
                string inst_desc4_4 = insoluveis.inst_desc4_4; string inst_cod4_4 = insoluveis.inst_cod4_4; string inst_validade4_4 = insoluveis.inst_validade4_4;
                string inst_desc5_5 = insoluveis.inst_desc5_5; string inst_cod5_5 = insoluveis.inst_desc5_5; string inst_validade5_5 = insoluveis.inst_validade5_5;
                string equi_ee = insoluveis.equi_ee; string equi_de = insoluveis.equi_de; string observacoes = insoluveis.observacoes; string executado = insoluveis.executado;
                string auxiliado = insoluveis.auxiliado;


                //CONTAS
                // Tabela preparação do filtro, conta do massa do filtro seco:

                var preparacao_massa1 = (preparacao_placa1 - preparacao_filtro1) * -1; // (multiplicamos por -1 pq o resultado das contas estava vindo como negativo
                var preparacao_massa2 = (preparacao_placa2 - preparacao_filtro2) * -1;


                // Tabela procedimento de pesagem, conta do filtro seco:
                var pesagem_filtro1 = pesagem_placa1 - preparacao_placa1;
                var pesagem_filtro2 = pesagem_placa2 - preparacao_placa2;

                // Tabela procedimento de pesagem, conta do resultado individual:
                pesagem_resultado1 = (((pesagem_filtro1 - preparacao_massa1) / pesagem_amostra1)) * 1000;
                pesagem_resultado2 = (((pesagem_filtro2 - preparacao_massa2) / pesagem_amostra2)) * 1000;

                // Tabela procedimento de pesagem, conta do média %:
                float valorAbsoluto = Math.Abs(pesagem_resultado1 - pesagem_resultado2);
                float maiorValor = Math.Max(pesagem_resultado1, pesagem_resultado2);
                float resultadoPercentual = (float)Math.Round((valorAbsoluto * 100 / maiorValor), 0);

                float pesagem_media = resultadoPercentual;  // Atribui o resultado ao float
                //resultado final
                float media = (pesagem_resultado1 + pesagem_resultado2) / 2;  // Calcula a média

                // Tabela procedimento de pesagem, conta do resultado final:
                float pesagem_resultfinal = media <= 10 ? (float)Math.Round(media, 1) : (float)Math.Round(media);



                var salvardados = new ColetaModel.ArlaInsoluveis
                {
                    os = OS,
                    orcamento = orcamento,
                    data_ini = data_ini,
                    data_term = data_term,
                    norma = norma,
                    np = np,
                    descricao = descricao,
                    preparacao_placa1 = preparacao_placa1,
                    preparacao_filtro1 = preparacao_filtro1,
                    preparacao_massa1 = preparacao_massa1,
                    preparacao_placa2 = preparacao_placa2,
                    preparacao_filtro2 = preparacao_filtro2,
                    preparacao_massa2 = preparacao_massa2,
                    pesagem_amostra1 = pesagem_amostra1,
                    pesagem_placa1 = pesagem_placa1,
                    pesagem_filtro1 = pesagem_filtro1,
                    pesagem_resultado1 = pesagem_resultado1,
                    pesagem_amostra2 = pesagem_amostra2,
                    pesagem_placa2 = pesagem_placa2,
                    pesagem_filtro2 = pesagem_filtro2,
                    pesagem_resultado2 = pesagem_resultado2,
                    pesagem_media = pesagem_media.ToString() + " %",
                    pesagem_resultfinal = pesagem_resultfinal.ToString() + " mg/kg",
                    mat_prima1 = mat_prima1,
                    mat_lote1 = mat_lote1,
                    mat_validade1 = mat_validade1,
                    mat_prima2 = mat_prima2,
                    mat_lote2 = mat_lote2,
                    mat_validade2 = mat_validade2,
                    inst_desc1 = inst_desc1,
                    inst_cod1 = inst_cod1,
                    inst_validade1 = inst_validade1,
                    inst_desc2 = inst_desc2,
                    inst_cod2 = inst_cod2,
                    inst_validade2 = inst_validade2,
                    inst_desc3 = inst_desc3,
                    inst_cod3 = inst_cod3,
                    inst_validade3 = inst_validade3,
                    inst_desc4 = inst_desc4,
                    inst_cod4 = inst_cod4,
                    inst_validade4 = inst_validade4,
                    inst_desc5 = inst_desc5,
                    inst_cod5 = inst_cod5,
                    inst_validade5 = inst_validade5,
                    inst_desc1_1 = inst_desc1_1,
                    inst_cod1_1 = inst_cod1_1,
                    inst_validade1_1 = inst_validade1_1,
                    inst_desc2_2 = inst_desc2_2,
                    inst_cod2_2 = inst_cod2_2,
                    inst_validade2_2 = inst_validade2_2,
                    inst_desc3_3 = inst_desc3_3,
                    inst_cod3_3 = inst_cod3_3,
                    inst_validade3_3 = inst_validade3_3,
                    inst_desc4_4 = inst_desc4_4,
                    inst_cod4_4 = inst_cod4_4,
                    inst_validade4_4 = inst_validade4_4,
                    inst_desc5_5 = inst_desc5_5,
                    inst_cod5_5 = inst_cod5_5,
                    inst_validade5_5 = inst_validade5_5,
                    equi_ee = equi_ee,
                    equi_de = equi_de,
                    observacoes = observacoes,
                    executado = executado,
                    auxiliado = auxiliado,
                };
                //salvando no banco.
                _qcontext.Add(salvardados);
                await _qcontext.SaveChangesAsync();
                TempData["Mensagem"] = "Salvo Com Sucesso";
                return RedirectToAction(nameof(EnsaioInsoluveis), new { OS, orcamento });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> SalvarMetais(string OS, string orcamento, [Bind("data_ini,data_term,norma,np,descricao,ex_lq_al,ex_lq_ca,ex_lq_cr,ex_lq_cu,ex_lq_fe,ex_lq_k,ex_lq_mg,ex_lq_na,ex_lq_ni,ex_lq_zn,ex_lim_al,ex_lim_ca,ex_lim_cr,ex_lim_cu,ex_lim_fe,ex_lim_k,ex_lim_mg," +
                "ex_lim_na,ex_lim_ni,ex_lim_zn, result_al,result_ca,result_cr,result_cu,result_fe,result_k,result_mg,result_na,result_ni,result_zn,mat_prima1,mat_lote1,mat_val1,mat_prima2,matt_lote2,mat_val2,mat_prima3,mat_lote3,mat_val3," +
                "mat_prima4,mat_lote4,mat_val4,mat_prima5,mat_lote5,mat_val5,mat_prima6,mat_lote6,mat_val6,mat_prima7,mat_lote7,mat_val7,mat_prima8,mat_lote8,mat_val8,mat_prima9,mat_lote9,mat_val9,mat_prima10,mat_lote10,mat_val10,mat_prima11,mat_lote11,mat_val11,mat_prima12,mat_lote12,mat_val12,mat_prima13,mat_lote13,mat_val13," +
                "inst_desc1,inst_cod1,inst_val1,inst_desc2,inst_cod2,inst_val2,inst_desc3,inst_cod3,inst_val3,inst_desc4,inst_cod4,inst_val4,inst_desc5,inst_cod5,inst_val5,inst_desc6,inst_cod6,inst_val6,inst_desc7,inst_cod7,inst_val7,inst_desc8,inst_cod8,inst_val8,inst_desc9,inst_cod9,inst_val9,inst_desc10,inst_cod10,inst_val10," +
                "inst_desc11,inst_cod11,inst_val11,inst_dec12,inst_cod12,inst_val12,inst_desc14,inst_cod14,inst_val14,equi_ee,equi_de,observacoes,executado,auxiliado,media_al,media_ca,media_cr,media_cu,media_fe,media_k,media_mg,media_na,media_ni,media_zn")] ColetaModel.ArlaMetais metais,
          [Bind("branco_al,branco_ca,branco_cr,branco_cu,branco_fe,branco_k,branco_mg,branco_na,branco_ni,branco_zn,resul_ob_al1,resul_ob_ca1,resul_ob_cr1,resul_ob_cu1," +
            "resul_ob_fe1,resul_ob_k1,resul_ob_mg1,resul_ob_na1,resul_ob_ni1,resul_ob_zn1,resul_ob_al2,resul_ob_ca2,resul_ob_cr2,resul_ob_cu2,resul_ob_fe2,resul_ob_k2,resul_ob_mg2,resul_ob_na2,resul_ob_ni2,resul_ob_zn2,resul_ob_al3," +
            "resul_ob_ca3,resul_ob_cr3,resul_ob_cu3,resul_ob_fe3,resul_ob_k3,resul_ob_mg3,resul_ob_na3,resul_ob_ni3,resul_ob_zn3")] ColetaModel.MetaisTratamento tratamento)
        {
            try
            {
                if (OS != null && OS != "0" && orcamento != "0")
                {
                    //pegando do html Arla Tratamento
                    DateTime data_ini = metais.data_ini;
                    DateTime data_term = metais.data_term;
                    string norma = metais.norma;
                    string np = metais.np;
                    string descricao = metais.descricao;
                    string observacoes = metais.observacoes;
                    string executado = metais.executado;
                    string auxiliado = metais.auxiliado;
                    float branco_al = tratamento.branco_al;
                    float branco_ca = tratamento.branco_ca;
                    float branco_cr = tratamento.branco_cr;
                    float branco_cu = tratamento.branco_cu;
                    float branco_fe = tratamento.branco_fe;
                    float branco_k = tratamento.branco_k;
                    float branco_mg = tratamento.branco_mg;
                    float branco_na = tratamento.branco_na;
                    float branco_ni = tratamento.branco_ni;
                    float branco_zn = tratamento.branco_zn;
                    float resul_ob_al1 = tratamento.resul_ob_al1;
                    float resul_ob_ca1 = tratamento.resul_ob_ca1;
                    float resul_ob_cr1 = tratamento.resul_ob_cr1;
                    float resul_ob_cu1 = tratamento.resul_ob_cu1;
                    float resul_ob_fe1 = tratamento.resul_ob_fe1;
                    float resul_ob_k1 = tratamento.resul_ob_k1;
                    float resul_ob_mg1 = tratamento.resul_ob_mg1;
                    float resul_ob_na1 = tratamento.resul_ob_na1;
                    float resul_ob_ni1 = tratamento.resul_ob_ni1;
                    float resul_ob_zn1 = tratamento.resul_ob_zn1;
                    float resul_ob_al2 = tratamento.resul_ob_al2;
                    float resul_ob_ca2 = tratamento.resul_ob_ca2;
                    float resul_ob_cr2 = tratamento.resul_ob_cr2;
                    float resul_ob_cu2 = tratamento.resul_ob_cu2;
                    float resul_ob_fe2 = tratamento.resul_ob_fe2;
                    float resul_ob_k2 = tratamento.resul_ob_k2;
                    float resul_ob_mg2 = tratamento.resul_ob_mg2;
                    float resul_ob_na2 = tratamento.resul_ob_na2;
                    float resul_ob_ni2 = tratamento.resul_ob_ni2;
                    float resul_ob_zn2 = tratamento.resul_ob_zn2;
                    float resul_ob_al3 = tratamento.resul_ob_al3;
                    float resul_ob_ca3 = tratamento.resul_ob_ca3;
                    float resul_ob_cr3 = tratamento.resul_ob_cr3;
                    float resul_ob_cu3 = tratamento.resul_ob_cu3;
                    float resul_ob_fe3 = tratamento.resul_ob_fe3;
                    float resul_ob_k3 = tratamento.resul_ob_k3;
                    float resul_ob_mg3 = tratamento.resul_ob_mg3;
                    float resul_ob_na3 = tratamento.resul_ob_na3;
                    float resul_ob_ni3 = tratamento.resul_ob_ni3;
                    float resul_ob_zn3 = tratamento.resul_ob_zn3;

                    //realizando a conta obtidos do resultados e ja convertendo para 5 casas decimais.
                    float resul_con_al1 = (float)Math.Round((((resul_ob_al1 - branco_al) * 0.01f) / 0.02f), 5);
                    float resul_con_ca1 = (float)Math.Round((((resul_ob_ca1 - branco_ca) * 0.01f) / 0.02f), 5);
                    float resul_con_cr1 = (float)Math.Round((((resul_ob_cr1 - branco_cr) * 0.01f) / 0.02f), 5);
                    float resul_con_cu1 = (float)Math.Round((((resul_ob_cu1 - branco_cu) * 0.01f) / 0.02f), 5);
                    float resul_con_fe1 = (float)Math.Round((((resul_ob_fe1 - branco_fe) * 0.01f) / 0.02f), 5);
                    float resul_con_k1 = (float)Math.Round((((resul_ob_k1 - branco_k) * 0.01f) / 0.02f), 5);
                    float resul_con_mg1 = (float)Math.Round((((resul_ob_mg1 - branco_mg) * 0.01f) / 0.02f), 5);
                    float resul_con_na1 = (float)Math.Round((((resul_ob_na1 - branco_na) * 0.01f) / 0.02f), 5);
                    float resul_con_ni1 = (float)Math.Round((((resul_ob_ni1 - branco_ni) * 0.01f) / 0.02f), 5);
                    float resul_con_zn1 = (float)Math.Round((((resul_ob_zn1 - branco_zn) * 0.01f) / 0.02f), 5);
                    float resul_con_al2 = (float)Math.Round((((resul_ob_al2 - branco_al) * 0.01f) / 0.02f), 5);
                    float resul_con_ca2 = (float)Math.Round((((resul_ob_ca2 - branco_ca) * 0.01f) / 0.02f), 5);
                    float resul_con_cr2 = (float)Math.Round((((resul_ob_cr2 - branco_cr) * 0.01f) / 0.02f), 5);
                    float resul_con_cu2 = (float)Math.Round((((resul_ob_cu2 - branco_cr) * 0.01f) / 0.02f), 5);
                    float resul_con_fe2 = (float)Math.Round((((resul_ob_fe2 - branco_fe) * 0.01f) / 0.02f), 5);
                    float resul_con_k2 = (float)Math.Round((((resul_ob_k2 - branco_k) * 0.01f) / 0.02f), 5);
                    float resul_con_mg2 = (float)Math.Round((((resul_ob_mg2 - branco_mg) * 0.01f) / 0.02f), 5);
                    float resul_con_na2 = (float)Math.Round((((resul_ob_na2 - branco_na) * 0.01f) / 0.02f), 5);
                    float resul_con_ni2 = (float)Math.Round((((resul_ob_ni2 - branco_ni) * 0.01f) / 0.02f), 5);
                    float resul_con_zn2 = (float)Math.Round((((resul_ob_zn2 - branco_zn) * 0.01f) / 0.02f), 5);
                    float resul_con_al3 = (float)Math.Round((((resul_ob_al3 - branco_al) * 0.01f) / 0.02f), 5);
                    float resul_con_ca3 = (float)Math.Round((((resul_ob_ca3 - branco_ca) * 0.01f) / 0.02f), 5);
                    float resul_con_cr3 = (float)Math.Round((((resul_ob_cr3 - branco_cr) * 0.01f) / 0.02f), 5);
                    float resul_con_cu3 = (float)Math.Round((((resul_ob_cu3 - branco_cu) * 0.01f) / 0.02f), 5);
                    float resul_con_fe3 = (float)Math.Round((((resul_ob_fe3 - branco_fe) * 0.01f) / 0.02f), 5);
                    float resul_con_k3 = (float)Math.Round((((resul_ob_k3 - branco_k) * 0.01f) / 0.02f), 5);
                    float resul_con_mg3 = (float)Math.Round((((resul_ob_mg3 - branco_mg) * 0.01f) / 0.02f), 5);
                    float resul_con_na3 = (float)Math.Round((((resul_ob_na3 - branco_na) * 0.01f) / 0.02f), 5);
                    float resul_con_ni3 = (float)Math.Round((((resul_ob_ni3 - branco_ni) * 0.01f) / 0.02f), 5);
                    float resul_con_zn3 = (float)Math.Round((((resul_ob_zn3 - branco_zn) * 0.01f) / 0.02f), 5);

                    //salvar dados no banco.
                    var salvardadostrat = new ColetaModel.MetaisTratamento
                    {
                        os = OS,
                        orcamento = orcamento,
                        branco_al = branco_al,
                        branco_ca = branco_ca,
                        branco_cr = branco_cr,
                        branco_cu = branco_cu,
                        branco_fe = branco_fe,
                        branco_k = branco_k,
                        branco_mg = branco_mg,
                        branco_na = branco_na,
                        branco_ni = branco_ni,
                        branco_zn = branco_zn,
                        resul_ob_al1 = resul_ob_al1,
                        resul_ob_ca1 = resul_ob_ca1,
                        resul_ob_cr1 = resul_ob_cr1,
                        resul_ob_cu1 = resul_ob_cu1,
                        resul_ob_fe1 = resul_ob_fe1,
                        resul_ob_k1 = resul_ob_k1,
                        resul_ob_mg1 = resul_ob_mg1,
                        resul_ob_na1 = resul_ob_na1,
                        resul_ob_ni1 = resul_ob_ni1,
                        resul_ob_zn1 = resul_ob_zn1,
                        resul_ob_al2 = resul_ob_al2,
                        resul_ob_ca2 = resul_ob_ca2,
                        resul_ob_cr2 = resul_ob_cr2,
                        resul_ob_cu2 = resul_ob_cu2,
                        resul_ob_fe2 = resul_ob_fe2,
                        resul_ob_k2 = resul_ob_k2,
                        resul_ob_mg2 = resul_ob_mg2,
                        resul_ob_na2 = resul_ob_na2,
                        resul_ob_ni2 = resul_ob_ni2,
                        resul_ob_zn2 = resul_ob_zn2,
                        resul_ob_al3 = resul_ob_al3,
                        resul_ob_ca3 = resul_ob_ca3,
                        resul_ob_cr3 = resul_ob_cr3,
                        resul_ob_cu3 = resul_ob_cu3,
                        resul_ob_fe3 = resul_ob_fe3,
                        resul_ob_k3 = resul_ob_k3,
                        resul_ob_mg3 = resul_ob_mg3,
                        resul_ob_na3 = resul_ob_na3,
                        resul_ob_ni3 = resul_ob_ni3,
                        resul_ob_zn3 = resul_ob_zn3,
                        resul_con_al1 = resul_con_al1,
                        resul_con_ca1 = resul_con_ca1,
                        resul_con_cr1 = resul_con_cr1,
                        resul_con_cu1 = resul_con_cu1,
                        resul_con_fe1 = resul_con_fe1,
                        resul_con_k1 = resul_con_k1,
                        resul_con_mg1 = resul_con_mg1,
                        resul_con_na1 = resul_con_na1,
                        resul_con_ni1 = resul_con_ni1,
                        resul_con_zn1 = resul_con_zn1,
                        resul_con_al2 = resul_con_al2,
                        resul_con_ca2 = resul_con_ca2,
                        resul_con_cr2 = resul_con_cr2,
                        resul_con_cu2 = resul_con_cu2,
                        resul_con_fe2 = resul_con_fe2,
                        resul_con_k2 = resul_con_k2,
                        resul_con_mg2 = resul_con_mg2,
                        resul_con_na2 = resul_con_na2,
                        resul_con_ni2 = resul_con_ni2,
                        resul_con_zn2 = resul_con_zn2,
                        resul_con_al3 = resul_con_al3,
                        resul_con_ca3 = resul_con_ca3,
                        resul_con_cr3 = resul_con_cr3,
                        resul_con_cu3 = resul_con_cu3,
                        resul_con_fe3 = resul_con_fe3,
                        resul_con_k3 = resul_con_k3,
                        resul_con_mg3 = resul_con_mg3,
                        resul_con_na3 = resul_con_na3,
                        resul_con_ni3 = resul_con_ni3,
                        resul_con_zn3 = resul_con_zn3,
                        observacoes = observacoes,
                        auxiliado = auxiliado,
                        executado = executado,
                    };
                    //salvando no banco.
                    //_qcontext.Add(salvardadostrat);
                    //await _qcontext.SaveChangesAsync();

                    //pegando do html Arla Metais

                    float ex_lq_al = metais.ex_lq_al;
                    float ex_lq_ca = metais.ex_lq_ca;
                    float ex_lq_cr = metais.ex_lq_cr;
                    float ex_lq_cu = metais.ex_lq_cu;
                    float ex_lq_fe = metais.ex_lq_fe;
                    float ex_lq_k = metais.ex_lq_k;
                    float ex_lq_mg = metais.ex_lq_mg;
                    float ex_lq_na = metais.ex_lq_na;
                    float ex_lq_ni = metais.ex_lq_ni;
                    float ex_lq_zn = metais.ex_lq_zn;
                    float ex_lim_al = metais.ex_lim_al;
                    float ex_lim_ca = metais.ex_lim_ca;
                    float ex_lim_cr = metais.ex_lim_cr;
                    float ex_lim_cu = metais.ex_lim_cu;
                    float ex_lim_fe = metais.ex_lim_fe;
                    float ex_lim_k = metais.ex_lim_k;
                    float ex_lim_mg = metais.ex_lim_mg;
                    float ex_lim_na = metais.ex_lim_na;
                    float ex_lim_ni = metais.ex_lim_ni;
                    float ex_lim_zn = metais.ex_lim_zn;
                    string mat_prima1 = metais.mat_prima1;
                    string mat_lote1 = metais.mat_lote1;
                    DateTime mat_val1 = metais.mat_val1;
                    string mat_prima2 = metais.mat_prima2;
                    string matt_lote2 = metais.matt_lote2;
                    DateTime mat_val2 = metais.mat_val2;
                    string mat_prima3 = metais.mat_prima3;
                    string mat_lote3 = metais.mat_lote3;
                    DateTime mat_val3 = metais.mat_val3;
                    string mat_prima4 = metais.mat_prima4;
                    string mat_lote4 = metais.mat_lote4;
                    DateTime mat_val4 = metais.mat_val4;
                    string mat_prima5 = metais.mat_prima5;
                    string mat_lote5 = metais.mat_lote5;
                    DateTime mat_val5 = metais.mat_val5;
                    string mat_prima6 = metais.mat_prima6;
                    string mat_lote6 = metais.mat_lote6;
                    DateTime mat_val6 = metais.mat_val6;
                    string mat_prima7 = metais.mat_prima7;
                    string mat_lote7 = metais.mat_lote7;
                    DateTime mat_val7 = metais.mat_val7;
                    string mat_prima8 = metais.mat_prima8;
                    string mat_lote8 = metais.mat_lote8;
                    DateTime mat_val8 = metais.mat_val8;
                    string mat_prima9 = metais.mat_prima9;
                    string mat_lote9 = metais.mat_lote9;
                    DateTime mat_val9 = metais.mat_val9;
                    string mat_prima10 = metais.mat_prima10;
                    string mat_lote10 = metais.mat_lote10;
                    DateTime mat_val10 = metais.mat_val10;
                    string mat_prima11 = metais.mat_prima11;
                    string mat_lote11 = metais.mat_lote11;
                    DateTime mat_val11 = metais.mat_val11;
                    string mat_prima12 = metais.mat_prima12;
                    string mat_lote12 = metais.mat_lote12;
                    DateTime mat_val12 = metais.mat_val12;
                    string mat_prima13 = metais.mat_prima13;
                    string mat_lote13 = metais.mat_lote13;
                    DateTime mat_val13 = metais.mat_val13;
                    string inst_desc1 = metais.inst_desc1;
                    string inst_cod1 = metais.inst_cod1;
                    DateTime inst_val1 = metais.inst_val1;
                    string inst_desc2 = metais.inst_desc2;
                    string inst_cod2 = metais.inst_cod2;
                    DateTime inst_val2 = metais.inst_val2;
                    string inst_desc3 = metais.inst_desc3;
                    string inst_cod3 = metais.inst_cod3;
                    DateTime inst_val3 = metais.inst_val3;
                    string inst_desc4 = metais.inst_desc4;
                    string inst_cod4 = metais.inst_cod4;
                    DateTime inst_val4 = metais.inst_val4;
                    string inst_desc5 = metais.inst_desc5;
                    string inst_cod5 = metais.inst_cod5;
                    DateTime inst_val5 = metais.inst_val5;
                    string inst_desc6 = metais.inst_desc6;
                    string inst_cod6 = metais.inst_cod6;
                    DateTime inst_val6 = metais.inst_val6;
                    string inst_desc7 = metais.inst_desc7;
                    string inst_cod7 = metais.inst_cod7;
                    DateTime inst_val7 = metais.inst_val7;
                    string inst_desc8 = metais.inst_desc8;
                    string inst_cod8 = metais.inst_cod8;
                    DateTime inst_val8 = metais.inst_val8;
                    string inst_desc9 = metais.inst_desc9;
                    string inst_cod9 = metais.inst_cod9;
                    DateTime inst_val9 = metais.inst_val9;
                    string inst_desc10 = metais.inst_desc10;
                    string inst_cod10 = metais.inst_cod10;
                    DateTime inst_val10 = metais.inst_val10;
                    string inst_desc11 = metais.inst_desc11;
                    string inst_cod11 = metais.inst_cod11;
                    DateTime inst_val11 = metais.inst_val11;
                    string inst_dec12 = metais.inst_dec12;
                    string inst_cod12 = metais.inst_cod12;
                    DateTime inst_val12 = metais.inst_val12;
                    string inst_desc14 = metais.inst_desc14;
                    string inst_cod14 = metais.inst_cod14;
                    DateTime inst_val14 = metais.inst_val14;
                    string equi_ee = metais.equi_ee;
                    string equi_de = metais.equi_de;

                    //criando as variaveis vazias para depois guardar os valores da quantificação.
                    string ex_quant_al1 = "";
                    string ex_quant_ca1 = "";
                    string ex_quant_cr1 = "";
                    string ex_quant_cu1 = "";
                    string ex_quant_fe1 = "";
                    string ex_quant_k1 = "";
                    string ex_quant_mg1 = "";
                    string ex_quant_na1 = "";
                    string ex_quant_ni1 = "";
                    string ex_quant_zn1 = "";
                    string ex_quant_al2 = "";
                    string ex_quant_ca2 = "";
                    string ex_quant_cr2 = "";
                    string ex_quant_cu2 = "";
                    string ex_quant_fe2 = "";
                    string ex_quant_k2 = "";
                    string ex_quant_mg2 = "";
                    string ex_quant_na2 = "";
                    string ex_quant_ni2 = "";
                    string ex_quant_zn2 = "";
                    string ex_quant_al3 = "";
                    string ex_quant_ca3 = "";
                    string ex_quant_cr3 = "";
                    string ex_quant_cu3 = "";
                    string ex_quant_fe3 = "";
                    string ex_quant_k3 = "";
                    string ex_quant_mg3 = "";
                    string ex_quant_na3 = "";
                    string ex_quant_ni3 = "";
                    string ex_quant_zn3 = "";


                    //verificnado a primeira fileira de quantificação do anexo i
                    if (resul_con_al1 < ex_lq_al)
                    {
                        ex_quant_al1 = "<LQ";
                    }
                    else
                    {
                        ex_quant_al1 = resul_con_al1.ToString();
                    }

                    if (resul_con_ca1 < ex_lq_ca)
                    {
                        ex_quant_ca1 = "<LQ";
                    }
                    else
                    {
                        ex_quant_ca1 = resul_con_ca1.ToString();
                    }

                    if (resul_con_cr1 < ex_lq_cr)
                    {
                        ex_quant_cr1 = "<LQ";
                    }
                    else
                    {
                        ex_quant_cr1 = resul_con_cr1.ToString();
                    }

                    if (resul_con_cu1 < ex_lq_cu)
                    {
                        ex_quant_cu1 = "<LQ";
                    }
                    else
                    {
                        ex_quant_cu1 = resul_con_cu1.ToString();
                    }

                    if (resul_con_fe1 < ex_lq_fe)
                    {
                        ex_quant_fe1 = "<LQ";
                    }
                    else
                    {
                        ex_quant_fe1 = resul_con_fe1.ToString();
                    }

                    if (resul_con_k1 < ex_lq_k)
                    {
                        ex_quant_k1 = "<LQ";
                    }
                    else
                    {
                        ex_quant_k1 = resul_con_k1.ToString();
                    }

                    if (resul_con_mg1 < ex_lq_mg)
                    {
                        ex_quant_mg1 = "<LQ";
                    }
                    else
                    {
                        ex_quant_mg1 = resul_con_mg1.ToString();
                    }

                    if (resul_con_na1 < ex_lq_na)
                    {
                        ex_quant_na1 = "<LQ";
                    }
                    else
                    {
                        ex_quant_na1 = resul_con_na1.ToString();
                    }

                    if (resul_con_ni1 < ex_lq_ni)
                    {
                        ex_quant_ni1 = "<LQ";
                    }
                    else
                    {
                        ex_quant_ni1 = resul_con_ni1.ToString();
                    }

                    if (resul_con_zn1 < ex_lq_zn)
                    {
                        ex_quant_zn1 = "<LQ";
                    }
                    else
                    {
                        ex_quant_zn1 = resul_con_zn1.ToString();
                    }

                    //verificando segunda fileira da quantificação anexo i.
                    if (resul_con_al2 < ex_lq_al)
                    {
                        ex_quant_al2 = "<LQ";
                    }
                    else
                    {
                        ex_quant_al2 = resul_con_al2.ToString();
                    }

                    if (resul_con_ca2 < ex_lq_ca)
                    {
                        ex_quant_ca2 = "<LQ";
                    }
                    else
                    {
                        ex_quant_ca2 = resul_con_ca2.ToString();
                    }

                    if (resul_con_cr2 < ex_lq_cr)
                    {
                        ex_quant_cr2 = "LQ";
                    }
                    else
                    {
                        ex_quant_cr2 = resul_con_cr2.ToString();
                    }

                    if (resul_con_cu2 < ex_lq_cu)
                    {
                        ex_quant_cu2 = "<LQ";
                    }
                    else
                    {
                        ex_quant_cu2 = resul_con_cu2.ToString();
                    }

                    if (resul_con_fe2 < ex_lq_fe)
                    {
                        ex_quant_fe2 = "<LQ";
                    }
                    else
                    {
                        ex_quant_fe2 = resul_con_fe2.ToString();
                    }

                    if (resul_con_k2 < ex_lq_k)
                    {
                        ex_quant_k2 = "<LQ";
                    }
                    else
                    {
                        ex_quant_k2 = resul_con_k2.ToString();
                    }

                    if (resul_con_mg2 < ex_lq_mg)
                    {
                        ex_quant_mg2 = "<LQ";
                    }
                    else
                    {
                        ex_quant_mg2 = resul_con_mg2.ToString();
                    }

                    if (resul_con_na2 < ex_lq_na)
                    {
                        ex_quant_na2 = "<LQ";
                    }
                    else
                    {
                        ex_quant_na2 = resul_con_na2.ToString();
                    }

                    if (resul_con_ni2 < ex_lq_ni)
                    {
                        ex_quant_ni2 = "<LQ";
                    }
                    else
                    {
                        ex_quant_ni2 = resul_con_ni2.ToString();
                    }

                    if (resul_con_zn2 < ex_lq_zn)
                    {
                        ex_quant_zn2 = "<LQ";
                    }
                    else
                    {
                        ex_quant_zn2 = resul_con_zn2.ToString();
                    }

                    //verificando  terceira da quantificação anexo i.
                    if (resul_con_al3 < ex_lq_al)
                    {
                        ex_quant_al3 = "<LQ";
                    }
                    else
                    {
                        ex_quant_al3 = resul_con_al3.ToString();
                    }

                    if (resul_con_ca3 < ex_lq_ca)
                    {
                        ex_quant_ca3 = "<LQ";
                    }
                    else
                    {
                        ex_quant_ca3 = resul_con_ca3.ToString();
                    }

                    if (resul_con_cr3 < ex_lq_cr)
                    {
                        ex_quant_cr3 = "LQ";
                    }
                    else
                    {
                        ex_quant_cr3 = resul_con_cr3.ToString();
                    }

                    if (resul_con_cu3 < ex_lq_cu)
                    {
                        ex_quant_cu3 = "<LQ";
                    }
                    else
                    {
                        ex_quant_cu3 = resul_con_cu3.ToString();
                    }

                    if (resul_con_fe3 < ex_lq_fe)
                    {
                        ex_quant_fe3 = "<LQ";
                    }
                    else
                    {
                        ex_quant_fe3 = resul_con_fe3.ToString();
                    }

                    if (resul_con_k3 < ex_lq_k)
                    {
                        ex_quant_k3 = "<LQ";
                    }
                    else
                    {
                        ex_quant_k3 = resul_con_k3.ToString();
                    }

                    if (resul_con_mg3 < ex_lq_mg)
                    {
                        ex_quant_mg3 = "<LQ";
                    }
                    else
                    {
                        ex_quant_mg3 = resul_con_mg3.ToString();
                    }

                    if (resul_con_na3 < ex_lq_na)
                    {
                        ex_quant_na3 = "<LQ";
                    }
                    else
                    {
                        ex_quant_na3 = resul_con_na3.ToString();
                    }

                    if (resul_con_ni3 < ex_lq_ni)
                    {
                        ex_quant_ni3 = "<LQ";
                    }
                    else
                    {
                        ex_quant_ni3 = resul_con_ni2.ToString();
                    }

                    if (resul_con_zn3 < ex_lq_zn)
                    {
                        ex_quant_zn3 = "<LQ";
                    }
                    else
                    {
                        ex_quant_zn3 = resul_con_zn3.ToString();
                    }

                    //pegando valor final dos resultados e realizando as verificações para cada resultado..
                    string result_al = "";
                    string result_ca = "";
                    string result_cr = "";
                    string result_cu = "";
                    string result_fe = "";
                    string result_k = "";
                    string result_mg = "";
                    string result_na = "";
                    string result_ni = "";
                    string result_zn = "";

                    if (ex_quant_al1 == "<LQ" || ex_quant_al2 == "<LQ" || ex_quant_al3 == "<LQ")
                    {
                        result_al = "<LQ";
                    }
                    else
                    {
                        float pegando_result_al = ((float.Parse(ex_quant_al1) + float.Parse(ex_quant_al2) + float.Parse(ex_quant_al3)) / 2);
                        string convert_result_al = pegando_result_al.ToString();
                        result_al = convert_result_al;
                    }

                    if (ex_quant_ca1 == "<LQ" || ex_quant_ca2 == "<LQ" || ex_quant_ca3 == "<LQ")
                    {
                        result_ca = "<LQ";
                    }
                    else
                    {
                        float pegando_result_ca = ((float.Parse(ex_quant_ca1) + float.Parse(ex_quant_ca2) + float.Parse(ex_quant_ca3)) / 2);
                        string convert_result_ca = pegando_result_ca.ToString();
                        result_ca = convert_result_ca;
                    }

                    if (ex_quant_cr1 == "<LQ" || ex_quant_cr2 == "<LQ" || ex_quant_cr3 == "<LQ")
                    {
                        result_cr = "<LQ";
                    }
                    else
                    {
                        float pegando_result_cr = ((float.Parse(ex_quant_cr1) + float.Parse(ex_quant_cr2) + float.Parse(ex_quant_cr3)) / 2);
                        string convert_result_cr = pegando_result_cr.ToString();
                        result_cr = convert_result_cr;
                    }

                    if (ex_quant_cu1 == "<LQ" || ex_quant_cu2 == "<LQ" || ex_quant_cu3 == "<LQ")
                    {
                        result_cu = "<LQ";
                    }
                    else
                    {
                        float pegando_result_cu = ((float.Parse(ex_quant_cu1) + float.Parse(ex_quant_cu2) + float.Parse(ex_quant_cu3)) / 2);
                        string convert_result_cu = pegando_result_cu.ToString();
                        result_cu = convert_result_cu;
                    }

                    if (ex_quant_fe1 == "<LQ" || ex_quant_fe2 == "<LQ" || ex_quant_fe3 == "<LQ")
                    {
                        result_fe = "<LQ";
                    }
                    else
                    {
                        float pegando_result_fe = ((float.Parse(ex_quant_fe1) + float.Parse(ex_quant_fe2) + float.Parse(ex_quant_fe3)) / 2);
                        string convert_result_fe = pegando_result_fe.ToString();
                        result_fe = convert_result_fe;
                    }

                    if (ex_quant_k1 == "<LQ" || ex_quant_k2 == "<LQ" || ex_quant_k3 == "<LQ")
                    {
                        result_k = "<LQ";
                    }
                    else
                    {
                        float pegando_result_k = ((float.Parse(ex_quant_k1) + float.Parse(ex_quant_k2) + float.Parse(ex_quant_k3)) / 2);
                        string convert_result_k = pegando_result_k.ToString();
                        result_k = convert_result_k;
                    }

                    if (ex_quant_mg1 == "<LQ" || ex_quant_mg2 == "<LQ" || ex_quant_mg3 == "<LQ")
                    {
                        result_mg = "<LQ";
                    }
                    else
                    {
                        float pegando_result_mg = ((float.Parse(ex_quant_mg1) + float.Parse(ex_quant_mg2) + float.Parse(ex_quant_mg3)) / 2);
                        string convert_result_mg = pegando_result_mg.ToString();
                        result_mg = convert_result_mg;
                    }

                    if (ex_quant_na1 == "<LQ" || ex_quant_na2 == "<LQ" || ex_quant_na3 == "<LQ")
                    {
                        result_na = "<LQ";
                    }
                    else
                    {
                        float pegando_result_na = ((float.Parse(ex_quant_na1) + float.Parse(ex_quant_na2) + float.Parse(ex_quant_na3)) / 2);
                        string convert_result_na = pegando_result_na.ToString();
                        result_na = convert_result_na;
                    }

                    if (ex_quant_ni1 == "<LQ" || ex_quant_ni2 == "<LQ" || ex_quant_ni3 == "<LQ")
                    {
                        result_ni = "<LQ";
                    }
                    else
                    {
                        float pegando_result_ni = ((float.Parse(ex_quant_ni1) + float.Parse(ex_quant_ni2) + float.Parse(ex_quant_ni3)) / 2);
                        string convert_result_ni = pegando_result_ni.ToString();
                        result_ni = convert_result_ni;
                    }

                    if (ex_quant_zn1 == "<LQ" || ex_quant_zn2 == "<LQ" || ex_quant_zn3 == "<LQ")
                    {
                        result_zn = "<LQ";
                    }
                    else
                    {
                        float pegando_result_zn = ((float.Parse(ex_quant_zn1) + float.Parse(ex_quant_zn2) + float.Parse(ex_quant_zn3)) / 2);
                        string convert_result_zn = pegando_result_zn.ToString();
                        result_zn = convert_result_zn;
                    }

                    var salvardados = new ColetaModel.ArlaMetais
                    {
                        os = OS,
                        orcamento = orcamento,
                        ex_lq_al = ex_lq_al,
                        ex_lq_ca = ex_lq_ca,
                        ex_lq_cr = ex_lq_cr,
                        ex_lq_cu = ex_lq_cu,
                        ex_lq_fe = ex_lq_fe,
                        ex_lq_k = ex_lq_k,
                        ex_lq_mg = ex_lq_mg,
                        ex_lq_na = ex_lq_na,
                        ex_lq_ni = ex_lq_ni,
                        ex_lq_zn = ex_lq_zn,
                        ex_lim_al = ex_lim_al,
                        ex_lim_ca = ex_lim_ca,
                        ex_lim_cr = ex_lim_cr,
                        ex_lim_cu = ex_lim_cu,
                        ex_lim_fe = ex_lim_fe,
                        ex_lim_k = ex_lim_k,
                        ex_lim_mg = ex_lim_mg,
                        ex_lim_na = ex_lim_na,
                        ex_lim_ni = ex_lim_ni,
                        ex_lim_zn = ex_lim_zn,
                        ex_quant_al1 = ex_quant_al1,
                        ex_quant_ca1 = ex_quant_ca1,
                        ex_quant_cr1 = ex_quant_cr1,
                        ex_quant_cu1 = ex_quant_cu1,
                        ex_quant_fe1 = ex_quant_fe1,
                        ex_quant_k1 = ex_quant_k1,
                        ex_quant_mg1 = ex_quant_mg1,
                        ex_quant_na1 = ex_quant_na1,
                        ex_quant_ni1 = ex_quant_ni1,
                        ex_quant_zn1 = ex_quant_zn1,
                        ex_quant_al2 = ex_quant_al2,
                        ex_quant_ca2 = ex_quant_ca2,
                        ex_quant_cr2 = ex_quant_cr2,
                        ex_quant_cu2 = ex_quant_cu2,
                        ex_quant_fe2 = ex_quant_fe2,
                        ex_quant_k2 = ex_quant_k2,
                        ex_quant_mg2 = ex_quant_mg2,
                        ex_quant_na2 = ex_quant_na2,
                        ex_quant_ni2 = ex_quant_ni2,
                        ex_quant_zn2 = ex_quant_zn2,
                        ex_quant_al3 = ex_quant_al3,
                        ex_quant_ca3 = ex_quant_ca3,
                        ex_quant_cr3 = ex_quant_cr3,
                        ex_quant_cu3 = ex_quant_cu3,
                        ex_quant_fe3 = ex_quant_fe3,
                        ex_quant_k3 = ex_quant_k3,
                        ex_quant_mg3 = ex_quant_mg3,
                        ex_quant_na3 = ex_quant_na3,
                        ex_quant_ni3 = ex_quant_ni3,
                        ex_quant_zn3 = ex_quant_zn3,
                        result_al = result_al,
                        result_ca = result_ca,
                        result_cr = result_cr,
                        result_cu = result_cu,
                        result_fe = result_fe,
                        result_k = result_k,
                        result_mg = result_mg,
                        result_na = result_na,
                        result_ni = result_ni,
                        result_zn = result_zn,
                        mat_prima1 = mat_prima1,
                        mat_lote1 = mat_lote1,
                        mat_val1 = mat_val1,
                        mat_prima2 = mat_prima2,
                        matt_lote2 = matt_lote2,
                        mat_val2 = mat_val2,
                        mat_prima3 = mat_prima3,
                        mat_lote3 = mat_lote3,
                        mat_val3 = mat_val3,
                        mat_prima4 = mat_prima4,
                        mat_lote4 = mat_lote4,
                        mat_val4 = mat_val4,
                        mat_prima5 = mat_prima5,
                        mat_lote5 = mat_lote5,
                        mat_val5 = mat_val5,
                        mat_prima6 = mat_prima6,
                        mat_lote6 = mat_lote6,
                        mat_val6 = mat_val6,
                        mat_prima7 = mat_prima7,
                        mat_lote7 = mat_lote7,
                        mat_val7 = mat_val7,
                        mat_prima8 = mat_prima8,
                        mat_lote8 = mat_lote8,
                        mat_val8 = mat_val8,
                        mat_prima9 = mat_prima9,
                        mat_lote9 = mat_lote9,
                        mat_val9 = mat_val9,
                        mat_prima10 = mat_prima10,
                        mat_lote10 = mat_lote10,
                        mat_val10 = mat_val10,
                        mat_prima11 = mat_prima11,
                        mat_lote11 = mat_lote11,
                        mat_val11 = mat_val11,
                        mat_prima12 = mat_prima12,
                        mat_lote12 = mat_lote12,
                        mat_val12 = mat_val12,
                        mat_prima13 = mat_prima13,
                        mat_lote13 = mat_lote13,
                        mat_val13 = mat_val13,
                        inst_desc1 = inst_desc1,
                        inst_cod1 = inst_cod1,
                        inst_val1 = inst_val1,
                        inst_desc2 = inst_desc2,
                        inst_cod2 = inst_cod2,
                        inst_val2 = inst_val2,
                        inst_desc3 = inst_desc3,
                        inst_cod3 = inst_cod3,
                        inst_val3 = inst_val3,
                        inst_desc4 = inst_desc4,
                        inst_cod4 = inst_cod4,
                        inst_val4 = inst_val4,
                        inst_desc5 = inst_desc5,
                        inst_cod5 = inst_cod5,
                        inst_val5 = inst_val5,
                        inst_desc6 = inst_desc6,
                        inst_cod6 = inst_cod6,
                        inst_val6 = inst_val6,
                        inst_desc7 = inst_desc7,
                        inst_cod7 = inst_cod7,
                        inst_val7 = inst_val7,
                        inst_desc8 = inst_desc8,
                        inst_cod8 = inst_cod8,
                        inst_val8 = inst_val8,
                        inst_desc9 = inst_desc9,
                        inst_cod9 = inst_cod9,
                        inst_val9 = inst_val9,
                        inst_desc10 = inst_desc10,
                        inst_cod10 = inst_cod10,
                        inst_val10 = inst_val10,
                        inst_desc11 = inst_desc11,
                        inst_cod11 = inst_cod11,
                        inst_val11 = inst_val11,
                        inst_dec12 = inst_dec12,
                        inst_cod12 = inst_cod12,
                        inst_val12 = inst_val12,
                        inst_desc14 = inst_desc14,
                        inst_cod14 = inst_cod14,
                        inst_val14 = inst_val14,
                        equi_ee = equi_ee,
                        equi_de = equi_de,
                        observacoes = observacoes,
                        executado = executado,
                        auxiliado = auxiliado,
                    };
                    //salvando no banco.
                    //_qcontext.Add(salvardados);
                    //await _qcontext.SaveChangesAsync();

                    TempData["Mensagem"] = "Salvo Com Sucesso";
                    return RedirectToAction(nameof(EnsaioMetais), new { OS, orcamento });
                }
                else
                {
                    TempData["Mensagem"] = "Erro ao gravar";
                    return RedirectToAction(nameof(EnsaioMetais), new { OS, orcamento });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;

            }
        }
        [HttpPost]
        public async Task<IActionResult> SalvarFosfato(string OS, string orcamento, [Bind("data_ini,data_term,cod_curva,fator_calibracao,massa,absorvancia1,absorvancia2,abs_branco,abs_qc,mat_prima_1,mat_lote_1,mat_validade_1,mat_prima_2,mat_lote_2,mat_validade_2,mat_prima_3,mat_lote_3,mat_validade_3,mat_prima_4,mat_lote_4,mat_validade_4,mat_prima_5,mat_lote_5,mat_validade_5,mat_prima_6,mat_lote_6,mat_validade_6,mat_prima_7,mat_lote_7,mat_validade_7,mat_prima_8,mat_lote_8,mat_validade_8,inst_desc1,inst_cod1,inst_data1,inst_desc1_1,inst_cod1_1,inst_data1_1,inst_desc2,inst_cod2,inst_data2,inst_desc2_2,inst_cod2_2,inst_data2_2,inst_desc3,inst_cod3,inst_data3,inst_desc3_3,inst_cod3_3,inst_data3_3,inst_desc4,inst_cod4,inst_data4,inst_desc4_4,inst_cod4_4,inst_data4_4,equi_ee,equi_de,obs,executado_por,auxiliado_por")] ColetaModel.ArlaFosfato salvarDados)
        {
            try
            {
                if (OS != null && OS != "0" && orcamento != "0")
                {
                    //pegando valores na web.
                    DateTime data_ini = salvarDados.data_ini;
                    DateTime data_term = salvarDados.data_term;
                    string cod_curva = salvarDados.cod_curva;
                    float fator_calibracao = salvarDados.fator_calibracao;
                    float massa = salvarDados.massa;
                    float absorvancia1 = salvarDados.absorvancia1;
                    float absorvancia2 = salvarDados.absorvancia2;
                    float abs_branco = salvarDados.abs_branco;
                    float abs_qc = salvarDados.abs_qc;
                    string mat_prima_1 = salvarDados.mat_prima_1;
                    string mat_lote_1 = salvarDados.mat_lote_1;
                    DateTime mat_validade_1 = salvarDados.mat_validade_1;
                    string mat_prima_2 = salvarDados.mat_prima_2;
                    string mat_lote_2 = salvarDados.mat_lote_2;
                    DateTime mat_validade_2 = salvarDados.mat_validade_2;
                    string mat_prima_3 = salvarDados.mat_prima_3;
                    string mat_lote_3 = salvarDados.mat_lote_3;
                    DateTime mat_validade_3 = salvarDados.mat_validade_3;
                    string mat_prima_4 = salvarDados.mat_prima_4;
                    string mat_lote_4 = salvarDados.mat_lote_4;
                    DateTime mat_validade_4 = salvarDados.mat_validade_4;
                    string mat_prima_5 = salvarDados.mat_prima_5;
                    string mat_lote_5 = salvarDados.mat_lote_5;
                    DateTime mat_validade_5 = salvarDados.mat_validade_5;
                    string mat_prima_6 = salvarDados.mat_prima_6;
                    string mat_lote_6 = salvarDados.mat_lote_6;
                    DateTime mat_validade_6 = salvarDados.mat_validade_6;
                    string mat_prima_7 = salvarDados.mat_prima_7;
                    string mat_lote_7 = salvarDados.mat_lote_7;
                    DateTime mat_validade_7 = salvarDados.mat_validade_7;
                    string mat_prima_8 = salvarDados.mat_prima_8;
                    string mat_lote_8 = salvarDados.mat_lote_8;
                    DateTime mat_validade_8 = salvarDados.mat_validade_8;
                    string inst_desc1 = salvarDados.inst_desc1;
                    string inst_cod1 = salvarDados.inst_cod1;
                    DateTime inst_data1 = salvarDados.inst_data1;
                    string inst_desc1_1 = salvarDados.inst_desc1_1;
                    string inst_cod1_1 = salvarDados.inst_cod1_1;
                    DateTime inst_data1_1 = salvarDados.inst_data1_1;
                    string inst_desc2 = salvarDados.inst_desc2;
                    string inst_cod2 = salvarDados.inst_cod2;
                    DateTime inst_data2 = salvarDados.inst_data2;
                    string inst_desc2_2 = salvarDados.inst_desc2_2;
                    string inst_cod2_2 = salvarDados.inst_cod2_2;
                    DateTime inst_data2_2 = salvarDados.inst_data2_2;
                    string inst_desc3 = salvarDados.inst_desc3;
                    string inst_cod3 = salvarDados.inst_cod3;
                    DateTime inst_data3 = salvarDados.inst_data3;
                    string inst_desc3_3 = salvarDados.inst_desc3_3;
                    string inst_cod3_3 = salvarDados.inst_cod3_3;
                    DateTime inst_data3_3 = salvarDados.inst_data3_3;
                    string inst_desc4 = salvarDados.inst_desc4;
                    string inst_cod4 = salvarDados.inst_cod4;
                    DateTime inst_data4 = salvarDados.inst_data4;
                    string inst_desc4_4 = salvarDados.inst_desc4_4;
                    string inst_cod4_4 = salvarDados.inst_cod4_4;
                    DateTime inst_data4_4 = salvarDados.inst_data4_4;
                    string equi_ee = salvarDados.equi_ee;
                    string equi_de = salvarDados.equi_de;
                    string obs = salvarDados.obs;
                    string executado_por = salvarDados.executado_por;
                    string auxiliado_por = salvarDados.auxiliado_por;

                    //conta do resulto final...
                    float result_final = (((absorvancia1 - absorvancia2) * fator_calibracao * 100 * 1000) / (50 * 1000 * massa) * -1);
                    string conv_result_final = result_final.ToString("N1");

                    //calculando cocentração Qc..
                    float concentracao_qc = (((abs_branco - abs_qc) * fator_calibracao * 100 * 1000) / (50 * 1000 * massa) * -1);
                    string conv_concentracao_qc = concentracao_qc.ToString("N3");

                    //massa ,fator_calibracao, abs_branco ,abs_qc

                    //salvar dados no banco.
                    var salvarDadosTabela = new ColetaModel.ArlaFosfato
                    {
                        os = OS,
                        orcamento = orcamento,
                        data_ini = data_ini,
                        data_term = data_term,
                        cod_curva = cod_curva,
                        fator_calibracao = fator_calibracao,
                        massa = massa,
                        absorvancia1 = absorvancia1,
                        absorvancia2 = absorvancia2,
                        result_final = conv_result_final,
                        abs_branco = abs_branco,
                        abs_qc = abs_qc,
                        concentracao_qc = conv_concentracao_qc,
                        mat_prima_1 = mat_prima_1,
                        mat_lote_1 = mat_lote_1,
                        mat_validade_1 = mat_validade_1,
                        mat_prima_2 = mat_prima_2,
                        mat_lote_2 = mat_lote_2,
                        mat_validade_2 = mat_validade_2,
                        mat_prima_3 = mat_prima_3,
                        mat_lote_3 = mat_lote_3,
                        mat_validade_3 = mat_validade_3,
                        mat_prima_4 = mat_prima_4,
                        mat_lote_4 = mat_lote_4,
                        mat_validade_4 = mat_validade_4,
                        mat_prima_5 = mat_prima_5,
                        mat_lote_5 = mat_lote_5,
                        mat_validade_5 = mat_validade_5,
                        mat_prima_6 = mat_prima_6,
                        mat_lote_6 = mat_lote_6,
                        mat_validade_6 = mat_validade_6,
                        mat_prima_7 = mat_prima_7,
                        mat_lote_7 = mat_lote_7,
                        mat_validade_7 = mat_validade_7,
                        mat_prima_8 = mat_prima_8,
                        mat_lote_8 = mat_lote_8,
                        mat_validade_8 = mat_validade_8,
                        inst_desc1 = inst_desc1,
                        inst_cod1 = inst_cod1,
                        inst_data1 = inst_data1,
                        inst_desc1_1 = inst_desc1_1,
                        inst_cod1_1 = inst_cod1_1,
                        inst_data1_1 = inst_data1_1,
                        inst_desc2 = inst_desc2,
                        inst_cod2 = inst_cod2,
                        inst_data2 = inst_data2,
                        inst_desc2_2 = inst_desc2_2,
                        inst_cod2_2 = inst_cod2_2,
                        inst_data2_2 = inst_data2_2,
                        inst_desc3 = inst_desc3,
                        inst_cod3 = inst_cod3,
                        inst_data3 = inst_data3,
                        inst_desc3_3 = inst_desc3_3,
                        inst_cod3_3 = inst_cod3_3,
                        inst_data3_3 = inst_data3_3,
                        inst_desc4 = inst_desc4,
                        inst_cod4 = inst_cod4,
                        inst_data4 = inst_data4,
                        inst_desc4_4 = inst_desc4_4,
                        inst_cod4_4 = inst_cod4_4,
                        inst_data4_4 = inst_data4_4,
                        equi_ee = equi_ee,
                        equi_de = equi_de,
                        obs = obs,
                        executado_por = executado_por,
                        auxiliado_por = auxiliado_por,
                    };

                    //salvando....
                    _qcontext.Add(salvarDadosTabela);
                    await _qcontext.SaveChangesAsync();

                    TempData["Mensagem"] = "Dados Salvos Com Sucesso.";
                    return RedirectToAction(nameof(EnsaioFosfato), new { OS, orcamento });
                }
                else
                {
                    TempData["Mensagem"] = "Desculpe, verifique a os e orcamento estão corretas.";
                    return RedirectToAction(nameof(EnsaioFosfato), new { OS, orcamento });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SalvarDensidade(string OS, string orcamento, [Bind("data_ini,data_term,temp_inicial,densidade_enc,temp_final,conc_ensaio,,mat_prima,mat_lote,mat_validade,inst_desc1,inst_cod1,inst_data1,inst_desc2,inst_cod2,inst_data2,,inst_desc3,inst_cod3,inst_data3,,inst_desc4,inst_cod4,inst_data4,equi_de,equi_ee,obs,executado_por,auxiliado_por,exec_temp_ini,tem_amostra,exec_densi_encont,exec_temp_final")] ColetaModel.ArlaDensidade salvarDados)
        {
            try
            {
                //verificando se existe dados com essa os.
                var editarDados = _qcontext.arla_densidade.Where(x => x.os == OS && x.orcamento == orcamento).FirstOrDefault();

                if (editarDados == null)
                {
                    if (OS != null && OS != "0" && orcamento != "0")
                    {
                        //pegando valores dos dados inseridos na pagina.
                        DateTime data_ini = salvarDados.data_ini;
                        DateTime data_term = salvarDados.data_term;
                        string temp_inicial = salvarDados.temp_inicial;
                        string densidade_enc = salvarDados.densidade_enc.ToUpper();
                        string temp_final = salvarDados.temp_final;
                        string densidade_banho;
                        string conc_ensaio = salvarDados.conc_ensaio;
                        string mat_prima = salvarDados.mat_prima;
                        string mat_lote = salvarDados.mat_lote;
                        DateTime mat_validade = salvarDados.mat_validade;
                        string inst_desc1 = salvarDados.inst_desc1;
                        string inst_cod1 = salvarDados.inst_cod1;
                        DateTime inst_data1 = salvarDados.inst_data1;
                        string inst_desc2 = salvarDados.inst_desc2;
                        string inst_cod2 = salvarDados.inst_cod2;
                        DateTime inst_data2 = salvarDados.inst_data2;
                        string inst_desc3 = salvarDados.inst_desc3;
                        string inst_cod3 = salvarDados.inst_cod3;
                        DateTime inst_data3 = salvarDados.inst_data3;
                        string inst_desc4 = salvarDados.inst_desc4;
                        string inst_cod4 = salvarDados.inst_cod4;
                        DateTime inst_data4 = salvarDados.inst_data4;
                        string equi_de = salvarDados.equi_de;
                        string equi_ee = salvarDados.equi_ee;
                        string obs = salvarDados.obs;
                        string executado_por = salvarDados.executado_por;
                        string auxiliado_por = salvarDados.auxiliado_por;
                        string exec_temp_ini = salvarDados.exec_temp_ini;
                        string tem_amostra = salvarDados.tem_amostra;
                        string exec_densi_encont = salvarDados.exec_densi_encont;
                        string exec_temp_final = salvarDados.exec_temp_final;

                        //realizando a conta e conversão da densidade ambiente.
                        float convertendo_densidade_enc = float.Parse(exec_densi_encont);
                        var result_conversao_encontrada = convertendo_densidade_enc / ((1 - 23 * Math.Pow(10, -6) * (20 - 15) - 23 * Math.Pow(10, -8) * Math.Pow((20 - 15), 2)));
                        result_conversao_encontrada = Math.Round(result_conversao_encontrada, 2);
                        string salvar_valor_result_conversao_encontrada = result_conversao_encontrada.ToString() + " kg / m³";

                        //realizando a conta e conversao da densidade banho maria.
                        if (densidade_enc == "NA")
                        {
                            densidade_banho = "NA";
                        }
                        else
                        {
                            float convertendo_dens_banho_maria = float.Parse(densidade_enc);
                            var result_conv_banho_maria = convertendo_dens_banho_maria / ((1 - 23 * Math.Pow(10, -6) * (20 - 15) - 23 * Math.Pow(10, -8) * Math.Pow((20 - 15), 2)));
                            result_conv_banho_maria = Math.Round(result_conv_banho_maria, 2);
                            string salvar_valor_banho_maria = result_conv_banho_maria.ToString() + " kg / m³";

                            densidade_banho = salvar_valor_banho_maria;
                        }

                        //passando os valores para a tabela.
                        var salvarDadosTabela = new ColetaModel.ArlaDensidade
                        {
                            os = OS,
                            orcamento = orcamento,
                            data_ini = data_ini,
                            data_term = data_term,
                            temp_inicial = temp_inicial,
                            densidade_enc = densidade_enc,
                            temp_final = temp_final,
                            densidade_ambiente = salvar_valor_result_conversao_encontrada,
                            densidade_banho = densidade_banho,
                            conc_ensaio = conc_ensaio,
                            mat_prima = mat_prima,
                            mat_lote = mat_lote,
                            mat_validade = mat_validade,
                            inst_desc1 = inst_desc1,
                            inst_cod1 = inst_cod1,
                            inst_data1 = inst_data1,
                            inst_desc2 = inst_desc2,
                            inst_cod2 = inst_cod2,
                            inst_data2 = inst_data2,
                            inst_desc3 = inst_desc3,
                            inst_cod3 = inst_cod3,
                            inst_data3 = inst_data3,
                            inst_desc4 = inst_desc4,
                            inst_cod4 = inst_cod4,
                            inst_data4 = inst_data4,
                            equi_de = equi_de,
                            equi_ee = equi_ee,
                            obs = obs,
                            executado_por = executado_por,
                            auxiliado_por = auxiliado_por,
                            exec_temp_ini = exec_temp_ini,
                            tem_amostra = tem_amostra,
                            exec_densi_encont = exec_densi_encont,
                            exec_temp_final = exec_temp_final,
                        };

                        //salvando no banco.
                        _qcontext.Add(salvarDadosTabela);
                        await _qcontext.SaveChangesAsync();

                        TempData["Mensagem"] = "Dados Salvos Com Sucesso.";
                        return RedirectToAction(nameof(EnsaioDensidade), new { OS, orcamento });
                    }
                    else
                    {
                        TempData["Mensagem"] = "Desculpe, verifique a os e orcamento estão corretas.";
                        return RedirectToAction(nameof(EnsaioDensidade), new { OS, orcamento });
                    }
                }
                else
                {
                    if(OS != null && OS != "0" && orcamento != "0")
                    {
                        //pegando valores dos dados inseridos na pagina.
                        editarDados.data_ini = salvarDados.data_ini;
                        editarDados.data_term = salvarDados.data_term;
                        editarDados.temp_inicial = salvarDados.temp_inicial;
                        string densidade_enc = salvarDados.densidade_enc.ToUpper();
                        editarDados.temp_final = salvarDados.temp_final;
                        string densidade_banho;
                        editarDados.conc_ensaio = salvarDados.conc_ensaio;
                        editarDados.mat_prima = salvarDados.mat_prima;
                        editarDados.mat_lote = salvarDados.mat_lote;
                        editarDados.mat_validade = salvarDados.mat_validade;
                        editarDados.inst_desc1 = salvarDados.inst_desc1;
                        editarDados.inst_cod1 = salvarDados.inst_cod1;
                        editarDados.inst_data1 = salvarDados.inst_data1;
                        editarDados.inst_desc2 = salvarDados.inst_desc2;
                        editarDados.inst_cod2 = salvarDados.inst_cod2;
                        editarDados.inst_data2 = salvarDados.inst_data2;
                        editarDados.inst_desc3 = salvarDados.inst_desc3;
                        editarDados.inst_cod3 = salvarDados.inst_cod3;
                        editarDados.inst_data3 = salvarDados.inst_data3;
                        editarDados.inst_desc4 = salvarDados.inst_desc4;
                        editarDados.inst_cod4 = salvarDados.inst_cod4;
                        editarDados.inst_data4 = salvarDados.inst_data4;
                        editarDados.equi_de = salvarDados.equi_de;
                        editarDados.equi_ee = salvarDados.equi_ee;
                        editarDados.obs = salvarDados.obs;
                        editarDados.executado_por = salvarDados.executado_por;
                        editarDados.auxiliado_por = salvarDados.auxiliado_por;
                        editarDados.exec_temp_ini = salvarDados.exec_temp_ini;
                        editarDados.tem_amostra = salvarDados.tem_amostra;
                        string exec_densi_encont = salvarDados.exec_densi_encont;
                        editarDados.exec_temp_final = salvarDados.exec_temp_final;

                        //realizando a conta e conversão da densidade ambiente.
                        float convertendo_densidade_enc = float.Parse(exec_densi_encont);
                        var result_conversao_encontrada = convertendo_densidade_enc / ((1 - 23 * Math.Pow(10, -6) * (20 - 15) - 23 * Math.Pow(10, -8) * Math.Pow((20 - 15), 2)));
                        result_conversao_encontrada = Math.Round(result_conversao_encontrada, 2);
                        string salvar_valor_result_conversao_encontrada = result_conversao_encontrada.ToString() + " kg / m³";

                        //realizando a conta e conversao da densidade banho maria.
                        if (densidade_enc == "NA")
                        {
                            densidade_banho = "NA";
                        }
                        else
                        {
                            float convertendo_dens_banho_maria = float.Parse(densidade_enc);
                            var result_conv_banho_maria = convertendo_dens_banho_maria / ((1 - 23 * Math.Pow(10, -6) * (20 - 15) - 23 * Math.Pow(10, -8) * Math.Pow((20 - 15), 2)));
                            result_conv_banho_maria = Math.Round(result_conv_banho_maria, 2);
                            string salvar_valor_banho_maria = result_conv_banho_maria.ToString() + " kg / m³";

                            densidade_banho = salvar_valor_banho_maria;
                        }

                        //EDITANDO NO BANCO.
                        await _qcontext.SaveChangesAsync();

                        TempData["Mensagem"] = "Dados Editado com Sucesso.";
                        return RedirectToAction(nameof(EnsaioDensidade), new { OS, orcamento });
                    }
                    else
                    {
                        TempData["Mensagem"] = "Desculpe, verifique a os e orcamento estão corretas.";
                        return RedirectToAction(nameof(EnsaioDensidade), new { OS, orcamento });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;
            }
        }
    }
}



