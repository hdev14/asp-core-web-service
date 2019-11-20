using System.Collections.Generic;

namespace web_service.Models
{
    public class Pelada : IModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } 
        public string Descricao { get; set; }
        public string Local { get; set; }
        
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int EsporteId { get; set; }
        public virtual Esporte Esporte { get; set; }

        public virtual List<Time> Times { get; set; }

    }
}