using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("address")]
    public class AddressController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> Get(int id)
        {
            using (var context = new TestContext())
            {
                context.Database.EnsureCreated();

                var address = context.Address.FirstOrDefault(x => x.ID == id);

                if (address == null)
                {
                    return this.NotFound();
                }

                return address;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddressPost model)
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

                    var address = new Address()
                    {
                        Name = model.name,
                        Person = person
                    };

                    context.Address.Add(address);

                    context.SaveChanges();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]AddressPut model)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                using (var context = new TestContext())
                {
                    context.Database.EnsureCreated();

                    var address = context.Address.FirstOrDefault(x => x.ID == model.id);

                    if (address == null)
                    {
                        return this.NotFound();
                    }

                    address.Name = model.name;

                    context.Address.Update(address);

                    context.SaveChanges();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (var context = new TestContext())
                {
                    context.Database.EnsureCreated();

                    var address = context.Address.FirstOrDefault(x => x.ID == id);

                    if (address == null)
                    {
                        return this.NotFound();
                    }

                    context.Address.Remove(address);

                    context.SaveChanges();
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
