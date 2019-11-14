using Microsoft.AspNetCore.Mvc;
using web_service.Repositories;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EsporteController : ControllerBase
    {
        private readonly EsporteRepository repository;
        
        public EsporteController(EsporteRepository repository)
        {
            this.repository = repository;
        }
    }
}