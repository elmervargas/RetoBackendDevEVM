using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("email")]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmailPost model)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                using (var context = new TestContext())
                {
                    context.Database.EnsureCreated();

                    var person = context.Person.FirstOrDefault(x => x.ID == model.personId);

                    if (person == null)
                    {
                        return this.NotFound();
                    }

                    var email = new Email()
                    {
                        Name = model.name,
                        Person = person
                    };

                    context.Email.Add(email);

                    context.SaveChanges();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
