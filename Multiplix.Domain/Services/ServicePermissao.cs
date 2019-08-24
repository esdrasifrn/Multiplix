using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Enums;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace QuizCorp.Domain.Services
{
    public class ServicePermissao : IServicePermissao
    {
        private readonly IPermissaoRepository _permissaoRepository;

        public ServicePermissao(IPermissaoRepository permissaoRepository)
        {
            _permissaoRepository = permissaoRepository;
        }
        public Permissao Adicionar(Permissao entity)
        {
            return _permissaoRepository.Adicionar(entity);
        }

        public void Atualizar(Permissao entity)
        {
            _permissaoRepository.Atualizar(entity);
        }

        public IEnumerable<Permissao> Buscar(Expression<Func<Permissao, bool>> predicado)
        {
            return _permissaoRepository.Buscar(predicado);
        }

        public Permissao BuscarEntidade(Expression<Func<Permissao, bool>> predicado)
        {
            return _permissaoRepository.BuscarEntidade(predicado);
        }

        public Permissao ObterPorId(int id)
        {
            return _permissaoRepository.ObterPorId(id);
        }

        public IEnumerable<Permissao> ObterTodos()
        {
            return _permissaoRepository.ObterTodos();
        }

        public IEnumerable<Permissao> ObterTodosPaginado(int skip, int take)
        {
            return _permissaoRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Permissao entity)
        {
            _permissaoRepository.Remover(entity);
        }

        public void SyncPermissao()
        {
            // atualiza permissões existentes ou adiciona novas permissões
            foreach (var permissaoSistema in EPermissao.All())
            {
                var permissaoBanco = _permissaoRepository.BuscarEntidade(x => x.NomeId == permissaoSistema.NomeId);
                if (permissaoBanco != null)
                {
                    permissaoBanco.Descricao = permissaoSistema.Descricao;

                    _permissaoRepository.Atualizar(permissaoBanco);
                }
                else
                {
                    _permissaoRepository.Adicionar(new Permissao(permissaoSistema.NomeId, permissaoSistema.Descricao));
                }
            }

            // remove permissões que deixaram de existir/órfãs
            _permissaoRepository.RemoverPermissoesOrfas();
        }

        public List<PermissaoDTO> UsuarioObterTodasPermissoes(Usuario usuario)
        {
            List<PermissaoDTO> permissoesUsuario = new List<PermissaoDTO>();

            // permissões indiretas (via grupos) do usuário
            foreach (var usuarioGrupo in usuario.UsuarioGrupos)
            {
                foreach (var permissaoGrupo in usuarioGrupo.Grupo.PermissaoGrupos)
                {
                    permissoesUsuario.Add(new PermissaoDTO
                    {
                        Id = permissaoGrupo.Permissao.PermisaoId,
                        NomeId = permissaoGrupo.Permissao.NomeId,
                        Descricao = permissaoGrupo.Permissao.Descricao
                    });
                }
            }

            // permissões diretas do usuário
            foreach (var permissaoUsuario in usuario.PermissaoUsuarios)
            {
                permissoesUsuario.Add(new PermissaoDTO
                {
                    Id = permissaoUsuario.Permissao.PermisaoId,
                    NomeId = permissaoUsuario.Permissao.NomeId,
                    Descricao = permissaoUsuario.Permissao.Descricao
                });
            }

            return permissoesUsuario;
        }
    }
}