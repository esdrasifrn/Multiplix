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
using Multiplix.UI.Utils;

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
      
        public IActionResult IndexProduto()
        {
            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_produto"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }

            return View();
        }

        [HttpGet]
        public IActionResult AdicionarProduto()
        {
            ViewData["Title"] = "Novo Produto";
            ProdutoDTO produtoDTO = new ProdutoDTO();
            return View("AdicionarEditarProduto", produtoDTO);
        }

        [HttpPost]
        public IActionResult AdicionarProduto(ProdutoDTO produtoDTO)
        {
            ViewData["Title"] = "Novo Produto";
            return SalvarProduto(produtoDTO, "Produdo adicionado com sucesso!");
        }

        [HttpGet]
        public IActionResult EditarProduto(int produtoId)
        {
            ViewData["Title"] = "Editar Produto";
            Produto produto = _serviceProduto.ObterPorId(produtoId);
            ProdutoDTO produtoDTO = new ProdutoDTO()
            {
                Descricao = produto.Descricao               
            };

            return View("AdicionarEditarProduto", produtoDTO);
        }

        [HttpPost]
        public IActionResult EditarProduto(ProdutoDTO produtoDTO)
        {
            ViewData["Title"] = "Editar produto";
            return SalvarProduto(produtoDTO, "Produto alterado com sucesso!");
        }

        private IActionResult SalvarProduto(ProdutoDTO  produtoDTO, string mensagemRetorno)
        {
            var result = _serviceProduto.Salvar(produtoDTO);

            if (result.IsValid)
            {
                TempData["success"] = mensagemRetorno;
                return RedirectToAction("IndexProduto");
            }
            return View("AdicionarEditarProduto", produtoDTO);
        }

        public JsonResult PesquisaProduto(string searchTerm, int pageNumber, int ParceiroId)
        {
            /*
             * consumido por um Select2 ajax
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com Select2
             */

            int pageSize = 10;

            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            Expression<Func<Produto, bool>> searchFor;

            IEnumerable<Produto> produtos = new List<Produto>();
            if (ParceiroId == 0)
            {
                if (!String.IsNullOrEmpty(searchTerm))
                    produtos = _serviceProduto.Buscar(x => x.Descricao.Contains(searchTerm));
                else
                    produtos = _serviceProduto.ObterTodos();
            }
            else
            {
                if (!String.IsNullOrEmpty(searchTerm))
                {
                    searchFor = x => x.Descricao.Contains(searchTerm) && (x.ParceiroProdutos.Any(y => y.ParceiroId == ParceiroId));
                    produtos = _serviceProduto.Buscar(searchFor);
                }
                else
                {
                    searchFor = x => (x.ParceiroProdutos.Any(y => y.ParceiroId == ParceiroId));
                    produtos = _serviceProduto.Buscar(searchFor);
                }
                   
            }
           

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


        [HttpPost]
        public JsonResult ListaProdutos(DataTableAjaxPostModel dataTableModel)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            string searchTerm = dataTableModel.search.value;
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<Produto> produtos = new List<Produto>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                produtos = _serviceProduto.Buscar(
                    x => x.Descricao.Contains(searchTerm) 
                );
            }
            else
                produtos = _serviceProduto.ObterTodos();

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Produto, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Descricao;
                        break;                    
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        produtos = produtos.OrderByDescending(orderByExpr);
                    else
                        produtos = produtos.OrderBy(orderByExpr);
                }
                else
                {
                    produtos = produtos.OrderBy(x => x.Descricao);
                }
            }
            else
            {
                produtos = produtos.OrderBy(x => x.Descricao);
            }

            // pagina a lista
            int totalResultados = produtos.Count();
            produtos = produtos.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var produto in produtos)
            {
                List<object> result_item = new List<object> {
                    produto.ProdutoId,
                    produto.Descricao                  
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