using web_service.database;

namespace web_service.Repositories
{
    public class EsporteRepository
    {
        private readonly WebServiceContext context;
        public EsporteRepository(WebServiceContext context)
        {
            this.context = context;
        }
    }
}