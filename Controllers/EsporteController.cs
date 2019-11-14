using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Esporte>> Get(int id)
        {
            var esporte = await repository.FindEsporteAsync(id);

            if (esporte != null)
                return esporte;

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<Esporte>>> Get()
        {
            var esportes = await repository.GetEsportesAsync();

            if (esportes != null)
                return esportes;

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Esporte esporte)
        {
            if (await repository.CreateEsporteAsync(esporte))
                return RedirectToAction("Get", new { id = esporte.Id });

            return StatusCode(500, new
            {
                message = "Não foi possível registrar o esporte."
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Esporte esporte)
        {
            if (await repository.UpdateEsporteAsync(id, esporte))
                return RedirectToAction("Get", new { id = esporte.Id });

            return StatusCode(500, new
            {
                message = "Não foi possível atualizar o esporte."
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await repository.DeleteEsporteAsync(id)) 
            {
                return Ok(new
                {
                    message = "Esporte excluído com sucesso !"
                });
            }

            return NotFound();
        }

    }
}