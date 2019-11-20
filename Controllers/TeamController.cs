using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_service.Models;
using web_service.Repositories;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeController : ControllerBase
    {

        private readonly TeamRepository repository;
        public TimeController(TeamRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> Get(int id)
        {
            var team = await repository.FindTeamAsync(id);
            if (team != null)
                return team;

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<Team>>> Get()
        {
            var teams = await repository.FindTeamsAsync();

            if (teams != null)
                return teams;

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Team team)
        {
            try
            {
                await repository.CreateTeamAsync(team);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return RedirectToAction("Get", new { id = team.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Team team)
        {
            try
            {
                if (await repository.UpdateTeamAsync(id, team))
                    return Ok(new { message = "Time atualizado com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new { message = "Time não encontrado !" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await repository.DeleteTeamAsync(id))
                    return Ok(new { message = "Time excluído com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new { message = "Time não encontrado !" });
        }
        
    }
}