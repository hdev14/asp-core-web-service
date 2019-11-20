using System.Collections.Generic;

namespace web_service.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual List<Pelada> Peladas { get; set; }
    }
}