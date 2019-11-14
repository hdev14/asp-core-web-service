using System.Collections.Generic;

namespace web_service.Models
{
    public class Pelada
    {
        public int Id { get; set; }
        public string Titulo { get; set; } 
        public string Descricao { get; set; }
        public string Local { get; set; }
        
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int EsporteId { get; set; }
        public Esporte Esporte { get; set; }
        
        public List<Time> Times { get; set; }

    }
}