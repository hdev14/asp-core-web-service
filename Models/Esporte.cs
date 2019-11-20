using System.Collections.Generic;

namespace web_service.Models
{
    public class Esporte : IModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int NumeroJogadores { get; set; }
        public int NumeroJogadoresTime { get; set; }
        public virtual List<Pelada> Pelada { get; set; }

    }
}