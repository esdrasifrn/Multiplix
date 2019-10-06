﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class ProdutoDTO
    {
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public float PontosPorRealProduto { get; set; }
        public float ValorProduto { get; set; }
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}