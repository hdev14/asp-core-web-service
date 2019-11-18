using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
using web_service.Repositories;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeladaController : ControllerBase
    {
        private readonly PeladaRepository repository;

        public PeladaController(PeladaRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pelada>> Get(int id)
        {
            var pelada = await repository.FindPeladaAsync(id);

            if (pelada != null)
                return pelada;

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<Pelada>>> Get()
        {
            var peladas = await repository.GetPeladasAsync();

            if (peladas != null)
                return peladas;

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Pelada pelada)
        {
            await repository.CreatePeladaAsync(pelada);
            return RedirectToAction("Get", new { id = pelada.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Pelada pelada)
        {
            if (await repository.UpdatePeladaAsync(id, pelada))
                return RedirectToAction("Get", new { id = id });

            return NotFound(new
            {
                message = "Pelada não encontrada !"
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await repository.DeletePeladaAsync(id))
            {
                return Ok(new
                {
                    message = "Pelada excluída com sucesso !"
                });
            }

            return NotFound(new
            {
                message = "Pelada não encontrada !"
            });
        }
    }
}