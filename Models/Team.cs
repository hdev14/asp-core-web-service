using System.Collections.Generic;

namespace web_service.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PeladaId { get; set; }
        public virtual Pelada Pelada { get; set; }
        public virtual List<Athlete> Athletes { get; set; }
    }
}