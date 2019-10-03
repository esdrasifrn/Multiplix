using Microsoft.Extensions.Configuration;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Infrastructure.Data;
using System.Linq;

namespace Multiplix.Infrastructure.RepositoryEF
{
    public class ParceiroRepository : EFRepository<Parceiro>, IParceiroRepository
    {
        public ParceiroRepository(MultiplixContext multiplixContext, IConfiguration configuration) : base(multiplixContext, configuration)
        {
              
        }

        public void DeleteProdutosParceiro(int parceiroId)
        {
            var parceiro = (from u in _dbContext.Parceiros where u.ParceiroId == parceiroId select u).FirstOrDefault();

            var produtosParceiro = parceiro.ParceiroProdutos;

            foreach (var produtoParceiro in produtosParceiro)
            {
                _dbContext.parceiroProdutos.Remove(produtoParceiro);
            }

            _dbContext.SaveChanges();
        }
    }
}
