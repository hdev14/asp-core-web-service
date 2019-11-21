using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<Pelada>> Get(int id)
        {
            var pelada = await repository.FindPeladaAsync(id);

            if (pelada != null)
                return pelada;

            return NotFound();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Pelada>>> Get()
        {
            var peladas = await repository.FindPeladasAsync();

            if (peladas != null)
                return peladas;

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(Pelada pelada)
        {
            try
            {
                await repository.CreatePeladaAsync(pelada);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return RedirectToAction("Get", new { id = pelada.Id });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Update(int id, Pelada pelada)
        {
            try
            {
                if (await repository.UpdatePeladaAsync(id, pelada))
                    return Ok(new { message = "Pelada atualizada com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new { message = "Pelada não encontrada !" });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await repository.DeletePeladaAsync(id))
                    return Ok(new { message = "Pelada excluída com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new { message = "Pelada não encontrada !" });
        }

    }
}