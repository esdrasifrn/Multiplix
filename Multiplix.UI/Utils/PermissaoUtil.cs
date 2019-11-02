using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Multiplix.Domain.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Multiplix.UI.Utils
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PermissaoRequeridaAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string PermissaoNomeId = null;
        private string[] PermissaoNomeIdLista = null;
        private bool PermissaoNomeIdListaOperadorOR = false; // AND por padrão

        public PermissaoRequeridaAttribute(string permissaoNomeId)
        {
            this.PermissaoNomeId = permissaoNomeId;
        }

        public PermissaoRequeridaAttribute(string[] permissaoNomeIdLista, bool operadorOR)
        {
            this.PermissaoNomeIdLista = permissaoNomeIdLista;
            this.PermissaoNomeIdListaOperadorOR = operadorOR;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            bool autorizado = false;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new RedirectResult("Usuario/LogOn");
                return;
            }

            if (this.PermissaoNomeId != null)
            {
                autorizado = PermissaoRequerida.TemPermissao(context.HttpContext, this.PermissaoNomeId);
            }
            else if (this.PermissaoNomeIdLista != null)
            {
                int qtdTrue = 0;

                foreach (var permissao in this.PermissaoNomeIdLista)
                {
                    bool temPermissao = PermissaoRequerida.TemPermissao(context.HttpContext, permissao);
                    if (temPermissao)
                        qtdTrue++;
                }

                if (this.PermissaoNomeIdListaOperadorOR)
                {
                    autorizado = qtdTrue > 0;
                }
                else
                {
                    autorizado = qtdTrue == this.PermissaoNomeIdLista.Length;
                }
            }
            else
            {
                autorizado = false;
            }

            if (!autorizado)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class PermissaoApenasSuperUserAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new RedirectResult("Usuario/LogOn");
                return;
            }

            if (!PermissaoRequerida.IsSuperUser(context.HttpContext))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }

    public class PermissaoRequerida
    {
        public static bool TemPeloMenosUmaPermissao(HttpContext httpContext, List<string> permissoes)
        {
            foreach (var permissao in permissoes)
            {
                if (TemPermissao(httpContext, permissao)) return true;
            }

            return false;
        }

        public static bool TemPermissao(HttpContext httpContext, string permissaoNomeId)
        {
            try
            {
                var permissoes = httpContext.Session.GetString("permissoes");
                var superUsuario = httpContext.Session.GetString("superUsuario");
                var usuarioPermissoes = JsonConvert.DeserializeObject<List<PermissaoDTO>>(permissoes);

                if (superUsuario == "true")
                {
                    return true;
                }

                foreach (var permissao in usuarioPermissoes)
                {
                    if (permissao.NomeId == permissaoNomeId)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static bool IsSuperUser(HttpContext httpContext)
        {
            bool UsuarioIsSuperUser = false;

            foreach (var idt in httpContext.User.Identities)
            {
                foreach (var claim in idt.Claims)
                {
                    if (claim.Type == "UsuarioIsSuperUser")
                    {
                        UsuarioIsSuperUser = claim.Value == "true" ? true : false;
                    }
                }
            }

            return UsuarioIsSuperUser;
        }      
    }
}
