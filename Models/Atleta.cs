namespace web_service.Models
{
    public class Atleta : IModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int TimeId { get; set; }
        public virtual Time Time { get; set; }
    }
}