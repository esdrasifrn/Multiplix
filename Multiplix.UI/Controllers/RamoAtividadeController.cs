using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.UI.Models;
using Multiplix.UI.Utils;

namespace Multiplix.UI.Controllers
{
    public class RamoAtividadeController : Controller
    {
        private IServiceRamoAtividade _serviceRamoAtividade;

        public RamoAtividadeController(IServiceRamoAtividade  serviceRamoAtividade)
        {
            _serviceRamoAtividade = serviceRamoAtividade;
        }
        
        public IActionResult IndexRamoAtividade()
        {           
            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_ramo_atividade"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }

            return View();
        }

        [HttpGet]
        public IActionResult AdicionarRamoAtividade()
        {
            ViewData["Title"] = "Novo Ramo de atividade";
            RamoAtividadeDTO  ramoAtividadeDTO = new RamoAtividadeDTO();
            return View("AdicionarEditarRamoAtividade", ramoAtividadeDTO);
        }

        [HttpPost]
        public IActionResult AdicionarRamoAtividade(RamoAtividadeDTO ramoAtividadeDTO)
        {
            ViewData["Title"] = "Novo Ramo de atividade";
            return SalvarRamoAtividade(ramoAtividadeDTO, "Ramo de atividade adicionado com sucesso!");
        }

        [HttpGet]
        public IActionResult EditarRamoAtividade(int ramoAtividadeId)
        {
            ViewData["Title"] = "Editar Ramo de atividade";
            RamoAtividade ramoAtividade = _serviceRamoAtividade.ObterPorId(ramoAtividadeId);
            RamoAtividadeDTO ramoAtividadeDTO = new RamoAtividadeDTO()
            {
                RamoAtividadeNome = ramoAtividade.Nome
            };

            return View("AdicionarEditarRamoAtividade", ramoAtividadeDTO);
        }

        [HttpPost]
        public IActionResult EditarRamoAtividade(RamoAtividadeDTO ramoAtividadeDTO)
        {
            ViewData["Title"] = "Editar Ramo de atividade";
            return SalvarRamoAtividade(ramoAtividadeDTO, "Produto alterado com sucesso!");
        }

        private IActionResult SalvarRamoAtividade(RamoAtividadeDTO ramoAtividadeDTO, string mensagemRetorno)
        {
            var result = _serviceRamoAtividade.Salvar(ramoAtividadeDTO);

            if (result.IsValid)
            {
                TempData["success"] = mensagemRetorno;
                return RedirectToAction("IndexRamoAtividade");
            }
            return View("AdicionarEditarRamoAtividade", ramoAtividadeDTO);
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

        [HttpPost]
        public JsonResult ListaRamoAtividade(DataTableAjaxPostModel dataTableModel)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            string searchTerm = dataTableModel.search.value?.ToUpper();
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<RamoAtividade> ramosAtividades = new List<RamoAtividade>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                ramosAtividades = _serviceRamoAtividade.Buscar(
                    x => x.Nome.Contains(searchTerm)
                );
            }
            else
                ramosAtividades = _serviceRamoAtividade.ObterTodos();

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<RamoAtividade, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Nome;
                        break;
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        ramosAtividades = ramosAtividades.OrderByDescending(orderByExpr);
                    else
                        ramosAtividades = ramosAtividades.OrderBy(orderByExpr);
                }
                else
                {
                    ramosAtividades = ramosAtividades.OrderBy(x => x.Nome);
                }
            }
            else
            {
                ramosAtividades = ramosAtividades.OrderBy(x => x.Nome);
            }

            // pagina a lista
            int totalResultados = ramosAtividades.Count();
            ramosAtividades = ramosAtividades.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var ramoAtividade in ramosAtividades)
            {
                List<object> result_item = new List<object> {
                    ramoAtividade.RamoAtividadeId,
                    ramoAtividade.Nome
                };
                result_data.Add(result_item);
            }

            return Json(new
            {
                recordsTotal = totalResultados,
                recordsFiltered = totalResultados,
                data = result_data
            });
        }
    }
}