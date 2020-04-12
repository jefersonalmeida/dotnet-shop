using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shop.Data;
using Shop.Models;
using Shop.Services;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Shop.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UserController : Controller
    {

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get(
            [FromServices] DataContext context
        )
        {
            var users = await context
                .Users
                .AsNoTracking()
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post(
            [FromBody] User model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                model.Role = "employee";
                context.Users.Add(model);
                await context.SaveChangesAsync();

                model.Password = null;
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });
            }
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put(
            Guid id,
            [FromBody] User model,
            [FromServices] DataContext context
        )
        {
            if (!model.Id.Equals(id))
                return NotFound(new { message = "Usuário não encontrado" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<User>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o usuário" });
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login(
            [FromBody] User model,
            [FromServices] DataContext context
        )
        {
            var user = await context.Users
                .AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(user);
            user.Password = null;
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
