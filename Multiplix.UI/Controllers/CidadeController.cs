using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.UI.Models;

namespace Multiplix.UI.Controllers
{
    public class CidadeController : Controller
    {
        private IServiceCidade _serviceCidade;

        public CidadeController(IServiceCidade serviceCidade)
        {
            _serviceCidade = serviceCidade;
        }

        public IActionResult IndexCidade()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdicionarCidade()
        {
            ViewData["Title"] = "Nova Cidade";
            CidadeDTO cidadeDTO = new CidadeDTO();
            return View("AdicionarEditarCidade", cidadeDTO);
        }

        [HttpPost]
        public IActionResult AdicionarCidade(CidadeDTO cidadeDTO)
        {
            ViewData["Title"] = "Nova Cidade";
            return SalvarCidade(cidadeDTO, "Cidade adicionada com sucesso!");
        }

        [HttpGet]
        public IActionResult EditarCidade(int cidadeId)
        {
            ViewData["Title"] = "Editar Cidade";
            Cidade cidade = _serviceCidade.ObterPorId(cidadeId);

            CidadeDTO cidadeDTO = new CidadeDTO()
            {
                Descricao = cidade.Descricao,
                EstadoId = cidade.Estado.EstadoId,
                EstadoNome = cidade.Estado.Nome
            };

            return View("AdicionarEditarCidade", cidadeDTO);
        }

        [HttpPost]
        public IActionResult EditarCidade(CidadeDTO cidadeDTO)
        {
            ViewData["Title"] = "Editar cidade";
            return SalvarCidade(cidadeDTO, "Cidade alterada com sucesso!");
        }


        private IActionResult SalvarCidade(CidadeDTO cidadeDTO, string mensagemRetorno)
        {
            var result = _serviceCidade.Salvar(cidadeDTO);

            if (result.IsValid)
            {
                TempData["success"] = mensagemRetorno;
                return RedirectToAction("IndexCidade");
            }
            return View("AdicionarEditarCidade", cidadeDTO);
        }

        [HttpPost]
        public JsonResult ListaCidades(DataTableAjaxPostModel dataTableModel)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            string searchTerm = dataTableModel.search.value?.ToUpper();
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<Cidade> cidades = new List<Cidade>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                cidades = _serviceCidade.Buscar(
                    x => x.Descricao.Contains(searchTerm)
                );
            }
            else
                cidades = _serviceCidade.ObterTodos();

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Cidade, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Descricao;
                        break;
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        cidades = cidades.OrderByDescending(orderByExpr);
                    else
                        cidades = cidades.OrderBy(orderByExpr);
                }
                else
                {
                    cidades = cidades.OrderBy(x => x.Descricao);
                }
            }
            else
            {
                cidades = cidades.OrderBy(x => x.Descricao);
            }

            // pagina a lista
            int totalResultados = cidades.Count();
            cidades = cidades.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var cidade in cidades)
            {
                List<object> result_item = new List<object> {
                    cidade.CidadeId,
                    cidade.Descricao,
                    cidade.Estado.Nome
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

        public JsonResult PesquisaCidade(string searchTerm, int pageNumber, int EstadoId)
        {
            /*
             * consumido por um Select2 ajax
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com Select2
             */

            int pageSize = 10;

            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            Expression<Func<Cidade, bool>> searchFor;

            IEnumerable<Cidade> cidades = new List<Cidade>();

            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchFor = x => x.Descricao.Contains(searchTerm) && (x.Estado.Cidades.Any(y => y.Estado.EstadoId == EstadoId));
                cidades = _serviceCidade.Buscar(searchFor);
            }
            else
            {
                searchFor = x => x.Estado.Cidades.Any(y => y.Estado.EstadoId == EstadoId);
                cidades = _serviceCidade.Buscar(searchFor);
            }
               

            int totalResults = cidades.Count();
            cidades = cidades.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var cidade in cidades)
            {
                Dictionary<string, string> result_item = new Dictionary<string, string>();
                result_item.Add("id", cidade.CidadeId + "");
                result_item.Add("text", cidade.Descricao);
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