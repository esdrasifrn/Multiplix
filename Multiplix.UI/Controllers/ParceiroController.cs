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
    public class ParceiroController : Controller
    {
        private IServiceParceiro _serviceParceiro { get; set; }

        public ParceiroController(IServiceParceiro serviceParceiro)
        {
            _serviceParceiro = serviceParceiro;
        }

        public IActionResult IndexParceiro()
        {
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
        public IActionResult AdicionarParceiro(UsuarioDTO usuarioDTO, List<ProdutoDTO> produtos)
        {
            ViewData["Title"] = "Novo Parceiro";
            return SalvarParceiro(usuarioDTO, "Parceiro adicionado com sucesso!");
        }

        [HttpGet]
        public IActionResult EditarParceiro(int parceiroId)
        {
            ViewData["Title"] = "Editar Parceiro";
            Parceiro parceiro = _serviceParceiro.ObterPorId(parceiroId);
            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Nome = parceiro.Usuario.Nome,               
                CNPJ = parceiro.CNPJ,               
                Rua = parceiro.Rua,
                Numero = parceiro.Numero,
                CEP = parceiro.CEP,
                Cidade = parceiro.Cidade,
                Bairro = parceiro.Bairro,
                Complemento = parceiro.Complemento,
                Estado = parceiro.Estado,
                Email = parceiro.Usuario.Email,               
                Celular = parceiro.Usuario.Celular,              
                Login = parceiro.Usuario.Login,
                HorarioFuncionamento = parceiro.HorarioFuncionamento,
                PontoPorReal = parceiro.PontoPorReal,
                RamoAtividadeId = parceiro.Ramo.RamoAtividadeId,
                RamoAtividadeNome = parceiro.Ramo.Nome
            };

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
                    case "6":
                        orderByExpr = x => x.PontoPorReal;
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
                    parceiro.ParceiroId,                   
                    parceiro.Usuario.Nome,
                    parceiro.CNPJ,
                    parceiro.Usuario.Celular,
                    parceiro.Usuario.Email,
                    parceiro.PontoPorReal,

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
    }
}