using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_service.Models;
using web_service.ModelsView;
using web_service.Repositories;
using web_service.Services.Auth;

namespace web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository repository;

        public UserController(UserRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await repository.FindUserAsync(id);

            if (user != null)
                return user;

            return NotFound();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UserView>>> Get()
        {
            var users = await repository.FindUsersAsync();

            if (users != null)
                return users;

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                user.Password = AuthManager.encrypt(user.Password);
                await repository.CreateUsuarioAsync(user);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return RedirectToAction("Get", new { id = user.Id });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Update(int id, User user)
        {
            try
            {
                if (await repository.UpdateUserAsync(id, user))
                    return Ok(new { message = "Usuário atualizado com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new { message = "Usuário não encontrado !" });
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> Delete(int id)
        {
            try
            {
                if (await repository.DeleteUserAsync(id))
                    return Ok(new { message = "Usuario excluído com sucesso !" });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = string.Format("Parâmetros inválidos - Error {0}", e.Message)
                });
            }

            return NotFound(new { message = "Usuário não encontrado !" });
        }

    }
}