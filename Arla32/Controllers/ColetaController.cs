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
    }
}
