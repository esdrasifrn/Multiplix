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
    public class BonusController : Controller
    {
        private IServicePatrocinador  _servicePatrocinador;
        private IServiceUsuario _serviceUsuario;

        public BonusController(IServicePatrocinador servicePatrocinador, IServiceUsuario serviceUsuario)
        {
            _servicePatrocinador = servicePatrocinador;
            _serviceUsuario = serviceUsuario;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Só bônus do usuário logado
        public IActionResult MeusBonus()
        {
            if (!PermissaoRequerida.TemPermissao(HttpContext, "pode_visualizar_meus_bonus"))
            {
                return RedirectToAction("UnauthorizedResult", "Permissao");
            }          

            BonusDTO bonusDTO = new BonusDTO();
            DateTime date = DateTime.Now;

            bonusDTO.DataInicio = new DateTime(date.Year, date.Month, 1);
            bonusDTO.DataFim = DateTime.Now;

            ViewBag.Di = new DateTime(date.Year, date.Month, 1).ToString("dd-MM-yyyy HH:mm:ss");
            ViewBag.Df = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            return View(bonusDTO);
        }

        [HttpPost]
        public IActionResult MeusBonus(BonusDTO bonusDTO)
        {
            DateTime date = DateTime.Now;
            ViewBag.Di = bonusDTO.DataInicio.ToString("dd-MM-yyyy HH:mm:ss");
            ViewBag.Df = bonusDTO.DataFim.ToString("dd-MM-yyyy HH:mm:ss");

            return View(bonusDTO);
        }

        [HttpPost]
        public JsonResult ListaMeusBonus(DataTableAjaxPostModel dataTableModel, string DataInicio, string DataFim)
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

            IEnumerable<Bonus> bonus = new List<Bonus>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                bonus = associadoLogado.Bonus.Where(x => x.AssociadoGerador.Usuario.Nome.ToUpper()
                   .Contains(searchTerm.ToUpper()) && x.DataCadastro >= di && x.DataCadastro <= df);               
            }
            else
                bonus = associadoLogado.Bonus.Where(x => x.DataCadastro >= di && x.DataCadastro <= df);

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Bonus, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.AssociadoGerador.Usuario.Nome;
                        break;
                    case "2":
                        orderByExpr = x => x.DataCadastro;
                        break;
                    case "3":
                        orderByExpr = x => x.Valor;
                        break;
                    case "4":
                        orderByExpr = x => x.AssociadoGerador.Nivel;
                        break;
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        bonus = bonus.OrderByDescending(orderByExpr);
                    else
                        bonus = bonus.OrderBy(orderByExpr);
                }
                else
                {
                    bonus = bonus.OrderBy(x => x.AssociadoGerador.Usuario.Nome);
                }
            }
            else
            {
                bonus = bonus.OrderBy(x => x.AssociadoGerador.Usuario.Nome);
            }

            // pagina a lista
            int totalResultados = bonus.Count();
            bonus = bonus.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var bo in bonus)
            {
                List<object> result_item = new List<object> {
                bo.BonusId,
                bo.AssociadoGerador.Usuario.Nome,
                bo.AssociadoGerador.Nivel,
                String.Format(new CultureInfo("pt-BR"),"{0:d/M/yyyy HH:mm:ss}", bo.DataCadastro),
                String.Format(new CultureInfo("pt-BR"), "{0:C}", bo.Valor),               
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