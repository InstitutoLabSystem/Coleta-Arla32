﻿using Arla32.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NuGet.Common;
using Arla32.Data;
using static Arla32.Models.HomeModel;
using System.Security.Policy;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Humanizer.DateTimeHumanizeStrategy;

namespace Arla32.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BancoContext _context;
        private readonly QuimicoContext _qcontext;

        public HomeController(ILogger<HomeController> logger, BancoContext context, QuimicoContext qcontext)
        {
            _logger = logger;
            _context = context;
            _qcontext = qcontext;

        }

        public IActionResult Index(string OS)
        {
            var nomeUsuarioClaim = User.FindFirstValue(ClaimTypes.Name);
            ViewBag.NomeUsuario = nomeUsuarioClaim;

            ViewBag.OS = OS;


            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acess");
        }


        public async Task<IActionResult> BuscarOS(string OS)
        {
            try
            {
                var resultado = (from p in _context.programacao_lab_ensaios
                                 join o in _context.ordemservicocotacaoitem_hc_copylab
                                 on p.Orcamento equals o.orcamento
                                 join w in _context.wmoddetprod
                                 on o.CodigoEnsaio equals w.codmaster
                                 join x in _context.ordemservicocotacao_hc_copylab
                                 on (p.Orcamento) equals (x.codigo + x.mes + x.ano)
                                 where p.OS == OS
                                 select new HomeModel.Resposta
                                 {
                                     OS = p.OS,
                                     NA = p.NA,
                                     Orcamento = p.Orcamento,
                                     Item = p.Item,
                                     CodigoEnsaio = o.CodigoEnsaio,
                                     Descricao = w.descricao,
                                     NormaOS = o.NormaOS,
                                     CodCli = x.CodCli,
                                     CodSol = x.CodSol,
                                     Ano = o.Ano,
                                     Rev = o.Rev,
                                     codigo = w.codigo,
                                 }).Distinct().ToList();


                var isOsSalva = _qcontext.regtro_amostra_painel_copy.Any(ic => ic.OS == OS);
                if (isOsSalva != null)
                {
                    ViewBag.IsOsSalva = isOsSalva;
                }
                else
                {
                    ViewBag.IsOsSalva = false;
                }

                // Verificar se listagem não é nula e se há resultados
                if (resultado != null)
                {
                    // Faça algo com os resultados
                    return View("Index", resultado);
                }
                else
                {
                    TempData["Mensagem"] = "Orçamento não encontrado.";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar orçamento: {}", ex.Message);
                throw;
            }
        }


        public async Task<IActionResult> IniciarColeta(string OS, [Bind("OS,orcamento,Qtd_Recebida,norma,revisao_os," +
            "CodCli, CodSol, Item")] HomeModel.IniciarColeta salvar)
        {
            try
            {
                // pegando os dados do html para salva-los
                var orcamento = salvar.orcamento;
                var Qtd_Recebida = salvar.Qtd_Recebida;
                var norma = salvar.norma;
                var revisao_os = salvar.revisao_os;
                var CodCli = salvar.CodCli;
                var CodSol = salvar.CodSol;
                var Item = salvar.Item_orcamento;

                //pegando os 2 ultimos digitos do ano.
                int Ano = Int32.Parse(DateTime.Now.Year.ToString().Substring(2));

                var IniciarColeta = new IniciarColeta
                {
                    OS = OS,
                    ano = Ano,
                    data_entrada = DateTime.Now.Date,
                    revisao_os = revisao_os,
                    orcamento = orcamento,
                    ensaio = "ARLA 32",
                    Qtd_Recebida = Qtd_Recebida,
                    laboratorio = "Quimico",
                    CodCli = CodCli,
                    CodSol = CodSol,
                    status = "Coleta",
                    norma = norma,
                    Item_orcamento = Item,

                };

                // salvando os valores recebidos.
                _qcontext.Add(IniciarColeta);
                await _qcontext.SaveChangesAsync();

                //recebendo o id do banco de dados p/ salvar amostra_id
                int idGerado = IniciarColeta.id;
                string convert_amostra_id = idGerado.ToString() + Ano.ToString();
                IniciarColeta.amostra_id = Int32.Parse(convert_amostra_id);

                //atualizando o campo de amostra_id
                _qcontext.Update(IniciarColeta);
                await _qcontext.SaveChangesAsync();

                TempData["Mensagem"] = "Pronto! Agora o Ensaido esta pronto para começar.";
                return RedirectToAction("BuscarOS", new { OS });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;
            }
        }
    }
}

