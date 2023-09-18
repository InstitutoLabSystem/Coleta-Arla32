using Arla32.Models;
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


        [HttpPost]
        public async Task<IActionResult> BuscarOS(string OS)
        {
            try
            {

                var resultado = (from p in _context.programacao_lab_ensaios
                                 where p.OS == OS
                                 join o in _context.ordemservicocotacaoitem_hc_copylab
                                 on p.Orcamento equals o.orcamento
                                 join w in _context.wmoddetprod
                                 on o.CodigoEnsaio equals w.codmaster
                                 join x in _context.ordemservicocotacao_hc_copylab
                                on (p.Orcamento) equals (x.codigo + x.mes + x.ano)
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
                                     qtdAmostra = o.qtdAmostra, 
                                     Ano = o.Ano,
                                     Rev = o.Rev,
                                     codigo = w.codigo,
                                 }).Distinct().ToList();

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

        public async Task<IActionResult> IniciarColeta(string OS, HomeModel.Resposta resultado)
        {
            try
            {
              
                
                if(resultado != null)
                {
                    var IniciarColeta = new IniciarColeta
                    {
                        os = OS,
                        revisao_os = resultado.Rev,
                        orcamento = resultado.Orcamento,
                        ensaio = "ARLA 32",
                        Qtd_Recebida = resultado.qtdAmostra.ToString(),
                        laboratorio = "Quimico",
                        CodCli = !string.IsNullOrEmpty(resultado.CodCli) ? int.Parse(resultado.CodCli) : 0,
                        CodSol = !string.IsNullOrEmpty(resultado.CodSol) ? int.Parse(resultado.CodSol) : 0

                    };
                    //_qcontext.Add(IniciarColeta);
                    //await _qcontext.SaveChangesAsync();
                    return View("Index");

                }
                else
                {
                    TempData["Mensagem"] = "Não foi possível salvar os dados";
                    return View("Index");
                }


            }catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;
            }



        }




    }


}

