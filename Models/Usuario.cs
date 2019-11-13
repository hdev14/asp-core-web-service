using System.Collections.Generic;

namespace web_service.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Pelada> Peladas { get; set; }
    }
}