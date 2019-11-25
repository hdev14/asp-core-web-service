using System.Collections.Generic;

namespace web_service.ModelsView
{
    public class TeamAthletesView
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public List<string> AthleteNames { get; set; }
    }
}