using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Enums;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.UI.Models;
using Multiplix.UI.Utils;

namespace Multiplix.UI.Controllers
{
    public class CompraController : Controller
    {
        private readonly IServiceCompra _serviceCompra;
        private readonly IServiceParceiro _serviceParceiro;
        private readonly IServicePatrocinador _servicePatrocinador;
        private readonly IServiceUsuario _serviceUsuario;

        public CompraController(IServiceCompra serviceCompra, IServiceParceiro serviceParceiro, IServicePatrocinador servicePatrocinador, IServiceUsuario serviceUsuario)
        {
            _serviceCompra = serviceCompra;
            _serviceParceiro = serviceParceiro;
            _servicePatrocinador = servicePatrocinador;
            _serviceUsuario = serviceUsuario;
        }
                      
        public IActionResult IndexCompra()
        {
            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_compras_parceiro"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }

            return View();
        }
        ///Só compra do usuário logado
        public IActionResult MinhasCompras()
        {

            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_minhas_compras"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }

            CompraDTO compraDTO = new CompraDTO();
            DateTime date = DateTime.Now;

            compraDTO.DataInicio = new DateTime(date.Year, date.Month, 1);
            compraDTO.DataFim = DateTime.Now;

            ViewBag.Di = new DateTime(date.Year, date.Month, 1).ToString("dd-MM-yyyy HH:mm:ss");
            ViewBag.Df = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            return View(compraDTO);           
        }

        [HttpPost]
        public IActionResult MinhasCompras(CompraDTO compraDTO)
        {
            DateTime date = DateTime.Now;
            ViewBag.Di = compraDTO.DataInicio.ToString("dd-MM-yyyy HH:mm:ss");
            ViewBag.Df = compraDTO.DataFim.ToString("dd-MM-yyyy HH:mm:ss");

            return View(compraDTO);
        }



        public IActionResult ComprasPorAssociado()
        {
            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_compras_por_associado"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }

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

        private void TipoUsuario()
        {
            var usuarioLogado = UsuarioUtils.GetUsuarioLogado(HttpContext, _serviceUsuario);

            if (usuarioLogado.TipoUsuario == (int)ETipoUsuario.ASSOCIADO)
            {
                ViewBag.TipoUsuario = EChoicesUtil.Get(ETipoUsuarioChoices.Choices, ETipoUsuario.ASSOCIADO).ValueInt;
            }
            else
            {
                ViewBag.TipoUsuario = EChoicesUtil.Get(ETipoUsuarioChoices.Choices, ETipoUsuario.PARCEIRO).ValueInt;
            }
        }

        [HttpGet]
        public IActionResult AdicionarCompra()
        {
            ViewData["Title"] = "Nova Venda";
            CompraDTO compraDTO = new CompraDTO();

            TipoUsuario();

            return View("AdicionarEditarCompra", compraDTO);
        }

        [HttpPost]
        public IActionResult AdicionarCompra(CompraDTO compraDTO)
        {
            ViewData["Title"] = "Nova Compra";
            TipoUsuario();

            return SalvarCompra(compraDTO, "Compra adicionada com sucesso!");
        }

        private IActionResult SalvarCompra(CompraDTO compraDTO, string mensagemRetorno)
        {
            var usuarioLogado = UsuarioUtils.GetUsuarioLogado(HttpContext, _serviceUsuario);

            //Se o usuário logado for um parceiro o código do parceiro será o do parceiro logado, isso é importante pq o usuário admin é um associado e tem acesso 
            //a essa área do parceiro também
            if (usuarioLogado.TipoUsuario == (int)ETipoUsuario.PARCEIRO)
            {
                compraDTO.ParceiroId = _serviceParceiro.Buscar(x => x.Usuario.UsuarioId == usuarioLogado.UsuarioId).FirstOrDefault().ParceiroId;
            }

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
            var usuarioLogado = UsuarioUtils.GetUsuarioLogado(HttpContext, _serviceUsuario);
            ParceiroProduto infoProdutoParceiro = null;
            Parceiro parceiro = null;

            if (usuarioLogado.TipoUsuario == (int)ETipoUsuario.ASSOCIADO)
            {
                parceiro = _serviceParceiro.ObterPorId(parceiroId);
                infoProdutoParceiro = parceiro.ParceiroProdutos.Where(x => x.ProdutoId == produtoId).FirstOrDefault();
            }
            else if (usuarioLogado.TipoUsuario == (int)ETipoUsuario.PARCEIRO)
            {
                //Localiza qual parceiro é o usuário logado
                parceiro = _serviceParceiro.Buscar(x => x.Usuario.UsuarioId == usuarioLogado.UsuarioId).FirstOrDefault();
                infoProdutoParceiro = parceiro.ParceiroProdutos.Where(x => x.ProdutoId == produtoId).FirstOrDefault();
            }
            
            
            var resultado = new
            {
                valor = /*string.Format("{0:#.00}", Convert.ToDecimal(*/infoProdutoParceiro.ValorProduto/*))*/, //passar os campos minusculos para o js
                pontos_produto = infoProdutoParceiro.PontosPorRealProduto
            };
            return Json(resultado);
        }

        [HttpPost]
        public JsonResult ListaVendas(DataTableAjaxPostModel dataTableModel)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            var usuarioLogado = UsuarioUtils.GetUsuarioLogado(HttpContext, _serviceUsuario);
            Parceiro parceiroLogado = null;

            if (usuarioLogado.TipoUsuario == (int)ETipoUsuario.PARCEIRO)
            {
                 parceiroLogado = _serviceParceiro.Buscar(x => x.Usuario.UsuarioId == usuarioLogado.UsuarioId).FirstOrDefault();
            }            

            string searchTerm = dataTableModel.search.value;
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<Compra> compras = new List<Compra>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                if (usuarioLogado.TipoUsuario == (int)ETipoUsuario.ASSOCIADO)
                {
                    compras = _serviceCompra.Buscar(
                    x => x.Associado.Usuario.Nome.Contains(searchTerm) ||
                    x.Associado.CPF.Contains(searchTerm) ||
                    x.Associado.IdCarteira.Contains(searchTerm)
                );

                }
                else
                {
                    compras = _serviceCompra.Buscar(x => x.Parceiro.ParceiroId == parceiroLogado.ParceiroId)
                   .Where(x => x.Associado.Usuario.Nome.ToUpper()
                   .Contains(searchTerm.ToUpper()) && x.Data.Month == DateTime.Now.Month);
                }

            }
            else
            {
                if (usuarioLogado.TipoUsuario == (int)ETipoUsuario.ASSOCIADO)
                {
                    compras = _serviceCompra.ObterTodos();
                }
                else
                {
                    compras = _serviceCompra.Buscar(x => x.Parceiro.ParceiroId == parceiroLogado.ParceiroId).Where(x => x.Data.Month == DateTime.Now.Month);
                }
            }              

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Compra, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {                   
                    case "1":
                        orderByExpr = x => x.Associado.Usuario.Nome;
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
                    String.Format(new CultureInfo("pt-BR"),"{0:d/M/yyyy HH:mm:ss}", compra.Data),
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

            string searchTerm = dataTableModel.search.value?.ToUpper();
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

            string searchTerm = dataTableModel.search.value?.ToUpper();
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


        [HttpPost]
        public JsonResult ListaMinhasCompras(DataTableAjaxPostModel dataTableModel, string DataInicio, string DataFim)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */
            var usuarioLogado = UsuarioUtils.GetUsuarioLogado(HttpContext, _serviceUsuario);
            var associadoLogado = _servicePatrocinador.Buscar(x => x.Usuario.UsuarioId == usuarioLogado.UsuarioId).FirstOrDefault();

            string searchTerm = dataTableModel.search.value?.ToUpper();
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";    
            
            DateTime di = DateTime.Parse(DataInicio.ToString(), new CultureInfo("pt-BR"));
            DateTime df = DateTime.Parse(DataFim.ToString(), new CultureInfo("pt-BR"));

            IEnumerable<Compra> compras = new List<Compra>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                compras = _serviceCompra.Buscar(x => x.Associado.Id == associadoLogado.Id)
                    .Where(x => x.Parceiro.Usuario.Nome.ToUpper()
                    .Contains(searchTerm.ToUpper()) || x.Associado.Usuario.Nome.ToUpper()
                    .Contains(searchTerm.ToUpper()) && x.Data >= di && x.Data  <= df);
            }
            else
                compras = _serviceCompra.Buscar(x => x.Associado.Id == associadoLogado.Id).Where(x =>  x.Data >= di && x.Data <= df);

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

    }
}