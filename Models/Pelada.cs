using System.Collections.Generic;

namespace web_service.Models
{
    public class Pelada
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public string Place { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }

        public virtual List<Team> Teams { get; set; }

    }
}