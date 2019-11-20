using Microsoft.AspNetCore.Mvc;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AthleteController : ControllerBase
    {   
        /*
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
            try
            {
                await repository.CreateAtletaAsync(atleta);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return RedirectToAction("Get", new { id = atleta.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Atleta atleta)
        {
            try
            {
                if (await repository.UpdateAtletaAsync(id, atleta))
                    return Ok(new { message = "Atleta atualizado com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new { message = "Atleta não encontrado !" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await repository.DeleteAtletaAsync(id))
                    return Ok(new { message = "Atleta excluído com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new { message = "Atleta não encontrado !" });
        }
        */
    }
}