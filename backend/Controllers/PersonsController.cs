using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetAll()
    {
        var testPersonsList = new List<Person>
        {
            new Person { Id = 1, Name = "Person 1"},
            new Person { Id = 2, Name = "Person 2"}
        };
        return Ok(testPersonsList);
    }
}
