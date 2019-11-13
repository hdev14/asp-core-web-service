using System.Collections.Generic;

namespace web_service.Models
{
    public class Time
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int PeladaId { get; set; }
        public Pelada Pelada { get; set; }
        public List<Atleta> Atletas { get; set; }
    }
}