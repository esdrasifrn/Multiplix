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
    }
}
