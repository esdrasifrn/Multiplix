using System;
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
        private readonly IServiceParceiro _serviceParceiro;

        public CompraController(IServiceCompra serviceCompra, IServiceParceiro serviceParceiro)
        {
            _serviceCompra = serviceCompra;
            _serviceParceiro = serviceParceiro;
        }

        public IActionResult IndexCompra()
        {
            return View();
        }
        ///Só compra do usuário logado
        public IActionResult MinhasCompras()
        {
            return View();
        }

        public IActionResult ComprasPorAssociado()
        {
            CompraDTO compraDTO = new CompraDTO();
            DateTime date = DateTime.Now;

            compraDTO.DataInicio = new DateTime(date.Year, date.Month, 1);
            compraDTO.DataFim = compraDTO.DataInicio.AddMonths(1).AddDays(-1);          

            return View(compraDTO);
        }

        [HttpPost]
        public IActionResult ComprasPorAssociado(CompraDTO compraDTO)
        {
            DateTime date = DateTime.Now;

            compraDTO.DataInicio = new DateTime(date.Year, date.Month, 1);
            compraDTO.DataFim = compraDTO.DataInicio.AddMonths(1).AddDays(-1);

            return View(compraDTO);
        }


        public IActionResult ItensComprados(int compraId)
        {           
            var compra = _serviceCompra.ObterPorId(compraId);

            ViewBag.CompraId = compraId;
            ViewBag.Parceiro = compra.Parceiro.Usuario.Nome;
            ViewBag.Associado = compra.Associado.Usuario.Nome;
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

        public IActionResult GetInfoProdutoParceiro(int produtoId, int parceiroId)
        {
            var parceiro = _serviceParceiro.ObterPorId(parceiroId);
            var infoProdutoParceiro = parceiro.ParceiroProdutos.Where(x => x.ProdutoId == produtoId).FirstOrDefault();
            var resultado = new
            {
                valor = /*string.Format("{0:#.00}", Convert.ToDecimal(*/infoProdutoParceiro.ValorProduto/*))*/, //passar os campos minusculos para o js
                pontos_produto = infoProdutoParceiro.PontosPorRealProduto
            };
            return Json(resultado);
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

        [HttpPost]
        public JsonResult ListaComprasPorAssociado(DataTableAjaxPostModel dataTableModel, int AssociadoId)
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
                compras = _serviceCompra.Buscar(x => x.Associado.Id == AssociadoId)
                    .Where(x => x.Parceiro.Usuario.Nome.ToUpper()
                    .Contains(searchTerm.ToUpper()) || x.Associado.Usuario.Nome.ToUpper()
                    .Contains(searchTerm.ToUpper()));                               
            }
            else
                compras = _serviceCompra.Buscar(x => x.Associado.Id == AssociadoId);

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Compra, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Parceiro.Usuario.Nome;
                        break;
                    case "2":
                        orderByExpr = x => x.Data;
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
                    compras = compras.OrderBy(x => x.Parceiro.Usuario.Nome);
                }
            }
            else
            {
                compras = compras.OrderBy(x => x.Parceiro.Usuario.Nome);
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
                compra.Parceiro.Usuario.Nome,
                String.Format(new CultureInfo("pt-BR"),"{0:d/M/yyyy HH:mm:ss}", compra.Data),                
                String.Format(new CultureInfo("pt-BR"), "{0:C}", compra.Valor),
                String.Format(new CultureInfo("pt-BR"), "{0:0,0.0}", compra.Pontos)               
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

        [HttpPost]
        public JsonResult ListaItensCompraPorAssociado(DataTableAjaxPostModel dataTableModel, int compraId)
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
            IEnumerable<CompraItem> compraItems = new List<CompraItem>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                compras = _serviceCompra.Buscar(x => x.CompraId == compraId)
                    .Where(x => x.Parceiro.Usuario.Nome.ToUpper()
                    .Contains(searchTerm.ToUpper()) || x.Associado.Usuario.Nome.ToUpper()
                    .Contains(searchTerm.ToUpper()));
            }
            else
                compraItems = _serviceCompra.ObterPorId(compraId).CompraItems;

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<CompraItem, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Produto.Descricao;                      
                        break;
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        compraItems = compraItems.OrderByDescending(orderByExpr);
                    else
                        compraItems = compraItems.OrderBy(orderByExpr);
                }
                else
                {
                    compraItems = compraItems.OrderBy(x => x.Produto.Descricao);
                }
            }
            else
            {
                compraItems = compraItems.OrderBy(x => x.Produto.Descricao);
            }

            // pagina a lista
            int totalResultados = compraItems.Count();
            compraItems = compraItems.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var compraItem in compraItems)
            {
                Decimal pontosPorReal = (Decimal)compraItem.Compra.Parceiro.ParceiroProdutos.Where(x => x.ProdutoId == compraItem.Produto.ProdutoId).FirstOrDefault().PontosPorRealProduto;

                List<object> result_item = new List<object> {
                compraItem.CompraItemId,
                compraItem.Produto.Descricao,
                compraItem.Qtd,
                String.Format(new CultureInfo("pt-BR"), "{0:C}", compraItem.ValorUnidade),
                String.Format(new CultureInfo("pt-BR"), "{0:C}", compraItem.Subtotal),
                pontosPorReal.ToString("0.0"),
                String.Format(new CultureInfo("pt-BR"), "{0:0,0.0}", compraItem.SubtotalPontos)
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