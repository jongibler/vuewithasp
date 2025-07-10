using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase {
    private readonly ITodoRepository _repo;

    public TodoController(ITodoRepository repo) {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll() =>
        Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> Get(int id) {
        var item = await _repo.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem item) {
        await _repo.AddAsync(item);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }
   
}
