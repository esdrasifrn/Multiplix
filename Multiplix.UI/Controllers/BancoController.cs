using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.UI.Models;

namespace Multiplix.UI.Controllers
{
    public class BancoController : Controller
    {
        private IServiceBanco _serviceBanco;

        public BancoController(IServiceBanco serviceBanco)
        {
            _serviceBanco = serviceBanco;
        }

        public IActionResult IndexBanco()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdicionarBanco()
        {
            ViewData["Title"] = "Novo Banco";
            BancoDTO bancoDTO = new BancoDTO();
            return View("AdicionarEditarBanco", bancoDTO);
        }

        [HttpPost]
        public IActionResult AdicionarBanco(BancoDTO bancoDTO)
        {
            ViewData["Title"] = "Novo Banco";
            return SalvarBanco(bancoDTO, "Banco adicionado com sucesso!");
        }

        [HttpGet]
        public IActionResult EditarBanco(int bancoId)
        {
            ViewData["Title"] = "Editar Banco";
            Banco banco = _serviceBanco.ObterPorId(bancoId);
            BancoDTO bancoDTO = new BancoDTO()
            {
                Nome = banco.Nome,
                Codigo = banco.Codigo
            };

            return View("AdicionarEditarBanco", bancoDTO);
        }

        [HttpPost]
        public IActionResult EditarBanco(BancoDTO bancoDTO)
        {
            ViewData["Title"] = "Editar banco";
            return SalvarBanco(bancoDTO, "Banco alterado com sucesso!");
        }

        private IActionResult SalvarBanco(BancoDTO bancoDTO, string mensagemRetorno)
        {
            var result = _serviceBanco.Salvar(bancoDTO);

            if (result.IsValid)
            {
                TempData["success"] = mensagemRetorno;
                return RedirectToAction("IndexBanco");
            }
            return View("AdicionarEditarBanco", bancoDTO);
        }


        [AllowAnonymous]
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

        [HttpPost]
        public JsonResult ListaBancos(DataTableAjaxPostModel dataTableModel)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            string searchTerm = dataTableModel.search.value;
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<Banco> bancos = new List<Banco>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                bancos = _serviceBanco.Buscar(
                    x => x.Nome.Contains(searchTerm)
                );
            }
            else
                bancos = _serviceBanco.ObterTodos();

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Banco, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Nome;
                        break;
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        bancos = bancos.OrderByDescending(orderByExpr);
                    else
                        bancos = bancos.OrderBy(orderByExpr);
                }
                else
                {
                    bancos = bancos.OrderBy(x => x.Nome);
                }
            }
            else
            {
                bancos = bancos.OrderBy(x => x.Nome);
            }

            // pagina a lista
            int totalResultados = bancos.Count();
            bancos = bancos.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var banco in bancos)
            {
                List<object> result_item = new List<object> {
                    banco.BancoId,
                    banco.Nome,
                    banco.Codigo
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