using Arla32.Data;
using Arla32.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
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

        public IActionResult EnsaioAlcalinidade(string OS, string orcamento)
        {
            ViewBag.OS = OS;
            ViewBag.orcamento = orcamento;

            return View();
        }
        public IActionResult EnsaioInsoluveis(string OS, string orcamento)
        {
            var dados = _qcontext.arla_insoluveis.Where(x => x.os == OS).FirstOrDefault();
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

        public async Task<IActionResult> SalvarAldeidos(string OS, string orcamento, [Bind("data_ini,data_term,os, norma, np, descricao,col_norma,col_np,col_desc,lote_sol," +
            "codigo_curva,fator_calibracao,massa_branco,absorbancia_branco,maximo_permitido,massa_amostra,absorbancia_amostra\"" +
            " carta_absorbancia,carta_concentracao,mat_prima1,mat_lote1,mat_validade1,mat_prima2, mat_lote2,mat_validade2,mat_prima3,mat_lote3,mat_validade3,mat_prima4,mat_lote4,mat_validade4," +
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
                string fator_calibracao = aldeidos.fator_calibracao;
                float massa_branco = aldeidos.massa_branco;
                float absorbancia_branco = aldeidos.absorbancia_branco;
                float massa_amostra = aldeidos.massa_amostra;
                float absorbancia_amostra = aldeidos.absorbancia_amostra;
                string maximo_permitido = aldeidos.maximo_permitido;
                float carta_absorbancia = aldeidos.carta_absorbancia;
                float carta_concentracao = aldeidos.carta_concentracao;
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
                    string.IsNullOrEmpty(fator_calibracao) ||
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
                        maximo_permitido = maximo_permitido,
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
                    _qcontext.Add(salvardados);
                    await _qcontext.SaveChangesAsync();
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

        public async Task<IActionResult> SalvarInsoluveis (string OS, string orcamento, [Bind("data_ini, data_term, norma, np, descricao, preparacao_placa1,preparacao_filtro1,preparacao_massa1," +
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
                float preparacao_filtro1 = insoluveis.preparacao_filtro1; float preparacao_massa1 = insoluveis.preparacao_massa1;
                float preparacao_placa2 = insoluveis.preparacao_placa2; float preparacao_filtro2 = insoluveis.preparacao_filtro2;
                float preparacao_massa2 = insoluveis.preparacao_massa2; float pesagem_amostra1 = insoluveis.pesagem_amostra1;
                float pesagem_placa1 = insoluveis.pesagem_placa1; float pesagem_filtro1 = insoluveis.pesagem_filtro1;
                float pesagem_resultado1 = insoluveis.pesagem_resultado1; float pesagem_amostra2 = insoluveis.pesagem_amostra2;
                float pesagem_placa2 = insoluveis.pesagem_placa2; float pesagem_filtro2 = insoluveis.pesagem_filtro2; float pesagem_resultado2 = insoluveis.pesagem_resultado2;
                float pesagem_media = insoluveis.pesagem_media; float pesagem_resultfinal = insoluveis.pesagem_resultfinal;
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
                pesagem_media = pesagem_media,
                pesagem_resultfinal = pesagem_resultfinal,
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
                inst_cod4  = inst_cod4,
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



    }
    }

