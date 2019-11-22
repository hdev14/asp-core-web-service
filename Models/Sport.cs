using System.Collections.Generic;

namespace web_service.Models
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberPlayers { get; set; }
        public int NumberPlayersTeam { get; set; }
        public virtual List<Pelada> Peladas { get; set; }
    }
}