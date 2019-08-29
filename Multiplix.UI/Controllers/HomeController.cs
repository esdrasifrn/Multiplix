using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.UI.Models;

namespace Multiplix.UI.Controllers
{
    public class HomeController : Controller
    {
        private IServicePatrocinador _servicePatrocinador;

        public HomeController(IServicePatrocinador servicePatrocinador)
        {
            _servicePatrocinador = servicePatrocinador;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }


        public IActionResult SalvarTeste()
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO();
            usuarioDTO.PatrocinadorId = 0;
            usuarioDTO.Nome = "Esdras";
            usuarioDTO.Email = "esdras.valentim@yahoo.com.br";
            usuarioDTO.Login = "esdras";
            usuarioDTO.Senha = "123";
            usuarioDTO.Liberado = true;
            usuarioDTO.SuperUser = 0;

            var result = _servicePatrocinador.SalvarPatrocinadorSemConvite(usuarioDTO);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
