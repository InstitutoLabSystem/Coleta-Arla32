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
                var listagem = (
                    from pr in _context.programacao_lab_ensaios
                    join ite in _context.ordemservicocotacaoitem_hc_copylab
                      on new { orcamento = pr.Orcamento, item = int.Parse(pr.Item) } equals new { orcamento = ite.orcamento, item = ite.Item } into join1
                    from x in join1.DefaultIfEmpty()
                    join hc in _context.wmoddetprod
                        on x.CodigoEnsaio equals hc.codmaster into join2
                    from y in join2.DefaultIfEmpty()
                    where pr.OS == OS
                    orderby y.codigo
                    select new
                    {
                        CodMaster = y.codmaster,
                        codigo = y.codigo,
                        descricao = y.descricao,
       
                        OS = pr.OS
                    }
                ).ToList();



                // Verificar se listagem não é nula e se há resultados
                if (listagem != null && listagem.Count > 0)
                {
                    // Faça algo com os resultados
                    return View("Index", listagem);
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

