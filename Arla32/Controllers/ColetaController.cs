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


        [HttpPost]

        public async Task<IActionResult> SalvarAldeidos (int OS, [Bind("data_ini,data_term,os,col_norma,col_np,col_desc,lote_sol," +
            "codigo_curva,fator_calibracao,massa_branco,absorbancia_branco,maximo_permitido,massa_amostra,absorbancia_amostra\"" +
            " carta_absorbancia,carta_concentracao,mat_prima1,mat_lote1,mat_validade,mat_prima3,mat_lote3,mat_validade3,mat_prima4,mat_lote4,mat_validade4," +
            "inst_desc1,inst_validade1,inst_desc2,inst_cod2,inst_validade2,inst_desc1_1,inst_cod1_1,inst_validade1_1,inst_desc2_2,inst_cod_2_2,inst_validade2_2 "
              )] ColetaModel.Aldeidos aldeidos ){

        }

           




    }
}
