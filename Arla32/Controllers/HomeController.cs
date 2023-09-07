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

namespace Arla32.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BancoContext _context;

        public HomeController(ILogger<HomeController> logger, BancoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var nomeUsuarioClaim = User.FindFirstValue(ClaimTypes.Name);
            ViewBag.NomeUsuario = nomeUsuarioClaim;
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
                var resultado = _context.programacao_lab_ensaios
                  .Where(p => p.OS == OS)
                  .Select(p => new HomeModel.Resposta
                  {
                      OS = p.OS,
                      NA = p.NA,
                      Orcamento = p.Orcamento,
                      Item = p.Item,
                      CodigoEnsaio = _context.ordemservicocotacaoitem_hc_copylab
                          .Where(o => o.orcamento == p.Orcamento)
                          .Select(o => o.CodigoEnsaio)
                          .ToList(),
                      Descricao = _context.wmoddetprod
                          .Where(w => _context.ordemservicocotacaoitem_hc_copylab
                              .Where(o => o.orcamento == p.Orcamento)
                              .Select(o => o.CodigoEnsaio)
                              .Contains(w.codmaster))
                          .Select(w => w.descricao)
                          .ToList(),
                  })
                  .ToList();



                // Verificar se listagem não é nula e se há resultados
                if (resultado != null && resultado.Count > 0)
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





    }


}

