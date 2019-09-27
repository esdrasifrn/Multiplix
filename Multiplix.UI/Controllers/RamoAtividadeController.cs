using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;

namespace Multiplix.UI.Controllers
{
    public class RamoAtividadeController : Controller
    {
        private IServiceRamoAtividade _serviceRamoAtividade;

        public RamoAtividadeController(IServiceRamoAtividade  serviceRamoAtividade)
        {
            _serviceRamoAtividade = serviceRamoAtividade;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult PesquisaRamoAtividade(string searchTerm, int pageNumber)
        {
            /*
             * consumido por um Select2 ajax
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com Select2
             */

            int pageSize = 10;

            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            IEnumerable<RamoAtividade> ramosAtividade = new List<RamoAtividade>();

            if (!String.IsNullOrEmpty(searchTerm))
                ramosAtividade = _serviceRamoAtividade.Buscar(x => x.Nome.Contains(searchTerm));
            else
                ramosAtividade = _serviceRamoAtividade.ObterTodos();

            int totalResults = ramosAtividade.Count();
            ramosAtividade = ramosAtividade.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var ramoAtividade in ramosAtividade)
            {
                Dictionary<string, string> result_item = new Dictionary<string, string>();
                result_item.Add("id", ramoAtividade.RamoAtividadeId + "");
                result_item.Add("text", ramoAtividade.Nome);
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