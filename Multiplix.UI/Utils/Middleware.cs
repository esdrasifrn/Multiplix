using System;
using System.Linq;
using System.Net;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Multiplix.UI.Utils
{
    public class GlobalFilter : IActionFilter
    {
        private readonly IServicePermissao _servicePermissao;
        private readonly IServiceUsuario _serviceUsuario;
        //private readonly IServiceNotificacao _serviceNotificacao;

        public GlobalFilter(IServicePermissao servicePermissao, IServiceUsuario serviceUsuario /*,IServiceNotificacao serviceNotificacao*/)
        {
            _servicePermissao = servicePermissao;
            _serviceUsuario = serviceUsuario;
            //_serviceNotificacao = serviceNotificacao;
        }

        //public static void RecalcularNotificacoes(HttpContext httpContext, /*IServiceNotificacao serviceNotificacao, */Usuario usuario)
        //{
        //    httpContext.Session.SetString("notificacoes", JsonConvert.SerializeObject(serviceNotificacao.ObterListaNotificacoes(usuario)));
         
        //    var contador = serviceNotificacao.ContarNotificacoes(usuario);
        //    var resultado = "Nenhuma notificação";
        //    if (contador > 0)
        //    {
        //        resultado = contador == 1 ? "1 nova notificação" : $"{contador} novas notificações";
        //    }
        //    httpContext.Session.SetString("contadorNotificacoes", resultado);
        //}

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {

                var usuarioId = context.HttpContext.User.Claims.First(x => x.Type == "UsuarioId").Value;
                var usuario = _serviceUsuario.ObterPorId(int.Parse(usuarioId));
                if (context.HttpContext.Session.GetString("permissoes") == null)
                {
                    context.HttpContext.Session.SetString("usuario", usuarioId);
                    context.HttpContext.Session.SetString("permissoes",
                        JsonConvert.SerializeObject(_servicePermissao.UsuarioObterTodasPermissoes(usuario)));
                    context.HttpContext.Session.SetString("superUsuario", usuario.IsSuperUser ? "true" : "false");
                }

                //RecalcularNotificacoes(context.HttpContext, _serviceNotificacao, usuario);

            }
            catch
            {
                // ignored
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
