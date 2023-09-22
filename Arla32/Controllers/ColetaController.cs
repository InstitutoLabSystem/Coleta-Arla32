using Arla32.Data;
using Arla32.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Versioning;
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
            ViewBag.OS = OS;
            ViewBag.orcamento = orcamento;

            return View();
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
        public async Task<IActionResult> SalvarConcentracao(string OS, string orcamento, [Bind("data_ini,data_term,lote_solucao,codigo_curva,fator_avaliacao,indice_agua,refracao_amostra1,refracao_amostra2,conc_biureto,conc_ureia,desc1_instrumento,codigo1_instrumento,validade1_instrumento,desc2_instrumento,codigo2_instrumento,validade2_instrumento,ee_equipamento,de_equipamento,obs,executado_por,auxiliado_por")] ColetaModel.ArlaConcentracao salvarDados)
        {
            //pegando os valores dos inputs no html.
            DateTime data_ini = salvarDados.data_ini;
            DateTime data_term = salvarDados.data_term;
            string lote_solucao = salvarDados.lote_solucao;
            string codigo_curva = salvarDados.codigo_curva;
            string fator_avaliacao = salvarDados.fator_avaliacao;
            float indice_agua = salvarDados.indice_agua;
            float refracao_amostra1 = salvarDados.refracao_amostra1;
            float refracao_amostra2 = salvarDados.refracao_amostra2;
            float conc_biureto = salvarDados.conc_biureto;
            float conc_ureia = salvarDados.conc_ureia;
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
                conc_biureto = conc_biureto,
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
                if ((OS != null && OS != "0") || (orcamento != null && orcamento != "0"))
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

                    // salvando no banco de dados.
                    var guardarDadosTabela = new ArlaAlcalinidade
                    {
                        os = OS,
                        orcamento = orcamento,
                        data_ini = data_ini,
                        data_term = data_term,
                        pre_massa_amostra = pre_massa_amostra,
                        pre_vol_titulado = pre_vol_titulado,
                        det_massa_amostra1 = det_massa_amostra1,
                        det_massa_amostra2 = det_massa_amostra2,
                        det_vol_titulado1 = det_vol_titulado1,
                        det_vol_titulado2 = det_vol_titulado2,
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

                    //_qcontext.Add(guardarDadosTabela);
                    //await _qcontext.SaveChangesAsync();
                    TempData["Mensagem"] = "salvo com Sucesso.";
                    return RedirectToAction(nameof(EnsaioAlcalinidade), new { OS, orcamento });
                }
                else
                {
                    TempData["Mensagem"] = "Desculpe, verifique a os e orcamento pela url se estão corretas.";
                    return RedirectToAction(nameof(EnsaioAlcalinidade), new { OS, orcamento });
                }
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

                    //verificando existe dados vazios.
                    if (string.IsNullOrEmpty(data_ini.ToString()) || string.IsNullOrEmpty(data_term.ToString()) || string.IsNullOrEmpty(codigo_curva) || fator_calibracao == 0 || fator_dia == 0 || amostra1 == 0 || amostra2 == 0 || absorbancia_1 == 0 || absorbancia_2 == 0 || absorbancia_3 == 0 || string.IsNullOrEmpty(mat_prima_1) || string.IsNullOrEmpty(mat_lote_1) || string.IsNullOrEmpty(mat_validade_1.ToString()) || string.IsNullOrEmpty(mat_prima_2) || string.IsNullOrEmpty(mat_lote_2) || string.IsNullOrEmpty(mat_validade_2.ToString()) || string.IsNullOrEmpty(mat_prima_3) || string.IsNullOrEmpty(mat_prima_3) || string.IsNullOrEmpty(mat_lote_3) || string.IsNullOrEmpty(mat_validade_3.ToString()) ||
                        string.IsNullOrEmpty(mat_prima_4) || string.IsNullOrEmpty(mat_lote_4) || string.IsNullOrEmpty(mat_validade_4.ToString()) || string.IsNullOrEmpty(mat_prima_5) || string.IsNullOrEmpty(mat_lote_5) || string.IsNullOrEmpty(mat_validade_5.ToString()) || string.IsNullOrEmpty(inst_desc1) || string.IsNullOrEmpty(inst_codigo1) || string.IsNullOrEmpty(inst_data1.ToString()) || string.IsNullOrEmpty(inst_desc1_1) || string.IsNullOrEmpty(inst_codigo1_1) || string.IsNullOrEmpty(inst_data1_1.ToString()) || string.IsNullOrEmpty(insta_desc2) || string.IsNullOrEmpty(inst_codigo2) || string.IsNullOrEmpty(inst_data2.ToString()) || string.IsNullOrEmpty(inst_desc2_2) || string.IsNullOrEmpty(inst_codigo2_2) || string.IsNullOrEmpty(inst_data2_2.ToString()) ||
                        string.IsNullOrEmpty(inst_desc3) || string.IsNullOrEmpty(inst_codigo3) || string.IsNullOrEmpty(inst_data3.ToString()) || string.IsNullOrEmpty(inst_desc3_3) || string.IsNullOrEmpty(inst_codigo3_3) || string.IsNullOrEmpty(inst_data3_3.ToString()) || string.IsNullOrEmpty(inst_desc4) || string.IsNullOrEmpty(inst_codigo4) || string.IsNullOrEmpty(inst_data4.ToString()) || string.IsNullOrEmpty(inst_desc4_4) || string.IsNullOrEmpty(inst_codigo4_4) || string.IsNullOrEmpty(inst_data4_4.ToString()))
                    {
                        TempData["Mensagem"] = "Preencha Todos Os Campos.";
                        return RedirectToAction(nameof(EnsaioBiureto), new { OS, orcamento });
                    }

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
                    float concentracao_qc = (((abs_branco- abs_qc) * fator_calibracao * 100 * 1000) /(50 * 1000 * massa) * -1);
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
                    var  result_conversao_encontrada = convertendo_densidade_enc / ((1 - 23 * Math.Pow(10, -6) * (20 - 15) - 23 * Math.Pow(10, -8) * Math.Pow((20 - 15), 2)));
                    result_conversao_encontrada = Math.Round(result_conversao_encontrada, 2);
                    string salvar_valor_result_conversao_encontrada = result_conversao_encontrada.ToString() + " kg / m³";

                    //realizando a conta e conversao da densidade banho maria.
                    if(densidade_enc == "NA")
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
                        equi_ee =equi_ee,
                        obs = obs,
                        executado_por = executado_por,
                        auxiliado_por = auxiliado_por,
                        exec_temp_ini = exec_temp_ini,
                        tem_amostra = tem_amostra,
                        exec_densi_encont = exec_densi_encont,
                        exec_temp_final = exec_temp_final,
                    };

                    //salvando no banco.
                    //_qcontext.Add(salvarDadosTabela);
                    //await _qcontext.SaveChangesAsync();

                    TempData["Mensagem"] = "Dados Salvos Com Sucesso.";
                    return RedirectToAction(nameof(EnsaioDensidade), new { OS, orcamento });
                }
                else
                {
                    TempData["Mensagem"] = "Desculpe, verifique a os e orcamento estão corretas.";
                    return RedirectToAction(nameof(EnsaioDensidade), new { OS, orcamento });
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
