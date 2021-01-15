using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("person")]
    public class PersonController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult<List<Person>>> Get()
        {
            using (var context = new TestContext())
            {
                context.Database.EnsureCreated();

                var items = context.Person
                    .Include(x => x.Addresses)
                    .Include(x => x.Emails)
                    .Include(x => x.Phones)
                    .ToList();

                return items;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            using (var context = new TestContext())
            {
                context.Database.EnsureCreated();

                var person = context.Person.Where(x => x.ID == id)
                    .Include(x => x.Addresses)
                    .Include(x => x.Emails)
                    .Include(x => x.Phones)
                    .FirstOrDefault();

                if (person == null)
                {
                    return this.NotFound();
                }

                return person;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonPost model)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                using (var context = new TestContext())
                {
                    context.Database.EnsureCreated();

                    var person = new Person()
                    {
                        Firstname = model.firstname,
                        Lastname = model.lastname,
                        Birthdate = DateTime.ParseExact(model.birthdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                        Documenttype = model.documenttype,
                        Documentnumber = model.documentnumber
                    };

                    context.Person.Add(person);

                    if (model.addresses != null && model.addresses.Any())
                    {
                        foreach (var address in model.addresses)
                        {
                            context.Address.Add(new Address()
                            {
                                Name = address.name,
                                Person = person
                            });
                        }
                    }

                    if (model.emails != null && model.emails.Any())
                    {
                        foreach (var email in model.emails)
                        {
                            context.Email.Add(new Email()
                            {
                                Name = email.name,
                                Person = person
                            });
                        }
                    }

                    if (model.phones != null && model.phones.Any())
                    {
                        foreach (var phone in model.phones)
                        {
                            context.Phone.Add(new Phone()
                            {
                                Name = phone.name,
                                Person = person
                            });
                        }
                    }

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
        public async Task<IActionResult> Put([FromBody]PersonPut model)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                using (var context = new TestContext())
                {
                    context.Database.EnsureCreated();

                    var person = context.Person.FirstOrDefault(x => x.ID == model.id);

                    if (person == null)
                    {
                        return this.NotFound();
                    }

                    person.Firstname = model.firstname;
                    person.Lastname = model.lastname;
                    person.Birthdate = DateTime.ParseExact(model.birthdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    person.Documenttype = model.documenttype;
                    person.Documentnumber = model.documentnumber;

                    context.Person.Update(person);

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

                    var person = context.Person.FirstOrDefault(x => x.ID == id);

                    if (person == null)
                    {
                        return this.NotFound();
                    }

                    context.Person.Remove(person);

                    context.SaveChanges();
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("{id}"), DisableRequestSizeLimit]
        public IActionResult Upload(int id)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            try
            {
                var file = Request.Form.Files[0];

                byte[] _fileBytes;

                if (file.Length == 0)
                {
                    return this.BadRequest(this.ModelState);
                }

                using (var ms = new System.IO.MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                    _fileBytes = fileBytes;
                }

                using (var context = new TestContext())
                {
                    context.Database.EnsureCreated();

                    var person = context.Person.FirstOrDefault(x => x.ID == id);

                    if (person == null)
                    {
                        return this.NotFound();
                    }

                    person.Photo = _fileBytes;

                    context.Person.Update(person);

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
