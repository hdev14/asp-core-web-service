using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
using web_service.ModelsView;
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
        [Authorize]
        public async Task<ActionResult<Athlete>> Get(int id)
        {
            var athlete = await repository.FindAthleteAsync(id);

            if (athlete != null)
                return athlete;

            return NotFound();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<AthleteView>>> Get()
        {
            var athlete = await repository.FindAthletesAsync();
            if (athlete != null)
                return athlete;

            return NoContent();
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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