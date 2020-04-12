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
    [Route("v1")]
    public class HomeController : Controller
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get(
            [FromServices] DataContext context
        )
        {
            context.Users.RemoveRange(context.Users);
            context.Products.RemoveRange(context.Products);
            context.Categories.RemoveRange(context.Categories);
            await context.SaveChangesAsync();

            var employee = new User { Username = "robin", Password = "robin", Role = "employee" };
            var manager = new User { Username = "batman", Password = "batman", Role = "manager" };
            var category = new Category { Title = "Informática" };
            var product = new Product { Title = "Mouse", Description = "Mouse Gamer", Price = 299, Category = category };

            context.Users.Add(employee);
            context.Users.Add(manager);
            context.Categories.Add(category);
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return Ok(new
            {
                message = "Dados configurados"
            });
        }
    }
}
