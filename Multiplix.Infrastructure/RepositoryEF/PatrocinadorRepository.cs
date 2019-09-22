﻿using Microsoft.Extensions.Configuration;
using Multiplix.Domain.Entities;
using Multiplix.Domain.Interfaces.Repository;
using Multiplix.Infrastructure.Data;
using System.Linq;

namespace Multiplix.Infrastructure.RepositoryEF
{
    public class PatrocinadorRepository : EFRepository<Associado>, IPatrocinadorRepository
    {
        public PatrocinadorRepository(MultiplixContext multiplixContext, IConfiguration configuration) : base(multiplixContext, configuration)
        {
              
        }      
    }
}
