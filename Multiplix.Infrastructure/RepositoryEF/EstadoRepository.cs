
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Infrastructure.Data;
using System;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Multiplix.Infrastructure.RepositoryEF
{
    public class EstadoRepository : EFRepository<Estado>, IEstadoRepository
    {
        public EstadoRepository(MultiplixContext multiplixContext, IConfiguration configuration) : base(multiplixContext, configuration)
        {

        }       
    }
}
