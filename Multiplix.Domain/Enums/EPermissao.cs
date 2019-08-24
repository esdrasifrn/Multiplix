using Multiplix.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Enums
{
    public class EPermissao
    {
        public static List<PermissaoDTO> All()
        {
            return new List<PermissaoDTO>()
            {               
                _("pode_visualizar_usuario", "Pode visualizar usuário"),
                _("pode_cadastrar_usuario", "Pode cadastrar usuário"),
                _("pode_alterar_usuario", "Pode alterar usuário"),
                _("pode_excluir_usuario", "Pode excluir usuário"),

                _("pode_visualizar_grupo", "Pode visualizar grupo"),
                _("pode_cadastrar_grupo", "Pode cadastrar grupo"),
                _("pode_alterar_grupo", "Pode alterar grupo"),
                _("pode_excluir_grupo", "Pode excluir grupo"),

                _("pode_visualizar_permissao_usuario", "Pode visualizar permissões do usuário"),
                _("pode_cadastrar_permissao_usuario", "Pode cadastrar permissões do usuário"),
                _("pode_alterar_permissao_usuario", "Pode alterar permissões do usuário"),
            };
        }

        public static PermissaoDTO Get(string nomeId)
        {
            foreach (var permissao in All())
            {
                if (permissao.NomeId == nomeId)
                {
                    return permissao;
                }
            }

            return null;
        }

        private static PermissaoDTO _(string nomeId, string descricao)
        {
            return new PermissaoDTO
            {
                NomeId = nomeId,
                Descricao = descricao
            };
        }
    }
}
