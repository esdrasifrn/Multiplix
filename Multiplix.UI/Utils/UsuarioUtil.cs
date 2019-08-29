using System;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Multiplix.UI.Utils
{
    public class UsuarioUtils
    {
        public static Usuario GetUsuarioLogado(HttpContext httpContext, IServiceUsuario serviceUsuario)
        {
            int usuarioId = 0;
            string usuarioLogin = "";
            string usuarioNome = "";

            foreach (var idt in httpContext.User.Identities)
            {
                foreach (var claim in idt.Claims)
                {
                    if (claim.Type == "UsuarioId")
                        usuarioId = Convert.ToInt32(claim.Value);
                    else if (claim.Type == "UsuarioLogin")
                        usuarioLogin = claim.Value;
                    else if (claim.Type == "UsuarioNome")
                        usuarioNome = claim.Value;
                }
            }

            if (usuarioId != 0)
            {
                return serviceUsuario.ObterPorId(usuarioId);
            }
            else
            {
                return null;
            }
        }
    }
}
