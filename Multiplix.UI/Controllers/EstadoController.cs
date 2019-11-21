using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;

namespace Multiplix.UI.Controllers
{
    public class EstadoController : Controller
    {
        private IServiceEstado _serviceEstado;

        public EstadoController(IServiceEstado serviceEstado)
        {
            _serviceEstado = serviceEstado;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult PesquisaEstado(string searchTerm, int pageNumber)
        {
            /*
             * consumido por um Select2 ajax
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com Select2
             */

            int pageSize = 10;

            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            IEnumerable<Estado> parceiros = new List<Estado>();

            if (!String.IsNullOrEmpty(searchTerm))
                parceiros = _serviceEstado.Buscar(x => x.Nome.Contains(searchTerm));
            else
                parceiros = _serviceEstado.ObterTodos();

            int totalResults = parceiros.Count();
            parceiros = parceiros.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var estado in parceiros)
            {
                Dictionary<string, string> result_item = new Dictionary<string, string>();
                result_item.Add("id", estado.EstadoId + "");
                result_item.Add("text", estado.Nome);
                results.Add(result_item);
            }

            return Json(new
            {
                pageSize,
                results,
                totalResults
            });
        }
    }
}