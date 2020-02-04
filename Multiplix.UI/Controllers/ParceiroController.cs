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
using Multiplix.UI.Utils;

namespace Multiplix.UI.Controllers
{
    public class ParceiroController : Controller
    {
        private IServiceParceiro _serviceParceiro { get; set; }
        private IServiceUsuario _serviceUsuario;
        private IServicePatrocinador _servicePatrocinador;

        public ParceiroController(IServiceParceiro serviceParceiro, IServiceUsuario serviceUsuario, IServicePatrocinador servicePatrocinador)
        {
            _serviceParceiro = serviceParceiro;
            _serviceUsuario = serviceUsuario;
            _servicePatrocinador = servicePatrocinador;
        }

        public IActionResult ProdutosParceiro()
        {

            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_produtos_por_parceiro"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }
            
            var usuarioLogado = UsuarioUtils.GetUsuarioLogado(HttpContext, _serviceUsuario);
            var associadoLogado = _servicePatrocinador.Buscar(x => x.Usuario.UsuarioId == usuarioLogado.UsuarioId).FirstOrDefault();

            if (!usuarioLogado.Liberado == true)
            {
                return RedirectToAction("Ativacao", "Permissao");
            }

            if ((!_servicePatrocinador.DadosAtualizados(associadoLogado, atualizando: true) == true) && (associadoLogado.PatrocinadorId != null))
            {
                return RedirectToAction("AtualizarDados", "Permissao");
            }

            UsuarioDTO usuarioDTO = new UsuarioDTO();
            usuarioDTO.EstadoId = associadoLogado.Cidade.Estado.EstadoId;
            usuarioDTO.EstadoNome = associadoLogado.Cidade.Estado.Nome;
            usuarioDTO.CidadeId = associadoLogado.Cidade.CidadeId;
            usuarioDTO.CidadeNome = associadoLogado.Cidade.Descricao;

            ViewBag.CidadeId = associadoLogado.Cidade.CidadeId;

            return View(usuarioDTO);
        }

        [HttpPost]
        public IActionResult ProdutosParceiro(UsuarioDTO usuarioDTO)
        {

            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_produtos_por_parceiro"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }

            ViewBag.CidadeId = usuarioDTO.CidadeId;

            return View(usuarioDTO);
        }

        public IActionResult IndexParceiro()
        {
            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_parceiro"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }

            return View();
        }

        [HttpGet]
        public IActionResult AdicionarParceiro()
        {
            ViewData["Title"] = "Novo Parceiro";
            UsuarioDTO usuarioDTO = new UsuarioDTO();
            return View("AdicionarEditarParceiro", usuarioDTO);
        }

        [HttpPost]
        public IActionResult AdicionarParceiro(UsuarioDTO usuarioDTO)
        {
            ViewData["Title"] = "Novo Parceiro";
            return SalvarParceiro(usuarioDTO, "Parceiro adicionado com sucesso!");
        }

        [HttpGet]
        public IActionResult EditarParceiro(int usuarioId)
        {
            ViewData["Title"] = "Editar Parceiro";
            Usuario usuario = _serviceUsuario.ObterPorId(usuarioId);

            Parceiro parceiro = _serviceParceiro.Buscar(x => x.Usuario.UsuarioId == usuario.UsuarioId).FirstOrDefault();

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Nome = parceiro.Usuario.Nome,               
                CNPJ = parceiro.CNPJ,               
                Rua = parceiro.Rua,
                Numero = parceiro.Numero,
                CEP = parceiro.CEP,
                CidadeId = parceiro.Cidade.CidadeId,
                CidadeNome = parceiro.Cidade.Descricao,
                Bairro = parceiro.Bairro,
                Complemento = parceiro.Complemento,                
                Email = parceiro.Usuario.Email,               
                Celular = parceiro.Usuario.Celular,              
                Login = parceiro.Usuario.Login,
                HorarioFuncionamento = parceiro.HorarioFuncionamento,               
                RamoAtividadeId = parceiro.Ramo != null ? parceiro.Ramo.RamoAtividadeId : 0,
                RamoAtividadeNome = parceiro.Ramo != null ? parceiro.Ramo.Nome : "",
                EstadoId = parceiro.Cidade.Estado.EstadoId,
                EstadoNome = parceiro.Cidade.Estado.Nome,
                Responsavel = parceiro.Responsavel
            };

            foreach (var parceiroProduto in parceiro.ParceiroProdutos)
            {
                usuarioDTO.Produtos.Add(new ProdutoDTO() {
                    ProdutoId = parceiroProduto.Produto.ProdutoId,
                    Descricao = parceiroProduto.Produto.Descricao,
                    PontosPorRealProduto = parceiroProduto.PontosPorRealProduto,
                    ValorProduto = parceiroProduto.ValorProduto,
                    PercentualRepasse = parceiroProduto.PercentualRepasseAtual
                });
            }

            return View("AdicionarEditarParceiro", usuarioDTO);
        }

        [HttpPost]
        public IActionResult EditarParceiro(UsuarioDTO usuarioDTO)
        {
            ViewData["Title"] = "Editar parceiro";
            return SalvarParceiro(usuarioDTO, "Parceiro alterado com sucesso!");
        }

        private IActionResult SalvarParceiro(UsuarioDTO usuarioDTO, string mensagemRetorno)
        {
            var result = _serviceParceiro.SalvarParceiro(usuarioDTO);

            if (result.IsValid)
            {
                TempData["success"] = mensagemRetorno;
                return RedirectToAction("IndexParceiro");
            }
            return View("AdicionarEditarParceiro", usuarioDTO);
        }

        public IActionResult RepassePorParceiro()
        {
            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_repasse_por_parceiro"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }

            ListaRepasseParceiroDTO listaRepasseParceiroDTO = new ListaRepasseParceiroDTO();
            DateTime date = DateTime.Now;

            listaRepasseParceiroDTO.DataInicio = new DateTime(date.Year, date.Month, 1);
            listaRepasseParceiroDTO.DataFim = listaRepasseParceiroDTO.DataInicio.AddMonths(1).AddDays(-1);

            ViewBag.Di = new DateTime(date.Year, date.Month, 1).ToString("dd-MM-yyyy HH:mm:ss");
            ViewBag.Df = listaRepasseParceiroDTO.DataFim.ToString("dd-MM-yyyy HH:mm:ss");

            return View(listaRepasseParceiroDTO);
        }

        [HttpPost]
        public IActionResult RepassePorParceiro(ListaRepasseParceiroDTO listaRepasseParceiroDTO)
        {
            DateTime date = DateTime.Now;
            ViewBag.Di = listaRepasseParceiroDTO.DataInicio.ToString("dd-MM-yyyy HH:mm:ss");
            ViewBag.Df = listaRepasseParceiroDTO.DataFim.ToString("dd-MM-yyyy HH:mm:ss");

            return View(listaRepasseParceiroDTO);
        }

        [HttpPost]
        public JsonResult ListaParceiros(DataTableAjaxPostModel dataTableModel)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            string searchTerm = dataTableModel.search.value;
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<Parceiro> parceiros = new List<Parceiro>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                parceiros = _serviceParceiro.Buscar(
                    x => x.CNPJ.Contains(searchTerm) ||
                         x.Usuario.Nome.Contains(searchTerm) ||
                         x.Usuario.Celular.Contains(searchTerm) ||
                         x.Usuario.Email.Contains(searchTerm) ||
                         x.Usuario.Login.Contains(searchTerm)
                );
            }
            else
                parceiros = _serviceParceiro.ObterTodos();

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Parceiro, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {                   
                    case "1":
                        orderByExpr = x => x.Usuario.Nome;
                        break;
                    case "2":
                        orderByExpr = x => x.Usuario.Celular;
                        break;
                    case "3":
                        orderByExpr = x => x.Usuario.Email;
                        break;
                    case "4":
                        orderByExpr = x => x.Usuario.Login;
                        break;
                    case "5":
                        orderByExpr = x => x.CNPJ;
                        break;                   
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        parceiros = parceiros.OrderByDescending(orderByExpr);
                    else
                        parceiros = parceiros.OrderBy(orderByExpr);
                }
                else
                {
                    parceiros = parceiros.OrderBy(x => x.Usuario.Nome);
                }
            }
            else
            {
                parceiros = parceiros.OrderBy(x => x.Usuario.Nome);
            }

            // pagina a lista
            int totalResultados = parceiros.Count();
            parceiros = parceiros.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var parceiro in parceiros)
            {
                List<object> result_item = new List<object> {
                    parceiro.Usuario.UsuarioId,                   
                    parceiro.Usuario.Nome,
                    parceiro.CNPJ,
                    parceiro.Usuario.Celular,
                    parceiro.Usuario.Email,
                    parceiro.ParceiroProdutos.Count,

                    _serviceParceiro.ObterPorId(id: parceiro.ParceiroId).Usuario.Nome ?? "-"
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


        public JsonResult PesquisaParceiro(string searchTerm, int pageNumber)
        {
            /*
             * consumido por um Select2 ajax
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com Select2
             */

            int pageSize = 10;

            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            IEnumerable<Parceiro> parceiros = new List<Parceiro>();

            if (!String.IsNullOrEmpty(searchTerm))
                parceiros = _serviceParceiro.Buscar(x => x.Usuario.Nome.Contains(searchTerm));
            else
                parceiros = _serviceParceiro.ObterTodos();

            int totalResults = parceiros.Count();
            parceiros = parceiros.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            foreach (var parceiro in parceiros)
            {
                Dictionary<string, string> result_item = new Dictionary<string, string>();
                result_item.Add("id", parceiro.ParceiroId + "");
                result_item.Add("text", parceiro.Usuario.Nome);
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
        public JsonResult ListaProdutosPorParceiro(DataTableAjaxPostModel dataTableModel, int cidadeId)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            string searchTerm = dataTableModel.search.value?.ToUpper();
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<ListaProdutoParceiroDTO> parceiros = new List<ListaProdutoParceiroDTO>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                //produtos dos parceiros da cidade do associado logado e que atenda a um termo pesquisado
                parceiros = _serviceParceiro.ListaProdutosParceiroDTO(searchTerm, cidadeId);

            }
            else//produtos dos parceiros da cidade do associado logado
                parceiros = _serviceParceiro.ListaProdutosParceiroDTO(cidadeId);

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<ListaProdutoParceiroDTO, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Parceiro;
                        break;
                    case "2":
                        orderByExpr = x => x.Telefone;
                        break;
                    case "3":
                        orderByExpr = x => x.Endereco;
                        break;
                    case "4":
                        orderByExpr = x => x.Produto;
                        break;
                    case "5":
                        orderByExpr = x => x.Preco;
                        break;
                    case "6":
                        orderByExpr = x => x.PontosPorReal;
                        break;
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        parceiros = parceiros.OrderByDescending(orderByExpr);
                    else
                        parceiros = parceiros.OrderBy(orderByExpr);
                }
                else
                {
                    parceiros = parceiros.OrderBy(x => x.Parceiro);
                }
            }
            else
            {
                parceiros = parceiros.OrderBy(x => x.Parceiro);
            }

            // pagina a lista
            int totalResultados = parceiros.Count();
            parceiros = parceiros.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
           
            foreach (var parceiro in parceiros)
            {
                List<object> result_item = new List<object> {
                parceiro.ParceiroId,
                parceiro.Parceiro,
                parceiro.Telefone,
                parceiro.Endereco,
                parceiro.Produto,
                String.Format(new CultureInfo("pt-BR"), "{0:C}", parceiro.Preco),
                parceiro.PontosPorReal

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
        public JsonResult ListaRepassePorParceiro(DataTableAjaxPostModel dataTableModel, int ParceiroId, string DataInicio, string DataFim)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            string searchTerm = dataTableModel.search.value?.ToUpper();
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            DateTime di = DateTime.Parse(DataInicio.ToString(), new CultureInfo("pt-BR"));
            DateTime df = DateTime.Parse(DataFim.ToString(), new CultureInfo("pt-BR"));

            IEnumerable<ListaRepasseParceiroDTO> parceiros = new List<ListaRepasseParceiroDTO>();

            if /*(!String.IsNullOrEmpty(dataTableModel.search.value))*/ (ParceiroId != 0)
            {
                //produtos dos parceiros da cidade do associado logado e que atenda a um termo pesquisado
                parceiros = _serviceParceiro.ListaRepasseParceiroDTO(di, df, ParceiroId);

            }
            else//produtos dos parceiros da cidade do associado logado
                parceiros = _serviceParceiro.ListaRepasseParceiroDTO(di, df);

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<ListaRepasseParceiroDTO, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Parceiro;
                        break;
                    case "2":
                        orderByExpr = x => x.ValorRepasse;
                        break;
                    case "3":
                        orderByExpr = x => x.NumeroVendas;
                        break;                   
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        parceiros = parceiros.OrderByDescending(orderByExpr);
                    else
                        parceiros = parceiros.OrderBy(orderByExpr);
                }
                else
                {
                    parceiros = parceiros.OrderBy(x => x.Parceiro);
                }
            }
            else
            {
                parceiros = parceiros.OrderBy(x => x.Parceiro);
            }

            // pagina a lista
            int totalResultados = parceiros.Count();
            parceiros = parceiros.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();

            foreach (var parceiro in parceiros)
            {
                List<object> result_item = new List<object> {
                parceiro.ParceiroId,
                parceiro.Parceiro,
                 String.Format(new CultureInfo("pt-BR"), "{0:C}", parceiro.ValorRepasse),
                parceiro.NumeroVendas,
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