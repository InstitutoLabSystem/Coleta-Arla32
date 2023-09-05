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
        public IActionResult BuscarOS(string OS)
        {
            

            var resultado = (
            from pr in _context.programacao_lab_ensaios
            join ite in _context.ordemservicocotacaoitem_hc_copylab
                on new { pr.Orcamento, pr.Item } equals new { ite.Orcamento, ite.Item } into join1
            from hc in _context.wmoddetprod.DefaultIfEmpty()
            where pr.OS == OS
            select new HomeModel.BuscarOs
            {
                CodMaster = hc != null ? hc.CodMaster : null,
                codigo = hc != null ? hc.codigo : null,
                Descricao = hc != null ? hc.Descricao : null,
                NA = pr.NA,
                PK = pr.PK,
                OS = pr.OS,
 
            }
        ).ToList();





            // Faça algo com o resultado, como retorná-lo ou passá-lo para a view
            return View(resultado);
        }






    }








}

