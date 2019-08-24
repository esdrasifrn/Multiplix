using Microsoft.Extensions.Configuration;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Enums;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Infrastructure.Data;

namespace Multiplix.Infrastructure.RepositoryEF
{
    public class PermissaoRepository : EFRepository<Permissao>, IPermissaoRepository
    {
        public PermissaoRepository(MultiplixContext multiplixContext, IConfiguration configuration) : base(multiplixContext, configuration)
        {

        }

        public void RemoverPermissoesOrfas()
        {
            // remove permissões que deixaram de existir/órfãs
            foreach (var permissaoBanco in _dbContext.Permissoes)
            {
                if (EPermissao.Get(permissaoBanco.NomeId) == null)
                {
                    _dbContext.Permissoes.Remove(permissaoBanco);
                }
            }

            _dbContext.SaveChanges();
        }
    }
}
