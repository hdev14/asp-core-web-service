using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
using web_service.Repositories;

namespace web_service.Controllers
{
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
            await repository.CreateTimeAsync(time);
            return RedirectToAction("Get", new { id = time.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Time time)
        {
            if (await repository.UpdateTimeAsync(id, time))
                return RedirectToAction("Get", new { id = id });

            return NotFound(new
            {
                message = "Time não encontrado !"
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await repository.DeleteTimeAsync(id))
            {
                return Ok(new
                {
                    message = "Time excluído com sucesso !"
                });
            }

            return NotFound(new
            {
                message = "Time não encontrado !"
            });
        }
    }
}