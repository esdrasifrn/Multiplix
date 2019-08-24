using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Interfaces.Repository
{
    public interface IGrupoRepository : IRepositoryBase<Grupo>
    {
        void DeleteGrupoUsuarios(int grupoId);
        void DeletePermissoesGrupo(int grupoId);
    }
}
