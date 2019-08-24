using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Multiplix.Domain.Services
{
    public class ServiceGrupo : IServiceGrupo
    {
        private readonly IGrupoRepository _grupoRepository;

        public ServiceGrupo(IGrupoRepository grupoRepository)
        {
            _grupoRepository = grupoRepository;
        }

        public Grupo Adicionar(Grupo entity)
        {
            return _grupoRepository.Adicionar(entity);
        }

        public void Atualizar(Grupo entity)
        {
            _grupoRepository.Atualizar(entity);
        }

        public IEnumerable<Grupo> Buscar(Expression<Func<Grupo, bool>> predicado)
        {
            return _grupoRepository.Buscar(predicado);
        }

        public Grupo BuscarEntidade(Expression<Func<Grupo, bool>> predicado)
        {
            return _grupoRepository.BuscarEntidade(predicado);
        }

        public Grupo ObterPorId(int id)
        {
            return _grupoRepository.ObterPorId(id);
        }

        public IEnumerable<Grupo> ObterTodos()
        {
            return _grupoRepository.ObterTodos();
        }

        public IEnumerable<Grupo> ObterTodosPaginado(int skip, int take)
        {
            return _grupoRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Grupo entity)
        {
            _grupoRepository.Remover(entity);
        }

        public void RemoverGrupo(int grupoId)
        {
            _grupoRepository.DeleteGrupoUsuarios(grupoId);
            var grupo = _grupoRepository.ObterPorId(grupoId);
            _grupoRepository.Remover(grupo);
        }

        public ValidationResult Salvar(GrupoDTO grupoDTO)
        {
            // grupo
            Grupo grupo;

            if (grupoDTO.GrupoId == 0)
            {
                grupo = new Grupo(
                    nome: grupoDTO.Nome,
                    descricao: grupoDTO.Descricao
                );

                grupo.GrupoId = 0;
            }
            else
            {
                grupo = _grupoRepository.ObterPorId(grupoDTO.GrupoId);

                grupo.Nome = grupoDTO.Nome;
                grupo.Descricao = grupoDTO.Descricao;
            }

            // usuários do grupo
            if (grupoDTO.GrupoId > 0)
                _grupoRepository.DeleteGrupoUsuarios(grupo.GrupoId);

            if (grupoDTO.Usuarios.Count > 0)
            {
                foreach (var usuarioDTO in grupoDTO.Usuarios)
                {
                    UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
                    usuarioGrupo.GrupoId = grupo.GrupoId;
                    usuarioGrupo.UsuarioId = usuarioDTO.UsuarioId;

                    // adiciona o usuário ao grupo
                    grupo.AddUsuarioGrupo(usuarioGrupo);
                }
            }

            ValidationResult result = new GrupoValidator().Validate(grupo);

            if (result.IsValid)
            {
                if (grupo.GrupoId == 0)
                    _grupoRepository.Adicionar(grupo);
                else
                    _grupoRepository.Atualizar(grupo);
            }
            else
            {
                grupoDTO.ValidationErrors = result.Errors;
            }

            return result;
        }

        /// <summary>
        /// Salva as permissões do grupo
        /// </summary>
        /// <param name="grupoDTO">ids das permissões a serem salvas no grupo devem ser passadas no grupoDTO</param>
        /// <returns>entidade grupo validada</returns>
        public ValidationResult SalvarPermissaoGrupo(GrupoDTO grupoDTO)
        {
            // grupo
            Grupo grupo;
            grupo = _grupoRepository.ObterPorId(grupoDTO.GrupoId);
            string[] permissoesIDs = null;

            _grupoRepository.DeletePermissoesGrupo(grupoDTO.GrupoId);
            if (!String.IsNullOrEmpty(grupoDTO.PermissoesIDs))
            {
                permissoesIDs = grupoDTO.PermissoesIDs.Split(',');

                #region permissoes do grupo
                if (permissoesIDs.Length > 0)
                {
                    for (int idx = 0; idx < permissoesIDs.Length; idx++)
                    {
                        int idPermissao = int.Parse(permissoesIDs[idx]);
                        PermissaoGrupo permissaoGrupo = new PermissaoGrupo();
                        permissaoGrupo.GrupoId = grupo.GrupoId;
                        permissaoGrupo.PermissaoId = idPermissao;

                        // adiciona permissão ao grupo
                        grupo.AddPermissaoGrupo(permissaoGrupo);
                    }
                }
                #endregion
            }
            ValidationResult result = new GrupoValidator().Validate(grupo);

            if (result.IsValid)
            {
                _grupoRepository.Atualizar(grupo);
            }
            else
            {
                grupoDTO.ValidationErrors = result.Errors;
            }

            return result;
        }
    }
}
