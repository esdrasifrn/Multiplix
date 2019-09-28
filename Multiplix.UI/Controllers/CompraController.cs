﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.UI.Models;

namespace Multiplix.UI.Controllers
{
    public class CompraController : Controller
    {
        private readonly IServiceCompra _serviceCompra;

        public CompraController(IServiceCompra serviceCompra)
        {
            _serviceCompra = serviceCompra;
        }

        public IActionResult IndexCompra()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdicionarCompra()
        {
            ViewData["Title"] = "Nova Compra";
            CompraDTO compraDTO = new CompraDTO();
            return View("AdicionarEditarCompra", compraDTO);
        }

        [HttpPost]
        public IActionResult AdicionarCompra(CompraDTO compraDTO)
        {
            ViewData["Title"] = "Nova Compra";
            return SalvarCompra(compraDTO, "Compra adicionada com sucesso!");
        }

        private IActionResult SalvarCompra(CompraDTO compraDTO, string mensagemRetorno)
        {
            var result = _serviceCompra.Salvar(compraDTO);

            if (result.IsValid)
            {
                TempData["success"] = mensagemRetorno;
                return RedirectToAction("IndexCompra");
            }
            return View("AdicionarEditarCompra", compraDTO);
        }

        [HttpPost]
        public JsonResult ListaCompras(DataTableAjaxPostModel dataTableModel)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            string searchTerm = dataTableModel.search.value;
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<Compra> compras = new List<Compra>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                compras = _serviceCompra.Buscar(
                    x => x.Parceiro.Usuario.Nome.Contains(searchTerm) ||
                         x.Associado.Usuario.Nome.Contains(searchTerm) 
                );
            }
            else
                compras = _serviceCompra.ObterTodos();

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Compra, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Parceiro.Usuario.Nome;
                        break;
                    case "2":
                        orderByExpr = x => x.Associado.Usuario.Nome;
                        break;
                    case "3":
                        orderByExpr = x => x.Valor;
                        break;
                    case "4":
                        orderByExpr = x => x.Pontos;
                        break;                 
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        compras = compras.OrderByDescending(orderByExpr);
                    else
                        compras = compras.OrderBy(orderByExpr);
                }
                else
                {
                    compras = compras.OrderBy(x => x.Associado.Usuario.Nome);
                } 
            }
            else
            {
                compras = compras.OrderBy(x => x.Associado.Usuario.Nome);
            }

            // pagina a lista
            int totalResultados = compras.Count();
            compras = compras.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var compra in compras)
            {
                List<object> result_item = new List<object> {
                    compra.CompraId,
                    compra.Associado.Usuario.Nome,
                    String.Format(new CultureInfo("pt-BR"), "{0:C}", compra.Valor),
                    compra.Pontos               
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