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
    public class EntradaController : Controller
    {
        readonly IServiceUsuario _serviceUsuario;
        readonly IServiceEntrada _serviceEntrada;

        public EntradaController(IServiceUsuario serviceUsuario, IServiceEntrada serviceEntrada)
        {
            _serviceUsuario = serviceUsuario;
            _serviceEntrada = serviceEntrada;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Entradas()
        {
            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_entradas"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }          

            EntradaDTO entradaDTO = new EntradaDTO();
            DateTime date = DateTime.Now;

            entradaDTO.DataInicio = new DateTime(date.Year, date.Month, 1);
            entradaDTO.DataFim = entradaDTO.DataInicio.AddMonths(1).AddDays(-1);

            ViewBag.Di = new DateTime(date.Year, date.Month, 1).ToString("dd-MM-yyyy HH:mm:ss");
            ViewBag.Df = entradaDTO.DataFim.ToString("dd-MM-yyyy HH:mm:ss");

            return View(entradaDTO);
        }

        [HttpPost]
        public IActionResult Entradas(EntradaDTO entradaDTO)
        {
            DateTime date = DateTime.Now;
            ViewBag.Di = entradaDTO.DataInicio.ToString("dd-MM-yyyy HH:mm:ss");
            ViewBag.Df = entradaDTO.DataFim.ToString("dd-MM-yyyy HH:mm:ss");

            return View(entradaDTO);
        }

        [HttpGet]
        public IActionResult AdicionarEntrada()
        {
            ViewData["Title"] = "Nova Entrada";
            EntradaDTO entradaDTO = new EntradaDTO();
            entradaDTO.DataVencimento = DateTime.Now;

            return View("AdicionarEditarEntrada", entradaDTO);
        }

        [HttpPost]
        public IActionResult AdicionarEntrada(EntradaDTO entradaDTO)
        {
            ViewData["Title"] = "Nova Entrada";
            return SalvarEntrada(entradaDTO, "Entrada adicionada com sucesso!");
        }

        [HttpGet]
        public IActionResult EditarEntrada(int entradaId)
        {
            ViewData["Title"] = "Editar Entrada";
            Entrada entrada = _serviceEntrada.ObterPorId(entradaId);
            EntradaDTO entradaDTO = new EntradaDTO()
            {
                DataVencimento = entrada.DataVencimento,
                Valor = entrada.Valor,
                Status = entrada.Status,
                Descricao = entrada.Descricao,
                AssociadoId = entrada.Associado.Id,
                NomeAssociado = entrada.Associado.Usuario.Nome
            };

            return View("AdicionarEditarEntrada", entradaDTO);
        }

        [HttpPost]
        public IActionResult EditarEntrada(EntradaDTO entradaDTO)
        {
            ViewData["Title"] = "Editar entrada";
            return SalvarEntrada(entradaDTO, "Entrada alterada com sucesso!");
        }

        private IActionResult SalvarEntrada(EntradaDTO entradaDTO, string mensagemRetorno)
        {
            var result = _serviceEntrada.Salvar(entradaDTO);

            if (result.IsValid)
            {
                TempData["success"] = mensagemRetorno;
                return RedirectToAction("Entradas");
            }
            return View("AdicionarEditarEntrada", entradaDTO);
        }

        [HttpPost]
        public JsonResult ListaEntradas(DataTableAjaxPostModel dataTableModel, string DataInicio, string DataFim)
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

            IEnumerable<Entrada> entradas = new List<Entrada>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                entradas = _serviceEntrada.Buscar(x=> x.DataVencimento >= di && x.DataVencimento <= df)
                    .Where(x => x.Associado.Usuario.Nome.ToUpper()
                    .Contains(searchTerm.ToUpper()) || x.Associado.Usuario.Nome.ToUpper()
                    .Contains(searchTerm.ToUpper()));
            }
            else
                entradas = _serviceEntrada.Buscar(x => x.DataVencimento >= di && x.DataVencimento <= df);

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Entrada, Object> orderByExpr = null;

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
                        orderByExpr = x => x.Status;
                        break;
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        entradas = entradas.OrderByDescending(orderByExpr);
                    else
                        entradas = entradas.OrderBy(orderByExpr);
                }
                else
                {
                    entradas = entradas.OrderBy(x => x.Associado.Usuario.Nome);
                }
            }
            else
            {
                entradas = entradas.OrderBy(x => x.Associado.Usuario.Nome);
            }

            // pagina a lista
            int totalResultados = entradas.Count();
            entradas = entradas.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var entrada in entradas)
            {
                List<object> result_item = new List<object> {
                entrada.EntradaId,
                entrada.Associado.Usuario.Nome,
                entrada.Descricao,
                String.Format(new CultureInfo("pt-BR"),"{0:d/M/yyyy}", entrada.DataVencimento),
                String.Format(new CultureInfo("pt-BR"), "{0:C}", entrada.Valor),
                AssociadoUtil.AplicarFormatacaoStatus(EStatusMovitacaoChoices.Choices.Find(x => x.ValueInt == entrada.Status).Name)                
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