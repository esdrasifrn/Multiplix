using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;

namespace Multiplix.UI.Controllers
{
    public class ProdutoController : Controller
    {
        private IServiceProduto _serviceProduto;

        public ProdutoController(IServiceProduto  serviceProduto)
        {
            _serviceProduto = serviceProduto;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult PesquisaProduto(string searchTerm, int pageNumber)
        {
            /*
             * consumido por um Select2 ajax
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com Select2
             */

            int pageSize = 10;

            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            IEnumerable<Produto> produtos = new List<Produto>();

            if (!String.IsNullOrEmpty(searchTerm))
                produtos = _serviceProduto.Buscar(x => x.Descricao.Contains(searchTerm));
            else
                produtos = _serviceProduto.ObterTodos();

            int totalResults = produtos.Count();
            produtos = produtos.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var ramoAtividade in produtos)
            {
                Dictionary<string, string> result_item = new Dictionary<string, string>();
                result_item.Add("id", ramoAtividade.ProdutoId + "");
                result_item.Add("text", ramoAtividade.Descricao);
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