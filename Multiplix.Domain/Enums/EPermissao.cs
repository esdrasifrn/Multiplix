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
               #region Associado

		        _("pode_visualizar_associado", "Pode visualizar associado"),
                _("pode_cadastrar_associado", "Pode cadastrar associado"),
                _("pode_alterar_associado", "Pode alterar associado"),
                _("pode_visualizar_link_convite", "Pode visualizar link de convite"),
                _("pode_visualizar_compras_por_associado", "Pode visualizar compras por associado"),
                _("pode_visualizar_minha_rede", "Pode visualizar minha rede"),
                _("pode_visualizar_menu_rede", "Pode visualizar menu rede"),              
                _("pode_visualizar_menu_associado", "Pode visualizar menu do associado"),
                _("pode_visualizar_minhas_compras", "Pode visualizar minhas compras"),
                _("pode_visualizar_produtos_por_parceiro", "Pode visualizar produtos por parceiro"),                

	            #endregion

               #region Administração
		
                _("pode_visualizar_administracao", "Pode visualizar administração"),
                _("pode_visualizar_ramo_atividade", "Pode visualizar ramo de atividade"),
                _("pode_cadastrar_ramo_atividade", "Pode cadastrar ramo de atividade"),
                _("pode_alterar_ramo_atividade", "Pode alterar ramo de atividade"),

                _("pode_visualizar_banco", "Pode visualizar banco"),
                _("pode_cadastrar_banco", "Pode cadastrar banco"),
                _("pode_alterar_banco", "Pode alterar banco"),

                _("pode_visualizar_produto", "Pode visualizar produto"),
                _("pode_cadastrar_produto", "Pode cadastrar produto"),
                _("pode_alterar_produto", "Pode alterar produto"),

                _("pode_visualizar_plano_assinatura", "Pode visualizar plano de assinatura"),
                _("pode_cadastrar_plano_assinatura", "Pode cadastrar plano de assinatura"),
                _("pode_alterar_plano_assinatura", "Pode alterar plano de assinatura"),

                _("pode_visualizar_unidade", "Pode visualizar unidade"),
                _("pode_cadastrar_unidade", "Pode cadastrar unidade"),
                _("pode_alterar_unidade", "Pode alterar unidade"), 

	            #endregion

                _("pode_visualizar_parceiro", "Pode visualizar parceiro"),
                _("pode_cadastrar_parceiro", "Pode cadastrar parceiro"),
                _("pode_alterar_parceiro", "Pode alterar parceiro"),
                _("pode_cadastrar_compras", "Pode cadastrar compras"),
                _("pode_visualizar_compras_parceiro", "Pode visualizar compras parceiro"),
                _("pode_visualizar_menu_parceiro", "Pode visualizar menu do parceiro"),                


                _("pode_visualizar_grupo", "Pode visualizar grupo"),
                _("pode_cadastrar_grupo", "Pode cadastrar grupo"),
                _("pode_alterar_grupo", "Pode alterar grupo"),
                _("pode_excluir_grupo", "Pode excluir grupo"),

                _("pode_visualizar_permissao", "Pode visualizar permissões"),
                _("pode_cadastrar_permissao", "Pode cadastrar permissões"),
                _("pode_alterar_permissao", "Pode alterar permissões"),


                _("pode_visualizar_menu_financeiro", "Pode visualizar menu financeiro"),
                _("pode_visualizar_saldo", "Pode visualizar saldo"),

                _("pode_visualizar_menu_relatorio", "Pode visualizar menu relatório"),

                _("pode_visualizar_dash_parceiro", "Pode visualizar dash do parceiro"),
                _("pode_visualizar_dash_associado", "Pode visualizar dash do associado"),
                

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
