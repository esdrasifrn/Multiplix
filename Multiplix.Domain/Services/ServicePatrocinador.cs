using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Enums;
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
        private readonly ICidadeRepository _cidadeRepository;
        private readonly ICompraRepository _compraRepository;
        private readonly IBonusRepository _bonusRepository;

        public ServicePatrocinador(IPatrocinadorRepository patrocinadorRepository, 
            IUsuarioRepository usuarioRepository, IBancoRepository bancoRepository, 
            IPlanoAssinaturaRepository planoAssinatura,
            ICidadeRepository cidadeRepository, 
            ICompraRepository compraRepository,
            IBonusRepository bonusRepository)
        {
            _patrocinadorRepository = patrocinadorRepository;
            _usuarioRepository = usuarioRepository;
            _bancoRepository = bancoRepository;
            _planoAssinatura = planoAssinatura;
            _cidadeRepository = cidadeRepository;
            _compraRepository = compraRepository;
            _bonusRepository = bonusRepository;
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

        public void GerarBonus(int associadoId)
        {
            int? patrocinadorIdPai1 = null;
            int? patrocinadorIdPai2 = null;
            int? patrocinadorIdPai3 = null;

            for (int i = 1; i <= 3; i++)
            {
                if (i == 1) // gera o primeiro bonus para o pai
                {
                    patrocinadorIdPai1 = _patrocinadorRepository.ObterPorId(associadoId).PatrocinadorId.Value;
                    if (patrocinadorIdPai1 != null && patrocinadorIdPai1 != 1)
                    {
                        SalvarBonus(patrocinadorIdPai1.Value, associadoId, 6);
                    }
                    
                }
                else if (i == 2)
                {
                    patrocinadorIdPai2 = _patrocinadorRepository.ObterPorId(patrocinadorIdPai1.Value).PatrocinadorId.Value;
                    if (patrocinadorIdPai2 != null && patrocinadorIdPai2 != 1)
                    {
                        SalvarBonus(patrocinadorIdPai2.Value, associadoId, 3);
                    }
                }
                else if (i == 3)
                {
                    if (patrocinadorIdPai3 != null && patrocinadorIdPai3 != 1)
                    {
                        patrocinadorIdPai3 = _patrocinadorRepository.ObterPorId(patrocinadorIdPai2.Value).PatrocinadorId.Value;
                        SalvarBonus(patrocinadorIdPai3.Value, associadoId, 1);
                    }                    
                }
            }           
        }

        public void SalvarBonus(int? associadoDonoId, int associadoGeradorId, float valor)
        {
            Bonus bonus = new Bonus(
                valor: valor,
                dataCadastro: DateTime.Now,
                dono:_patrocinadorRepository.ObterPorId(associadoDonoId.Value),
                gerador: _patrocinadorRepository.ObterPorId(associadoGeradorId)
                );

            _bonusRepository.Adicionar(bonus);
        }

        public float GetBonusPorMes(int mes, int associadoId)
        {
            var associado = _patrocinadorRepository.ObterPorId(associadoId);

            var somaBonus = associado.Bonus.Where(x => x.DataCadastro.Date.Month == mes).Sum(x => x.Valor);
            
            return somaBonus;
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
                    liberado: usuarioDTO.Liberado,
                    tipoUsuario: (int)ETipoUsuario.ASSOCIADO
                );
                               

                //associa o associado ao patrocinador
                associado = new Associado(
                    usuario: usuario,
                    patrocinadorId: usuarioDTO.PatrocinadorId, // patrocinador raiz multiplix
                    rua: usuarioDTO.Rua,
                    numero: usuarioDTO.Numero,
                    cep: usuarioDTO.CEP,
                    cidade: _cidadeRepository.ObterPorId(usuarioDTO.CidadeId),
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

                //Na edição só atualiza a senha se for digitada alguma
                if (!String.IsNullOrEmpty(usuarioDTO.Senha))
                {
                    associado.Usuario.Senha = usuarioDTO.Senha;
                }                

                associado.Usuario.Nome = usuarioDTO.Nome;
                associado.Usuario.Celular = usuarioDTO.Celular;
                associado.Usuario.Email = usuarioDTO.Email;
                associado.Usuario.Liberado = usuarioDTO.Liberado;

                associado.Rua = usuarioDTO.Rua;
                associado.Numero = usuarioDTO.Numero;
                associado.CEP = usuarioDTO.CEP;
                associado.Cidade = _cidadeRepository.ObterPorId(usuarioDTO.CidadeId);
                associado.Bairro = usuarioDTO.Bairro;
                associado.Complemento = usuarioDTO.Complemento;               
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

            if ((!string.IsNullOrEmpty(associado.CPF)))
            {
                if (_patrocinadorRepository.CPFJaExiste(associado.CPF) && !atualizando)
                {
                    result.Errors.Add(new ValidationFailure("CPF", "CPF já existe"));
                }
            }

            if ((string.IsNullOrEmpty(associado.CPF)))
            {
                result.Errors.Add(new ValidationFailure("CPF", "CPF é obrigatório"));
            }

            if ((!string.IsNullOrEmpty(associado.CPF)))
            {
                if (!ValidaCPF.IsCpf(associado.CPF))
                {
                    result.Errors.Add(new ValidationFailure("CPF", "CPF inválido"));
                }
            }

            //se estou inserindo a senha é obrigatória
            if ((!atualizando) && (string.IsNullOrEmpty(associado.Usuario.Senha)))
            {
                result.Errors.Add(new ValidationFailure("Senha", "A Senha é obrigatória."));
            }

            if (associado.Cidade == null)
            {
                result.Errors.Add(new ValidationFailure("Cidade", "Campo obrigatório."));
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

            if (totalGeralPontos <= 1199.99)
            {
                percentagemAtual = 0;
            }
            else if (totalGeralPontos >= 1200 && totalGeralPontos <= 6199.9)
            {
                percentagemAtual = 3;
            }
            else if (totalGeralPontos >= 6200 && totalGeralPontos <= 31199.9)
            {
                percentagemAtual = 6;
            }
            else if (totalGeralPontos >= 31200 && totalGeralPontos <= 156199.9)
            {
                percentagemAtual = 9;
            }
            else if (totalGeralPontos >= 156200 && totalGeralPontos <= 781199.9)
            {
                percentagemAtual = 12;
            }
            else if (totalGeralPontos >= 781200 && totalGeralPontos <= 3906019)
            {
                percentagemAtual = 15;
            }
            else if (totalGeralPontos >= 3906020 && totalGeralPontos <= 19530119)
            {
                percentagemAtual = 18;
            }
            else if (totalGeralPontos >= 19530120)
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
            Entrada entrada;


            //cria o usuário do patrocinador
            usuario = new Usuario(
                    login: usuarioDTO.Login,
                    senha: usuarioDTO.Senha,
                    nome: usuarioDTO.Nome,
                    celular: usuarioDTO.Celular,
                    email: usuarioDTO.Email,
                    liberado: usuarioDTO.Liberado,
                    tipoUsuario: (int)ETipoUsuario.ASSOCIADO
                );


                //associa o associado ao patrocinador
                associado = new Associado(
                    usuario: usuario,
                    patrocinadorId: usuarioDTO.PatrocinadorId, // patrocinador raiz multiplix
                    rua: usuarioDTO.Rua,
                    numero: usuarioDTO.Numero,
                    cep: usuarioDTO.CEP,
                    cidade: _cidadeRepository.ObterPorId(usuarioDTO.CidadeId),
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


            entrada = new Entrada(
                descricao: "Adesão de novo afiliado",
                data: DateTime.Now,
                associado: associado,
                status: (int)EStatusMovimentacao.PENDENTE,
                valor: _planoAssinatura.ObterPorId(usuarioDTO.PlanoAssinaturaId).Valor
                );

            associado.AddEntrada(entrada);

            UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
            usuarioGrupo.UsuarioId = associado.Usuario.UsuarioId;
            usuarioGrupo.GrupoId = 1;

            // adiciona o grupo ao usuário via patrocinador
            associado.Usuario.AddUsuarioGrupo(usuarioGrupo);

            associado.Id = 0;    

            ValidationResult result = new UsuarioValidator(_usuarioRepository, atualizando:false).Validate(associado.Usuario);

          
            if (associado.PlanoAssinatura == null)
            {
                result.Errors.Add(new ValidationFailure("PlanoAssinatura", "Campo obrigatório."));
            }

            if ((!string.IsNullOrEmpty(associado.CPF)))
            {
                if (_patrocinadorRepository.CPFJaExiste(associado.CPF))
                {
                    result.Errors.Add(new ValidationFailure("CPF", "CPF já existe"));
                }
            }

            if ((string.IsNullOrEmpty(associado.CPF)))
            {
                result.Errors.Add(new ValidationFailure("CPF", "CPF é obrigatório"));
            }

            if ((!string.IsNullOrEmpty(associado.CPF)))
            {
                if (!ValidaCPF.IsCpf(associado.CPF))
                {
                    result.Errors.Add(new ValidationFailure("CPF", "CPF inválido"));
                }
            }

            if (result.IsValid)
            {
                if (associado.Id == 0)
                {
                    _patrocinadorRepository.Adicionar(associado);
                    associado.IdCarteira = associado.GenerateCarteiraPatrocinador();
                    _patrocinadorRepository.Atualizar(associado);

                    GerarBonus(associado.Id);
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

        public List<Associado> GetAssociadosAniversariantes(int mesNascimento)
        {
            List<Associado> associados = _patrocinadorRepository.Buscar(x => x.Nascimento.Date.Month == mesNascimento).ToList();

            return associados;
        }

        public List<DiasSemComprarDTO> GetDiasSemConsumo(int dias)
        {
            List<Associado> associados = _patrocinadorRepository.ObterTodos().ToList();
            List<DiasSemComprarDTO> listadiasSemComprarDTO = new List<DiasSemComprarDTO>();
            foreach (var associado in associados)
            {
                DiasSemComprarDTO semcomprarDTO = new DiasSemComprarDTO();

                Compra ultimaCompra = associado.Compras.LastOrDefault();
                
                if (ultimaCompra != null)
                {
                    semcomprarDTO.DiasSemComprar = (int)Math.Abs(Math.Round(DateTime.Now.Subtract(ultimaCompra.Data).TotalDays));
                    semcomprarDTO.NomeAssociado = associado.Usuario.Nome;
                    semcomprarDTO.UsuarioId = associado.Usuario.UsuarioId;
                    semcomprarDTO.DataUltimaCompra = ultimaCompra.Data;

                    if (semcomprarDTO.DiasSemComprar >= dias)
                    {
                        listadiasSemComprarDTO.Add(semcomprarDTO);
                    }
                }
               
            }

            return listadiasSemComprarDTO;
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
