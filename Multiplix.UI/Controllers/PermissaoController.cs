using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.UI.Utils;

namespace Multiplix.UI.Controllers
{
    public class PermissaoController : Controller
    {
        private IServiceUsuario _serviceUsuario;
        private IServiceGrupo _serviceGrupo;
        private IServicePermissao _servicePermissao;

        public PermissaoController(IServiceUsuario serviceUsuario, IServiceGrupo serviceGrupo, IServicePermissao servicePermissao)
        {
            _serviceUsuario = serviceUsuario;
            _serviceGrupo = serviceGrupo;
            _servicePermissao = servicePermissao;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult PermissaoUsuario(int usuarioId)
        {
            ViewData["Title"] = "Permissões do Usuário";

            Expression<Func<Usuario, bool>> FiltroExp = r => r.UsuarioId.Equals(usuarioId);
            var usuario = _serviceUsuario.BuscarEntidade(FiltroExp);

            // todas as permissões cadastradas
            var permissoes = _servicePermissao.ObterTodos();
            IList<PermissaoDTO> permissoesCadastrados = new List<PermissaoDTO>();
            foreach (var permissao in permissoes)
            {
                permissoesCadastrados.Add(new PermissaoDTO()
                {
                    Id = permissao.PermisaoId,
                    NomeId = permissao.NomeId,
                    Descricao = permissao.Descricao
                });
            }

            List<PermissaoDTO> permissoesUsuario = _servicePermissao.UsuarioObterTodasPermissoes(usuario);

            ViewBag.ListaPermissaoTodas = permissoesCadastrados;
            ViewBag.ListaPermissaoUsuario = permissoesUsuario;
            ViewBag.Usuario = usuario;

            return View();
        }

        [HttpPost]
        public ActionResult SalvarPermissaoUsuario(UsuarioDTO dto)
        {
            var resultado = _serviceUsuario.SalvarPermissaoUsuario(dto);

            if (resultado.IsValid)
            {
                return Redirect("/Usuario/Index");
            }
            else
            {
                return View(dto);
            }

        }


        public ActionResult PermissaoGrupo(int grupoId)
        {
            ViewData["Title"] = "Permissões do Grupo";

            Expression<Func<Grupo, bool>> FiltroExp = r => r.GrupoId.Equals(grupoId);
            var grupo = _serviceGrupo.BuscarEntidade(FiltroExp);

            // todas as permissões cadastradas
            IList<PermissaoDTO> permissoesCadastrados = new List<PermissaoDTO>();
            var permissoes = _servicePermissao.ObterTodos();

            foreach (var permissao in permissoes)
            {
                permissoesCadastrados.Add(new PermissaoDTO()
                {
                    Id = permissao.PermisaoId,
                    NomeId = permissao.NomeId,
                    Descricao = permissao.Descricao
                });
            }

            IList<PermissaoDTO> permissoesGrupo = new List<PermissaoDTO>();

            // permissões do grupo
            foreach (var permissaoGrupo in grupo.PermissaoGrupos)
            {
                permissoesGrupo.Add(new PermissaoDTO
                {
                    Id = permissaoGrupo.Permissao.PermisaoId,
                    NomeId = permissaoGrupo.Permissao.NomeId,
                    Descricao = permissaoGrupo.Permissao.Descricao
                });
            }

            ViewBag.ListaPermissaoTodas = permissoesCadastrados;
            ViewBag.ListaPermissaoGrupo = permissoesGrupo;
            ViewBag.Grupo = grupo;

            return View();

        }

        [HttpPost]
        public ActionResult SalvarPermissaoGrupo(GrupoDTO dto)
        {
            var resultado = _serviceGrupo.SalvarPermissaoGrupo(dto);

            if (resultado.IsValid)
            {
                return Redirect("/Usuario/IndexGrupo");
            }
            else
            {
                return View(dto);
            }

        }

        [PermissaoApenasSuperUser]
        public JsonResult SyncPermissao()
        {
            _servicePermissao.SyncPermissao();

            return Json(new
            {
                message = "Permissões sincronizadas."
            });
        }

        public IActionResult UnauthorizedResult()
        {
            return View();
        }

        public IActionResult Ativacao()
        {
            return View();
        }
    }
}