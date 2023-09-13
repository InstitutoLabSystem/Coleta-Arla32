using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arla32.Controllers
{
    [Authorize]
    public class ColetaController : Controller
    {
        public IActionResult EnsaioConcentracao()
        {
            return View();
        }
        public IActionResult EnsaioAldeidos()
        {
            return View();
        }

        public IActionResult EnsaioAlcalinidade()
        {
            return View();
        }
        public IActionResult EnsaioInsoluveis()
        {
            return View();
        }
        public IActionResult EnsaioFosfato()
        {
            return View();
        }
        public IActionResult EnsaioIdentidade(){
             return View();
        }
        public IActionResult EnsaioBiureto()
        {
            return View();
        }
        public IActionResult EnsaioMetais(string OS)
        {
            ViewBag.OS = OS;
            return View();
        }
    }
}
