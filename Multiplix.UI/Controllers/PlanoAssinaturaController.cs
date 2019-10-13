using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.UI.Models;

namespace Multiplix.UI.Controllers
{
    public class PlanoAssinaturaController : Controller
    {
        private IServicePlanoAssinatura _servicePlanoAssinatura;

        public PlanoAssinaturaController(IServicePlanoAssinatura servicePlanoAssinatura)
        {
            _servicePlanoAssinatura = servicePlanoAssinatura;
        }

        public IActionResult IndexPlanoAssinatura()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdicionarPlanoAssinatura()
        {
            ViewData["Title"] = "Novo Plano de Assinatura";
            PlanoAssinaturaDTO planoAssinaturaDTO = new PlanoAssinaturaDTO();
            return View("AdicionarEditarPlanoAssinatura", planoAssinaturaDTO);
        }

        [HttpPost]
        public IActionResult AdicionarPlanoAssinatura(PlanoAssinaturaDTO planoAssinaturaDTO)
        {
            ViewData["Title"] = "Novo Plano de Assinatura";
            return SalvarPlanoAssinatura(planoAssinaturaDTO, "Plano de assinatura adicionado com sucesso!");
        }

        [HttpGet]
        public IActionResult EditarPlanoAssinatura(int planoAssinaturaId)
        {
            ViewData["Title"] = "Editar Plano de Assinatura";
            PlanoAssinatura planoAssinatura = _servicePlanoAssinatura.ObterPorId(planoAssinaturaId);
            PlanoAssinaturaDTO planoAssinaturaDTO = new PlanoAssinaturaDTO()
            {
                Descricao = planoAssinatura.Descricao,
                Valor = planoAssinatura.Valor
            };

            return View("AdicionarEditarPlanoAssinatura", planoAssinaturaDTO);
        }

        [HttpPost]
        public IActionResult EditarPlanoAssinatura(PlanoAssinaturaDTO planoAssinaturaDTO)
        {
            ViewData["Title"] = "Editar Plano de Assinatura";
            return SalvarPlanoAssinatura(planoAssinaturaDTO, "Plano de Assinatura alterado com sucesso!");
        }

        private IActionResult SalvarPlanoAssinatura(PlanoAssinaturaDTO planoAssinaturaDTO, string mensagemRetorno)
        {
            var result = _servicePlanoAssinatura.Salvar(planoAssinaturaDTO);

            if (result.IsValid)
            {
                TempData["success"] = mensagemRetorno;
                return RedirectToAction("IndexPlanoAssinatura");
            }
            return View("AdicionarEditarPlanoAssinatura", planoAssinaturaDTO);
        }


        public JsonResult PesquisaPlanoAssinatura(string searchTerm, int pageNumber)
        {
            /*
             * consumido por um Select2 ajax
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com Select2
             */

            int pageSize = 10;

            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            IEnumerable<PlanoAssinatura> planoAssinaturas = new List<PlanoAssinatura>();

            if (!String.IsNullOrEmpty(searchTerm))
                planoAssinaturas = _servicePlanoAssinatura.Buscar(x => x.Descricao.Contains(searchTerm));
            else
                planoAssinaturas = _servicePlanoAssinatura.ObterTodos();

            int totalResults = planoAssinaturas.Count();
            planoAssinaturas = planoAssinaturas.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var planoAssinatura in planoAssinaturas)
            {
                Dictionary<string, string> result_item = new Dictionary<string, string>();
                result_item.Add("id", planoAssinatura.PlanoAssinaturaId + "");
                result_item.Add("text", planoAssinatura.Descricao);
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
        public JsonResult ListaPlanosAssinatura(DataTableAjaxPostModel dataTableModel)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            string searchTerm = dataTableModel.search.value;
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<PlanoAssinatura> planoAssinaturas = new List<PlanoAssinatura>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                planoAssinaturas = _servicePlanoAssinatura.Buscar(
                    x => x.Descricao.Contains(searchTerm)
                );
            }
            else
                planoAssinaturas = _servicePlanoAssinatura.ObterTodos();

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<PlanoAssinatura, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Descricao;
                        break;
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        planoAssinaturas = planoAssinaturas.OrderByDescending(orderByExpr);
                    else
                        planoAssinaturas = planoAssinaturas.OrderBy(orderByExpr);
                }
                else
                {
                    planoAssinaturas = planoAssinaturas.OrderBy(x => x.Descricao);
                }
            }
            else
            {
                planoAssinaturas = planoAssinaturas.OrderBy(x => x.Descricao);
            }

            // pagina a lista
            int totalResultados = planoAssinaturas.Count();
            planoAssinaturas = planoAssinaturas.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var banco in planoAssinaturas)
            {
                List<object> result_item = new List<object> {
                    banco.PlanoAssinaturaId,
                    banco.Descricao,
                    banco.Valor
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