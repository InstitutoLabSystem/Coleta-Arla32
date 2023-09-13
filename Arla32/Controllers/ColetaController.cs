using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arla32.Controllers
{
    [Authorize]
    public class ColetaController : Controller
    {
        public IActionResult EnsaioConcentracao(string OS)
        {
            ViewBag.OS = OS;
            return View();
        }
        public IActionResult EnsaioAldeidos(string OS)
        {
            ViewBag.OS = OS;
            return View();
        }

        public IActionResult EnsaioAlcalinidade(string OS)
        {
            ViewBag.OS = OS;
            return View();
        }
        public IActionResult EnsaioInsoluveis(string OS)
        {
            ViewBag.OS = OS;
            return View();
        }
        public IActionResult EnsaioFosfato(string OS)
        {
            ViewBag.OS = OS;
            return View();
        }
        public IActionResult EnsaioIdentidade(string OS)
        {
            ViewBag.OS = OS;
            return View();
        }
        public IActionResult EnsaioBiureto(string OS)
        {
            ViewBag.OS = OS;
            return View();
        }
        public IActionResult EnsaioMetais(string OS)
        {
            ViewBag.OS = OS;
            return View();
        }
    }
}
