using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;

namespace Multiplix.UI.Controllers
{
    public class BancoController : Controller
    {
        private IServiceBanco _serviceBanco;

        public BancoController(IServiceBanco serviceBanco)
        {
            _serviceBanco = serviceBanco;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult PesquisaBanco(string searchTerm, int pageNumber)
        {
            /*
             * consumido por um Select2 ajax
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com Select2
             */

            int pageSize = 10;

            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            IEnumerable<Banco> bancos = new List<Banco>();

            if (!String.IsNullOrEmpty(searchTerm))
                bancos = _serviceBanco.Buscar(x => x.Nome.Contains(searchTerm));
            else
                bancos = _serviceBanco.ObterTodos();

            int totalResults = bancos.Count();
            bancos = bancos.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var banco in bancos)
            {
                Dictionary<string, string> result_item = new Dictionary<string, string>();
                result_item.Add("id", banco.BancoId + "");
                result_item.Add("text", banco.Nome);
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