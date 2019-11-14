using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
using web_service.Repositories;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtletaController : ControllerBase
    {
        private readonly AtletaRepository repository;
        
        public AtletaController(AtletaRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Atleta>> Get(int id)
        {
            var atleta = await repository.FindAtletaAsync(id);
            
            if (atleta != null)
                return atleta;

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<Atleta>>> Get()
        {
            var atletas = await repository.GetAtletasAsync();
            if (atletas != null)
                return atletas;

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Atleta atleta)
        {
            if (await repository.CreateAtletaAsync(atleta))
                return RedirectToAction("Get", new {id = atleta.Id });

            return StatusCode(500, new {
                message = "Não foi possível registrar o atleta."
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Atleta atleta)
        {
            if (await repository.UpdateAtletaAsync(id, atleta))
                return NoContent();

            return StatusCode(500, new {
                message = "Não foi possível atualizar o atleta."
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await repository.DeleteAtletaAsync(id))
            {
                return Ok(new {
                    message = "Atleta excluído com sucesso !"
                });
            }

            return NotFound();
        }
    }
}