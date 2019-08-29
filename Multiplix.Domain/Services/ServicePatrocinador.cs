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

namespace Multiplix.Domain.Services
{
    public class ServicePatrocinador : IServicePatrocinador
    {
        private const int PatrocinadorRaiz = 1;
        private readonly IPatrocinadorRepository _patrocinadorRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ServicePatrocinador(IPatrocinadorRepository patrocinadorRepository, IUsuarioRepository usuarioRepository)
        {
            _patrocinadorRepository = patrocinadorRepository;
            _usuarioRepository = usuarioRepository;
        }

        public Patrocinador Adicionar(Patrocinador entity)
        {
            return _patrocinadorRepository.Adicionar(entity);
        }

        public void Atualizar(Patrocinador entity)
        {
            _patrocinadorRepository.Atualizar(entity);
        }

        public IEnumerable<Patrocinador> Buscar(Expression<Func<Patrocinador, bool>> predicado)
        {
            return _patrocinadorRepository.Buscar(predicado);
        }

        public Patrocinador BuscarEntidade(Expression<Func<Patrocinador, bool>> predicado)
        {
            return _patrocinadorRepository.BuscarEntidade(predicado);
        }

        public Patrocinador ObterPorId(int id)
        {
            return _patrocinadorRepository.ObterPorId(id);
        }

        public IEnumerable<Patrocinador> ObterTodos()
        {
            return _patrocinadorRepository.ObterTodos();
        }

        public IEnumerable<Patrocinador> ObterTodosPaginado(int skip, int take)
        {
            return _patrocinadorRepository.ObterTodosPaginado(skip, take);
        }

        public void Remover(Patrocinador entity)
        {
            _patrocinadorRepository.Remover(entity);
        }

        /// <summary>
        /// Patrocinadores que serão cadastrados via web, sem um convite mobile. esse patrocinadores terão como patrocinador pai a multiplix
        /// que é o patrocinador raiz
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        public ValidationResult SalvarPatrocinadorSemConvite(UsuarioDTO usuarioDTO)
        {
            // usuário
            Usuario usuario;
            Patrocinador patrocinador;

            if (usuarioDTO.PatrocinadorId == 0)
            {
                //cria o usuário do patrocinador
                usuario = new Usuario(
                    login: usuarioDTO.Login,
                    senha: usuarioDTO.Senha,
                    nome: usuarioDTO.Nome,
                    celular: usuarioDTO.Celular,
                    email: usuarioDTO.Email,
                    liberado: usuarioDTO.Liberado
                );

                //associa o usuário ao patrocinador
                patrocinador = new Patrocinador(
                    usuario: usuario,
                    patrocinadorId: PatrocinadorRaiz // patrocinador raiz multiplix                   
                    ) ; 

                patrocinador.Id = 0;
            }
            else
            {
                patrocinador = _patrocinadorRepository.ObterPorId(usuarioDTO.PatrocinadorId);

                patrocinador.Usuario.Login = usuarioDTO.Login;
                patrocinador.Usuario.Senha = usuarioDTO.Senha;
                patrocinador.Usuario.Nome = usuarioDTO.Nome;
                patrocinador.Usuario.Celular = usuarioDTO.Celular;
                patrocinador.Usuario.Email = usuarioDTO.Email;
                patrocinador.Usuario.Liberado = usuarioDTO.Liberado;
            }

            // grupos do usuário do patrocinador
            if (usuarioDTO.PatrocinadorId > 0)
                _usuarioRepository.DeleteUsuarioGrupos(patrocinador.Usuario.UsuarioId);

            if (usuarioDTO.Grupos.Count > 0)
            {
                foreach (var grupoDTO in usuarioDTO.Grupos)
                {
                    UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
                    usuarioGrupo.UsuarioId = patrocinador.Usuario.UsuarioId;
                    usuarioGrupo.GrupoId = grupoDTO.GrupoId;

                    // adiciona o grupo ao usuário via patrocinador
                    patrocinador.Usuario.AddUsuarioGrupo(usuarioGrupo);
                }
            }

            ValidationResult result = new UsuarioValidator().Validate(patrocinador.Usuario);

            if (result.IsValid)
            {
                if (patrocinador.Id == 0)
                    _patrocinadorRepository.Adicionar(patrocinador);
                else
                    _patrocinadorRepository.Atualizar(patrocinador);
            }
            else
            {
                usuarioDTO.ValidationErrors = result.Errors;
            }

            return result;
        }
    }
}
