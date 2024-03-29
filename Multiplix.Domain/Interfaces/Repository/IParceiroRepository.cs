﻿using Multiplix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Interfaces.Repository
{
    public interface IParceiroRepository : IRepositoryBase<Parceiro>
    {
        void DeleteProdutosParceiro(int parceiroId);
    }
}
