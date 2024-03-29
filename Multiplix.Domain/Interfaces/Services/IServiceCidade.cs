﻿using FluentValidation.Results;
using Multiplix.Domain.DTOs;
using Multiplix.Domain.Entities;


namespace Multiplix.Domain.Interfaces.Services
{
    public interface IServiceCidade : IServiceBase<Cidade>
    {
        ValidationResult Salvar(CidadeDTO cidadeDTO);      
    }
}
