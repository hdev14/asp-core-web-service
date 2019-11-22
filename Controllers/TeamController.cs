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
    public class TeamController : ControllerBase
    {

        private readonly TeamRepository repository;
        public TeamController(TeamRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Team>> Get(int id)
        {
            var team = await repository.FindTeamAsync(id);
            if (team != null)
                return team;

            return NotFound();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<TeamView>>> Get()
        {
            var teams = await repository.FindTeamsAsync();

            if (teams != null)
                return teams;

            return NoContent();
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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