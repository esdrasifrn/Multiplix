using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class DiasSemComprarDTO
    {
        public String NomeAssociado { get; set; }
        public int DiasSemComprar { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataUltimaCompra { get; set; }

    }
}
