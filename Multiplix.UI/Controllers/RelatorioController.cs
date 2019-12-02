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
using Multiplix.UI.Models.ViewModel;

namespace Multiplix.UI.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IServicePatrocinador _servicePatrocinador;

        public RelatorioController(IServicePatrocinador servicePatrocinador)
        {
            _servicePatrocinador = servicePatrocinador;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Aniversariantes()
        {
            AniversariantesVM aniversariantesVM = new AniversariantesVM();
            DateTime date = DateTime.Now;

            ViewBag.mes = date.Month;

            return View();
        }

        [HttpPost]
        public IActionResult Aniversariantes(AniversariantesVM aniversariantesVM)
        {
            ViewBag.mes = aniversariantesVM.Mes;

            return View(aniversariantesVM);
        }

        public IActionResult DiasSemComprar()
        {
            DiasSemComprarVM diasSemComprarVM = new DiasSemComprarVM();
            diasSemComprarVM.Dias = 1;
            ViewBag.dias = 1;

            return View(diasSemComprarVM);
        }

        [HttpPost]
        public IActionResult DiasSemComprar(DiasSemComprarVM diasSemComprarVM)
        {           
            ViewBag.dias = diasSemComprarVM.Dias;

            return View(diasSemComprarVM);
        }


        [HttpPost]
        public JsonResult ListaAniversariantes(DataTableAjaxPostModel dataTableModel, int mes)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable            */
           

            string searchTerm = dataTableModel.search.value?.ToUpper();
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";          

            IEnumerable<Associado> associados = new List<Associado>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                associados = _servicePatrocinador.GetAssociadosAniversariantes(mes);                  
            }
            else
                associados = _servicePatrocinador.GetAssociadosAniversariantes(mes);

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Associado, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.Usuario.Nome;
                        break;
                    case "2":
                        orderByExpr = x => x.Nascimento;
                        break;                   
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        associados = associados.OrderByDescending(orderByExpr);
                    else
                        associados = associados.OrderBy(orderByExpr);
                }
                else
                {
                    associados = associados.OrderBy(x => x.Usuario.Nome);
                }
            }
            else
            {
                associados = associados.OrderBy(x => x.Usuario.Nome);
            }

            // pagina a lista
            int totalResultados = associados.Count();
            associados = associados.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var associado in associados)
            {
                List<object> result_item = new List<object> {
                associado.Usuario.UsuarioId,
                associado.Usuario.Nome,
                String.Format(new CultureInfo("pt-BR"),"{0:d/M/yyyy}", associado.Nascimento)
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
        public JsonResult ListaDiasSemComprar(DataTableAjaxPostModel dataTableModel, int dias)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable            */


            string searchTerm = dataTableModel.search.value?.ToUpper();
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<DiasSemComprarDTO> associadosSemComprar = new List<DiasSemComprarDTO>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                associadosSemComprar = _servicePatrocinador.GetDiasSemConsumo(dias);
            }
            else
                associadosSemComprar = _servicePatrocinador.GetDiasSemConsumo(dias);

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<DiasSemComprarDTO, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.NomeAssociado;
                        break;
                    case "2":
                        orderByExpr = x => x.DiasSemComprar;
                        break;
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        associadosSemComprar = associadosSemComprar.OrderByDescending(orderByExpr);
                    else
                        associadosSemComprar = associadosSemComprar.OrderBy(orderByExpr);
                }
                else
                {
                    associadosSemComprar = associadosSemComprar.OrderBy(x => x.NomeAssociado);
                }
            }
            else
            {
                associadosSemComprar = associadosSemComprar.OrderBy(x => x.NomeAssociado);
            }

            // pagina a lista
            int totalResultados = associadosSemComprar.Count();
            associadosSemComprar = associadosSemComprar.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var associado in associadosSemComprar)
            {
                List<object> result_item = new List<object> {
                associado.UsuarioId,
                associado.NomeAssociado,
                associado.DataUltimaCompra,
                String.Format(new CultureInfo("pt-BR"),"{0:d/M/yyyy}", associado.DiasSemComprar)                               
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