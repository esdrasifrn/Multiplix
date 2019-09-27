using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.DTOs
{
    public class RamoAtividadeDTO
    {
        public int RamoAtividadeId { get; set; }
        public string RamoAtividadeNome { get; set; }          
        public IList<ValidationFailure> ValidationErrors { get; set; } = new List<ValidationFailure>();
 
    }
}
