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
        private readonly TimeRepository repository;
        public TimeController(TimeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Time>> Get(int id)
        {
            var time = await repository.FindTimeAsync(id);
            if (time != null)
                return time;

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<List<Time>>> Get()
        {
            var times = await repository.GetTimesAsync();

            if (times != null)
                return times;

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Time time)
        {
            try
            {
                await repository.CreateTimeAsync(time);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return RedirectToAction("Get", new { id = time.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Time time)
        {
            try
            {
                if (await repository.UpdateTimeAsync(id, time))
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
                if (await repository.DeleteTimeAsync(id))
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