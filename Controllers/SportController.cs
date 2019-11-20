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
    public class SportController : ControllerBase
    {   
        private readonly SportRepository repository;

        public SportController(SportRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sport>> Get(int id)
        {
            var sport = await repository.FindSportAsync(id);

            if (sport != null)
                return sport;

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<Sport>>> Get()
        {
            var sports = await repository.FindSportsAsync();

            if (sports != null)
                return sports;

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Sport sport)
        {
            try
            {
                await repository.CreateSportAsync(sport);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return RedirectToAction("Get", new { id = sport.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Sport sport)
        {
            try
            {
                if (await repository.UpdateSportAsync(id, sport))
                    return Ok(new { message = "Esporte atualizado com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new
            {
                message = "Esporte não encontrado !"
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await repository.DeleteSportAsync(id))
                    return Ok(new { message = "Esporte excluído com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new { message = "Esporte não encontrado !" });
        }
    }
}