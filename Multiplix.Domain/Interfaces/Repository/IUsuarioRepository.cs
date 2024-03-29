﻿using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        void DeleteUsuarioGrupos(int usuarioId);
        void DeleteUsuarioPermissoes(int usuarioId);

        //bool CPFJaExiste(string cpf);
        bool LoginJaExiste(string login);
        bool EmailJaExiste(string email);
    }
}
