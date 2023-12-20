using Arla32.Data;
using System.Security.Claims;
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
using Humanizer.DateTimeHumanizeStrategy;
using static Arla32.Models.ColetaModel;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Arla32.Controllers
{
    [Authorize]
    public class LotesController : Controller
    {
        private readonly ILogger<LotesController> _logger;
        private readonly BancoContext _context;
        private readonly QuimicoContext _qcontext;

        public LotesController(ILogger<LotesController> logger, BancoContext context, QuimicoContext qcontext)
        {
            _logger = logger;
            _context = context;
            _qcontext = qcontext;

        }

        public IActionResult Index(string OS)
        {
              return View();
        
        }

        public IActionResult Editar(int Id)
        {
            var dados = _qcontext.arla_lotes.Where(x => x.id == Id).FirstOrDefault();

            return View(dados);
        }

        public async Task<IActionResult> ListarLotes(string escolha)
        {
            try
            {
                var resultado = (from p in _qcontext.arla_lotes
                                 where p.anexo == escolha
                                 select new lotesModel.ArlaLotes
                                 {
                                        id = p.id,
                                        lote = p.lote,
                                        anexo = p.anexo,

                                 }).Distinct().ToList(); ;


                // Verificar se listagem não é nula e se há resultados
                if (resultado != null)
                {
                    return View("Index", (resultado));
                }
                else
                {
                    TempData["Mensagem"] = "Erro.";
                    return View("Index");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;
            }

        }

        public async Task<IActionResult> EditarLotes(int Id, lotesModel.ArlaLotes dados)
        {
            try
            {
                var editar = _qcontext.arla_lotes.Where(x => x.id == Id).FirstOrDefault();

                editar.id = dados.id;
                editar.lote = dados.lote;
                editar.anexo = dados.anexo;

                _qcontext.Update(editar);
                await _qcontext.SaveChangesAsync();

                return View("Editar", dados);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error", ex.Message);
                throw;
            }


        }
       

    }
}

