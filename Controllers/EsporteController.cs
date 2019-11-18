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
            try
            {
                await repository.CreateEsporteAsync(esporte);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return RedirectToAction("Get", new { id = esporte.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Esporte esporte)
        {
            try
            {
                if (await repository.UpdateEsporteAsync(id, esporte))
                    return RedirectToAction("Get", new { id = esporte.Id });
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
                if (await repository.DeleteEsporteAsync(id))
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