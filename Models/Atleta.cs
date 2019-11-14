namespace web_service.Models
{
    public class Atleta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int TimeId { get; set; }
        public virtual Time Time { get; set; }
    }
}