using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplix.Domain.Entities
{
    public class Patrocinador
    {   
        /// <summary>
        /// Um patrocinador pode ter vários patrocinados (auto-relacionamento)
        /// </summary>
        private IList<Patrocinador> _patrocinados;

        public Patrocinador(Usuario usuario, int? patrocinadorId)
        {
            Usuario = usuario;
            PatrocinadorId = patrocinadorId;
            IdCarteira = $"{DateTime.Now.Year.ToString()}{PatrocinadorId:00000000}";
            _patrocinados = new List<Patrocinador>();
        }

        public Patrocinador()
        {
            _patrocinados = new List<Patrocinador>();
        }

        public int Id { get; set; }
        public string IdCarteira { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int? PatrocinadorId { get; set; }
        public virtual ICollection<Patrocinador> Patrocinados { get => _patrocinados; set { } }             

        public void AddPatrocinado(Patrocinador patrocinador)
        {
            _patrocinados.Add(patrocinador);
        }

        public string GenerateCarteiraPatrocinador()
        {
            return $"{DateTime.Now.Year.ToString()}{PatrocinadorId:00000000}";
        }
    }
}
