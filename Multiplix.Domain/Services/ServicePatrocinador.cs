using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Domain.Interfaces.Services;
using Multiplix.Domain.Validations;
using Multiplix.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Multiplix.Domain.Services
{
    public class ServicePatrocinador : IServicePatrocinador
    {
        private const int PatrocinadorRaiz = 1;
        private readonly IPatrocinadorRepository _patrocinadorRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IBancoRepository _bancoRepository;
        private readonly IPlanoAssinaturaRepository _planoAssinatura;

        public ServicePatrocinador(IPatrocinadorRepository patrocinadorRepository, 
            IUsuarioRepository usuarioRepository, IBancoRepository bancoRepository, 
            IPlanoAssinaturaRepository planoAssinatura)
        {
            _patrocinadorRepository = patrocinadorRepository;
            _usuarioRepository = usuarioRepository;
            _bancoRepository = bancoRepository;
            _planoAssinatura = planoAssinatura;
        }

        public Associado Adicionar(Associado entity)
        {
            return _patrocinadorRepository.Adicionar(entity);
        }

        public void Atualizar(Associado entity)
        {
            _patrocinadorRepository.Atualizar(entity);
        }

        public IEnumerable<Associado> Buscar(Expression<Func<Associado, bool>> predicado)
        {
            return _patrocinadorRepository.Buscar(predicado);
        }

        public Associado BuscarEntidade(Expression<Func<Associado, bool>> predicado)
        {
            return _patrocinadorRepository.BuscarEntidade(predicado);
        }

        public Associado ObterPorId(int id)
        {
            return _patrocinadorRepository.ObterPorId(id);
        }

        public IEnumerable<Associado> ObterTodos()
        {
            return _patrocinadorRepository.ObterTodos();
        }

        public IEnumerable<Associado> ObterTodosPaginado(int skip, int take)
        {
            return _patrocinadorRepository.ObterTodosPaginado(skip, take);
        }

        public List<Associado> GetRedeAssociado(int associadoId)
        {
            var associadosPatrocinados = new List<Associado>();
            var associados_patrocinados_diretos = _patrocinadorRepository.ObterPorId(associadoId).Patrocinados.ToList();
            foreach (var item in associados_patrocinados_diretos)
            {
                associadosPatrocinados.Add(item);
               // if (item.Id != 1)
               // {
                    associadosPatrocinados = associadosPatrocinados.Union(GetRedeAssociado(item.Id)).ToList();
                //}

            }
            var ids = associadosPatrocinados;
            return associadosPatrocinados;
        }

        public void Remover(Associado entity)
        {
            _patrocinadorRepository.Remover(entity);
        }

        /// <summary>
        /// Patrocinadores que serão cadastrados via web, sem um convite mobile. esse patrocinadores terão como patrocinador pai a multiplix
        /// que é o patrocinador raiz
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        public ValidationResult SalvarAssociadoSemConvite(UsuarioDTO usuarioDTO)
        {
            // usuário
            Usuario usuario;
            Associado associado;
            var atualizando = false;
           
            if (usuarioDTO.UsuarioId == 0)
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
                               

                //associa o associado ao patrocinador
                associado = new Associado(
                    usuario: usuario,
                    patrocinadorId: usuarioDTO.PatrocinadorId, // patrocinador raiz multiplix
                    rua: usuarioDTO.Rua,
                    numero: usuarioDTO.Numero,
                    cep: usuarioDTO.CEP,
                    cidade: usuarioDTO.Cidade,
                    bairro: usuarioDTO.Bairro,
                    complemento: usuarioDTO.Complemento,
                    estado: usuarioDTO.Estado,
                    nascimento: usuarioDTO.Nascimento,
                    sexo: usuarioDTO.Sexo,
                    cpf: usuarioDTO.CPF,
                    emailAlternativo: usuarioDTO.EmailAlternativo,
                    banco: _bancoRepository.ObterPorId(usuarioDTO.BancoId),
                    tipoConta: usuarioDTO.TipoConta,
                    agengia: usuarioDTO.Agencia,
                    conta: usuarioDTO.Conta,
                    nivel: usuarioDTO.Nivel + 1, // multiplys é o nível zero e seus convidados serão 0 + 1, e assim sucessivamente
                    planoAssinatura: _planoAssinatura.ObterPorId(usuarioDTO.PlanoAssinaturaId)

                    ) ;


                UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
                usuarioGrupo.UsuarioId = associado.Usuario.UsuarioId;
                usuarioGrupo.GrupoId = 1;

                // adiciona o grupo ao usuário via patrocinador
                associado.Usuario.AddUsuarioGrupo(usuarioGrupo);

                associado.Id = 0;
            }
            else
            {
                atualizando = true;
                associado = _patrocinadorRepository.Buscar(x => x.UsuarioId == usuarioDTO.UsuarioId).FirstOrDefault();

                associado.Usuario.Login = usuarioDTO.Login;
                associado.Usuario.Senha = usuarioDTO.Senha;
                associado.Usuario.Nome = usuarioDTO.Nome;
                associado.Usuario.Celular = usuarioDTO.Celular;
                associado.Usuario.Email = usuarioDTO.Email;
                associado.Usuario.Liberado = usuarioDTO.Liberado;

                associado.Rua = usuarioDTO.Rua;
                associado.Numero = usuarioDTO.Numero;
                associado.CEP = usuarioDTO.CEP;
                associado.Cidade = usuarioDTO.Cidade;
                associado.Bairro = usuarioDTO.Bairro;
                associado.Complemento = usuarioDTO.Complemento;
                associado.Estado = usuarioDTO.Estado;
                associado.Nascimento = usuarioDTO.Nascimento;
                associado.Sexo = usuarioDTO.Sexo;
                associado.CPF = usuarioDTO.CPF;
                associado.EmailAlternativo = usuarioDTO.EmailAlternativo;
                associado.Banco = _bancoRepository.ObterPorId(usuarioDTO.BancoId);
                associado.TipoConta = usuarioDTO.TipoConta;
                associado.Agencia = usuarioDTO.Agencia;
                associado.Conta = usuarioDTO.Conta;
                associado.PlanoAssinatura = _planoAssinatura.ObterPorId(usuarioDTO.PlanoAssinaturaId);
            }

            // grupos do usuário do patrocinador
            //if (usuarioDTO.Grupos.Count > 0)
            //    _usuarioRepository.DeleteUsuarioGrupos(associado.Usuario.UsuarioId);

            //if (usuarioDTO.Grupos.Count > 0)
            //{
            //    foreach (var grupoDTO in usuarioDTO.Grupos)
            //    {
            //        UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
            //        usuarioGrupo.UsuarioId = associado.Usuario.UsuarioId;
            //        usuarioGrupo.GrupoId = grupoDTO.GrupoId;

            //        // adiciona o grupo ao usuário via patrocinador
            //        associado.Usuario.AddUsuarioGrupo(usuarioGrupo);
            //    }
            //}

            ValidationResult result = new UsuarioValidator(_usuarioRepository, atualizando).Validate(associado.Usuario);

            if (associado.Banco == null)
            {
                result.Errors.Add(new ValidationFailure("Banco", "Campo obrigatório."));
            }

            if (associado.PlanoAssinatura == null)
            {
                result.Errors.Add(new ValidationFailure("PlanoAssinatura", "Campo obrigatório."));
            }

            if (associado.PatrocinadorId == 0)
            {
                result.Errors.Add(new ValidationFailure("Associado", "O patrocinador é obrigatório."));
            }

            if (_patrocinadorRepository.CPFJaExiste(associado.CPF) && !atualizando)
            {
                result.Errors.Add(new ValidationFailure("CPF", "CPF já existe"));
            }

            if ((string.IsNullOrEmpty(associado.CPF)))
            {
                result.Errors.Add(new ValidationFailure("CPF", "CPF é obrigatório"));
            }

            if (!ValidaCPF.IsCpf(associado.CPF))
            {
                result.Errors.Add(new ValidationFailure("CPF", "CPF inválido"));
            }

            if (result.IsValid)
            {
                if (associado.Id == 0)
                {
                    _patrocinadorRepository.Adicionar(associado);
                    associado.IdCarteira = associado.GenerateCarteiraPatrocinador();
                    _patrocinadorRepository.Atualizar(associado);
                }

                else
                {
                    _patrocinadorRepository.Atualizar(associado);
                }
                   
            }
            else
            {
                usuarioDTO.ValidationErrors = result.Errors;
            }

            return result;
        }

        public int GetPercentagem(float totalGeralPontos)
        {
            int percentagemAtual = 0;

            if (totalGeralPontos <= 599.99)
            {
                percentagemAtual = 0;
            }
            else if (totalGeralPontos >= 600 && totalGeralPontos <= 3099.9)
            {
                percentagemAtual = 3;
            }
            else if (totalGeralPontos >= 3100 && totalGeralPontos <= 15599.9)
            {
                percentagemAtual = 6;
            }
            else if (totalGeralPontos >= 15600 && totalGeralPontos <= 78099.9)
            {
                percentagemAtual = 9;
            }
            else if (totalGeralPontos >= 78100 && totalGeralPontos <= 390599.9)
            {
                percentagemAtual = 12;
            }
            else if (totalGeralPontos >= 390600 && totalGeralPontos <= 1953009)
            {
                percentagemAtual = 15;
            }
            else if (totalGeralPontos >= 1953010 && totalGeralPontos <= 9765059)
            {
                percentagemAtual = 18;
            }
            else if (totalGeralPontos >= 9765060)
            {
                percentagemAtual = 21;
            }

            return percentagemAtual;
        }

        public float GetPontosIndividuaisPorMes(int mes, int associadoId)
        {
           var associado =  _patrocinadorRepository.ObterPorId(associadoId);

            return associado.Compras.Where(x => x.Data.Month == mes).Sum(x => x.Pontos);
        }

        public float GetPontosRedePorMes(int mes, int associadoId)
        {
           var redeAssociado = GetRedeAssociado(associadoId);
           var pontosRede = redeAssociado.SelectMany(x => x.Compras).Where(x => x.Data.Month == mes).Sum(x => x.Pontos);

            return pontosRede;
        }

        public float GetGanhosIndividual(int mes, int associadoId)
        {
            float pontosIndividual = GetPontosIndividuaisPorMes(mes, associadoId);
            float pontosRede = GetPontosRedePorMes(mes, associadoId);
            float pontosTotal = pontosIndividual + pontosRede;
            float perncentualAssociado = GetPercentagem(pontosTotal);

            float valor_a_receber_individualmente = (pontosIndividual * perncentualAssociado)/100;

            return valor_a_receber_individualmente;

        }

        public float GetGanhosRede(int mes, int associadoId)
        {
            float totalGeralRede = 0;
            float totalParcialRede = 0;

            var pontosTotais = GetPontosTotal(mes, associadoId);

            var percentagemAssociadoPatrocinador = GetPercentagem(pontosTotais);
            var percentagemAssociadoPatrocinado = 0;

            var associado = ObterPorId(associadoId);

            //itera sobre todos os associados diretos (patrocinados)
            foreach (var patrocinado in associado.Patrocinados)
            {
                percentagemAssociadoPatrocinado = GetPercentagem(GetPontosTotal(mes, patrocinado.Id));
                totalParcialRede = ((percentagemAssociadoPatrocinador - percentagemAssociadoPatrocinado) * (GetPontosTotal(mes, patrocinado.Id)))/100;
                totalGeralRede += totalParcialRede;
            }

            return totalGeralRede;
        }

        public float GetPontosTotal(int mes, int associadoId)
        {
            float pontosIndividual = GetPontosIndividuaisPorMes(mes, associadoId);
            float pontosRede = GetPontosRedePorMes(mes, associadoId);
            float pontosTotal = pontosIndividual + pontosRede;
            return pontosTotal;
        }

        public ValidationResult SalvarAssociadoComConvite(UsuarioDTO usuarioDTO)
        {
            // usuário
            Usuario usuario;
            Associado associado;

           
                //cria o usuário do patrocinador
                usuario = new Usuario(
                    login: usuarioDTO.Login,
                    senha: usuarioDTO.Senha,
                    nome: usuarioDTO.Nome,
                    celular: usuarioDTO.Celular,
                    email: usuarioDTO.Email,
                    liberado: usuarioDTO.Liberado
                );


                //associa o associado ao patrocinador
                associado = new Associado(
                    usuario: usuario,
                    patrocinadorId: usuarioDTO.PatrocinadorId, // patrocinador raiz multiplix
                    rua: usuarioDTO.Rua,
                    numero: usuarioDTO.Numero,
                    cep: usuarioDTO.CEP,
                    cidade: usuarioDTO.Cidade,
                    bairro: usuarioDTO.Bairro,
                    complemento: usuarioDTO.Complemento,
                    estado: usuarioDTO.Estado,
                    nascimento: usuarioDTO.Nascimento,
                    sexo: usuarioDTO.Sexo,
                    cpf: usuarioDTO.CPF,
                    emailAlternativo: usuarioDTO.EmailAlternativo,
                    banco: _bancoRepository.ObterPorId(usuarioDTO.BancoId),
                    tipoConta: usuarioDTO.TipoConta,
                    agengia: usuarioDTO.Agencia,
                    conta: usuarioDTO.Conta,
                    nivel: usuarioDTO.Nivel + 1, // convidado é o nível do patrocinador mais 1
                    planoAssinatura: _planoAssinatura.ObterPorId(usuarioDTO.PlanoAssinaturaId)

                    );

            UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
            usuarioGrupo.UsuarioId = associado.Usuario.UsuarioId;
            usuarioGrupo.GrupoId = 1;

            // adiciona o grupo ao usuário via patrocinador
            associado.Usuario.AddUsuarioGrupo(usuarioGrupo);

            associado.Id = 0;       

           

            ValidationResult result = new UsuarioValidator(_usuarioRepository, atualizando:false).Validate(associado.Usuario);

            if (associado.Banco == null)
            {
                result.Errors.Add(new ValidationFailure("Banco", "Campo obrigatório."));
            }

            if (associado.PlanoAssinatura == null)
            {
                result.Errors.Add(new ValidationFailure("PlanoAssinatura", "Campo obrigatório."));
            }

            if (_patrocinadorRepository.CPFJaExiste(associado.CPF))
            {
                result.Errors.Add(new ValidationFailure("CPF", "CPF já existe"));
            }

            if ((string.IsNullOrEmpty(associado.CPF)))
            {
                result.Errors.Add(new ValidationFailure("CPF", "CPF é obrigatório"));
            }

            if (!ValidaCPF.IsCpf(associado.CPF))
            {
                result.Errors.Add(new ValidationFailure("CPF", "CPF inválido"));
            }

            if (result.IsValid)
            {
                if (associado.Id == 0)
                {
                    _patrocinadorRepository.Adicionar(associado);
                    associado.IdCarteira = associado.GenerateCarteiraPatrocinador();
                    _patrocinadorRepository.Atualizar(associado);
                }

            }
            else
            {
                usuarioDTO.ValidationErrors = result.Errors;
            }

            return result;
        }

        public bool CPFJaExiste(string cpf)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Realiza a validação do CPF
        /// </summary>
        public static class ValidaCPF
        {
            public static bool IsCpf(string cpf)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }
        }
    }
}
