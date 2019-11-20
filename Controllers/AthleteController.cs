using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
using web_service.Repositories;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AthleteController : ControllerBase
    {   
        
        private readonly AthleteRepository repository;

        public AthleteController(AthleteRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Athlete>> Get(int id)
        {
            var athlete = await repository.FindAthleteAsync(id);

            if (athlete != null)
                return athlete;

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<Athlete>>> Get()
        {
            var athlete = await repository.FindAthletesAsync();
            if (athlete != null)
                return athlete;

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Athlete athlete)
        {
            try
            {
                await repository.CreateAthleteAsync(athlete);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return RedirectToAction("Get", new { id = athlete.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Athlete athlete)
        {
            try
            {
                if (await repository.UpdateAthleteAsync(id, athlete))
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
                if (await repository.DeleteAthleteAsync(id))
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
    }
}