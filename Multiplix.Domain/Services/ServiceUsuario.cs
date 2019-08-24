using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QuizCorp.Domain.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ServiceUsuario(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Adicionar(Usuario entity)
        {
            return _usuarioRepository.Adicionar(entity);
        }

        public void Atualizar(Usuario entity)
        {
            _usuarioRepository.Atualizar(entity);
        }

        public Usuario Autenticar(string login, string password)
        {
            //Busca o usuário com o login e senha passados
            Expression<Func<Usuario, bool>> FiltroExp = x => x.Login.Equals(login) && x.Senha.Equals(password);
            Usuario user = _usuarioRepository.BuscarEntidade(FiltroExp);

            return user;
        }

        public IEnumerable<Usuario> Buscar(Expression<Func<Usuario, bool>> predicado)
        {
            return _usuarioRepository.Buscar(predicado);
        }

        public Usuario BuscarEntidade(Expression<Func<Usuario, bool>> predicado)
        {
            return _usuarioRepository.BuscarEntidade(predicado);
        }

        public Usuario ObterPorId(int id)
        {
            return _usuarioRepository.ObterPorId(id);
        }

        public IEnumerable<Usuario> ObterTodos()
        {
            return _usuarioRepository.ObterTodos();
        }

        public IEnumerable<Usuario> ObterTodosPaginado(int skip, int take)
        {
            return _usuarioRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Usuario entity)
        {
            _usuarioRepository.Remover(entity);
        }

        public void RemoverUsuario(int usuarioId)
        {
            _usuarioRepository.DeleteUsuarioGrupos(usuarioId);
            var usuario = _usuarioRepository.ObterPorId(usuarioId);
            _usuarioRepository.Remover(usuario);
        }

        public ValidationResult Salvar(UsuarioDTO usuarioDTO)
        {
            // usuário
            Usuario usuario;

            if (usuarioDTO.UsuarioId == 0)
            {
                usuario = new Usuario(
                    login: usuarioDTO.Login,
                    senha: usuarioDTO.Senha,
                    nome: usuarioDTO.Nome,
                    celular: usuarioDTO.Celular,
                    email: usuarioDTO.Email,
                    liberado: usuarioDTO.Liberado
                );

                usuario.UsuarioId = 0;
            }
            else
            {
                usuario = _usuarioRepository.ObterPorId(usuarioDTO.UsuarioId);

                usuario.Login = usuarioDTO.Login;
                usuario.Senha = usuarioDTO.Senha;
                usuario.Nome = usuarioDTO.Nome;
                usuario.Celular = usuarioDTO.Celular;
                usuario.Email = usuarioDTO.Email;
                usuario.Liberado = usuarioDTO.Liberado;
            }

            // grupos do usuário
            if (usuarioDTO.UsuarioId > 0)
                _usuarioRepository.DeleteUsuarioGrupos(usuario.UsuarioId);

            if (usuarioDTO.Grupos.Count > 0)
            {
                foreach (var grupoDTO in usuarioDTO.Grupos)
                {
                    UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
                    usuarioGrupo.UsuarioId = usuario.UsuarioId;
                    usuarioGrupo.GrupoId = grupoDTO.GrupoId;

                    // adiciona o grupo ao usuário
                    usuario.AddUsuarioGrupo(usuarioGrupo);
                }
            }

            ValidationResult result = new UsuarioValidator().Validate(usuario);

            if (result.IsValid)
            {
                if (usuario.UsuarioId == 0)
                    _usuarioRepository.Adicionar(usuario);
                else
                    _usuarioRepository.Atualizar(usuario);
            }
            else
            {
                usuarioDTO.ValidationErrors = result.Errors;
            }

            return result;
        }

        public ValidationResult SalvarPermissaoUsuario(UsuarioDTO usuarioDTO)
        {
            Usuario usuario;
            usuario = _usuarioRepository.ObterPorId(usuarioDTO.UsuarioId);
            string[] gruposIDs = null;
            string[] permissoesIDs = null;

            _usuarioRepository.DeleteUsuarioGrupos(usuario.UsuarioId);
            if (!String.IsNullOrEmpty(usuarioDTO.GruposIDs))
            {
                gruposIDs = usuarioDTO.GruposIDs.Split(',');

                #region Grupos do usuário
                if (gruposIDs.Length > 0)
                {

                    for (int idx = 0; idx < gruposIDs.Length; idx++)
                    {
                        int idGrupo = int.Parse(gruposIDs[idx]);
                        UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
                        usuarioGrupo.UsuarioId = usuario.UsuarioId;
                        usuarioGrupo.GrupoId = idGrupo;

                        // adiciona o grupo ao usuário
                        usuario.AddUsuarioGrupo(usuarioGrupo);
                    }
                }
                #endregion
            }

            _usuarioRepository.DeleteUsuarioPermissoes(usuario.UsuarioId);
            if (!String.IsNullOrEmpty(usuarioDTO.PermissoesIDs))
            {
                permissoesIDs = usuarioDTO.PermissoesIDs.Split(',');

                #region Permissões do usuário
                if (permissoesIDs.Length > 0)
                {
                    for (int idx = 0; idx < permissoesIDs.Length; idx++)
                    {
                        int idPermissao = int.Parse(permissoesIDs[idx]);
                        PermissaoUsuario permissaoUsuario = new PermissaoUsuario();
                        permissaoUsuario.UsuarioId = usuario.UsuarioId;
                        permissaoUsuario.PermissaoId = idPermissao;

                        // adiciona a permissao ao usuário
                        usuario.AddPermissaoUsuario(permissaoUsuario);
                    }
                }
                #endregion
            }

            ValidationResult result = new UsuarioValidator().Validate(usuario);

            if (result.IsValid)
            {
                _usuarioRepository.Atualizar(usuario);
            }
            else
            {
                usuarioDTO.ValidationErrors = result.Errors;
            }

            return result;
        }
    }
}
