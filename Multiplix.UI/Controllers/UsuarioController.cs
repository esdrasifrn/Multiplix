﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.UI.Models;
using Newtonsoft.Json;

namespace Multiplix.UI.Controllers
{
    public class UsuarioController : Controller
    {
        private IServiceUsuario _serviceUsuario;
        private IServiceGrupo _serviceGrupo;
        private IServicePermissao _servicePermissao;
        private IServicePatrocinador _servicePatrocinador;

        public UsuarioController(IServiceUsuario serviceUsuario, IServiceGrupo serviceGrupo, IServicePermissao servicePermissao,
            IServicePatrocinador servicePatrocinador)
        {
            _serviceUsuario = serviceUsuario;
            _serviceGrupo = serviceGrupo;
            _servicePermissao = servicePermissao;
            _servicePatrocinador = servicePatrocinador;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> LogOn(IFormCollection f, string returnUrl)
        {
            var usuario = _serviceUsuario.Autenticar(f["usuario"], f["senha"]);
            if (usuario != null)
            {
                HttpContext.Session.SetString("usuario", f["usuario"]);
                HttpContext.Session.SetString("permissoes", JsonConvert.SerializeObject(_servicePermissao.UsuarioObterTodasPermissoes(usuario)));
                HttpContext.Session.SetString("superUsuario", usuario.IsSuperUser ? "true" : "false");
                // criar uma identificação com 3 chaves personalizadas (ID do usuário, login e nome)
                // e 1 chave padrão
                var claims = new List<Claim>()
                {
                    new Claim("UsuarioId", usuario.UsuarioId + ""),
                    new Claim("UsuarioLogin", usuario.Login),
                    new Claim("UsuarioNome", usuario.Nome),                    
                    new Claim("UsuarioIsSuperUser", usuario.IsSuperUser ? "true" : "false"),                   
                    new Claim(ClaimTypes.Name, usuario.Nome)
                };

                var userIdentity = new ClaimsIdentity(claims, "UsuarioLogin");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return Redirect("/Home/Dashboard");
            }
            else
            {
                ViewBag.Mensagem = "Login ou senha inválidos. tente novamente!";
                return View();
            }
        }

        public async Task<ActionResult> LogOffAsync()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return Redirect("/Usuario/LogOn");
        }

        [HttpGet]
        public IActionResult AdicionarAssociado()
        {
            ViewData["Title"] = "Novo Associado";
            UsuarioDTO usuarioDTO = new UsuarioDTO();
            return View("AdicionarEditarPatrocinador", usuarioDTO);
        }

        [HttpPost]
        public IActionResult AdicionarAssociado(UsuarioDTO usuarioDTO)
        {
            ViewData["Title"] = "Novo Associado";
            return SalvarAssociado(usuarioDTO, "Associado adicionado com sucesso!");
        }

        [HttpGet]
        public IActionResult EditarAssociado(int associadoId)
        {
            ViewData["Title"] = "Editar Associado";
            Associado associado = _servicePatrocinador.ObterPorId(associadoId);
            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Nome = associado.Usuario.Nome,
                Nascimento = associado.Nascimento,
                Sexo = associado.Sexo,
                CPF = associado.CPF,
                Liberado = associado.Usuario.Liberado,
                Rua = associado.Rua,
                Numero = associado.Numero,
                CEP = associado.CEP,
                Cidade = associado.Cidade,
                Bairro = associado.Bairro,
                Complemento = associado.Complemento,
                Estado = associado.Estado,
                Email = associado.Usuario.Email,
                EmailAlternativo = associado.EmailAlternativo,
                Celular = associado.Usuario.Celular,
                BancoNome = associado.Banco.Nome,
                BancoId = associado.Banco.BancoId,
                TipoConta = associado.TipoConta,
                Agencia = associado.Agencia,
                Conta = associado.Conta,
                Login = associado.Usuario.Login

            };

            return View("AdicionarEditarPatrocinador", usuarioDTO);
        }

        [HttpPost]
        public IActionResult EditarAssociado(UsuarioDTO usuarioDTO)
        {
            ViewData["Title"] = "Editar associado";
            return SalvarAssociado(usuarioDTO, "Associado alterado com sucesso!");
        }

        private IActionResult SalvarAssociado(UsuarioDTO usuarioDTO, string mensagemRetorno)
        {
            var result = _servicePatrocinador.SalvarAssociadoSemConvite(usuarioDTO);

            if (result.IsValid)
            {
                TempData["success"] = mensagemRetorno;
                return RedirectToAction("Index");
            }
            return View("AdicionarEditarPatrocinador", usuarioDTO);
        }

        [HttpPost]
        public JsonResult ListaPatrocinadores(DataTableAjaxPostModel dataTableModel)
        {
            /*
             * consumido por um DataTable serverSide processing ajax POST
             * 
             * o código deste controlador pode ser usado como base para futuras implementações genéricas com DataTable
             */

            string searchTerm = dataTableModel.search.value;
            string firstOrderColumnIdx = dataTableModel.order.Count > 0 ? dataTableModel.order[0].column.ToString() : "";
            string firstOrderDirection = dataTableModel.order.Count > 0 ? dataTableModel.order[0].dir.ToString() : "";

            IEnumerable<Associado> patrocinadores = new List<Associado>();

            if (!String.IsNullOrEmpty(dataTableModel.search.value))
            {
                patrocinadores = _servicePatrocinador.Buscar(
                    x => x.IdCarteira.Contains(searchTerm) ||
                         x.Usuario.Nome.Contains(searchTerm) ||
                         x.Usuario.Celular.Contains(searchTerm) ||
                         x.Usuario.Email.Contains(searchTerm) ||
                         x.Usuario.Login.Contains(searchTerm)
                );
            }
            else
                patrocinadores = _servicePatrocinador.ObterTodos();

            if (firstOrderColumnIdx.Length > 0)
            {
                Func<Associado, Object> orderByExpr = null;

                switch (firstOrderColumnIdx)
                {
                    case "1":
                        orderByExpr = x => x.IdCarteira;
                        break;
                    case "2":
                        orderByExpr = x => x.Usuario.Nome;
                        break;
                    case "3":
                        orderByExpr = x => x.Usuario.Celular;
                        break;
                    case "4":
                        orderByExpr = x => x.Usuario.Email;
                        break;
                    case "5":
                        orderByExpr = x => x.Usuario.Login;
                        break;                    
                }

                if (orderByExpr != null)
                {
                    if (firstOrderDirection.Length > 0 && firstOrderDirection.Equals("desc"))
                        patrocinadores = patrocinadores.OrderByDescending(orderByExpr);
                    else
                        patrocinadores = patrocinadores.OrderBy(orderByExpr);
                }
                else
                {
                    patrocinadores = patrocinadores.OrderBy(x => x.Usuario.Nome);
                }
            }
            else
            {
                patrocinadores = patrocinadores.OrderBy(x => x.Usuario.Nome);
            }

            // pagina a lista
            int totalResultados = patrocinadores.Count();
            patrocinadores = patrocinadores.Skip(dataTableModel.start).Take(dataTableModel.length);

            // monta o resultado final
            List<object> result_data = new List<object>();
            foreach (var patrocinador in patrocinadores)
            {               
                List<object> result_item = new List<object> {
                    patrocinador.Id,
                    patrocinador.IdCarteira,
                    patrocinador.Usuario.Nome,
                    patrocinador.Usuario.Celular,
                    patrocinador.Usuario.Email,
                   
                    _servicePatrocinador.ObterPorId(id: patrocinador.PatrocinadorId.Value).Usuario.Nome ?? "-"
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