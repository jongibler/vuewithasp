using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Repositories;

public class TodoRepository : ITodoRepository {
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync() =>
        await _context.TodoItems.ToListAsync();

    //Alternative if we want to pass through the DTO instead of resolving name in frontend...
    public async Task<IEnumerable<TodoItemDto>> GetAllDtosAsync()
    {
        var query = from t in _context.TodoItems
                    join p in _context.Persons on t.PersonId equals p.Id
                    select new TodoItemDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        IsComplete = t.IsComplete,
                        PersonId = t.PersonId,
                        PersonName = p.Name
                    };
        return await query.ToListAsync();
    }        

    public async Task<TodoItem?> GetByIdAsync(int id) =>
        await _context.TodoItems.FindAsync(id);

    public async Task AddAsync(TodoItem item) {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem item) {
        _context.TodoItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        var item = await _context.TodoItems.FindAsync(id);
        if (item != null) {
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        //one hit altenative. does not work with in-memory provider
        //await _context.TodoItems.Where(i => i.Id == id).ExecuteDeleteAsync();        
    }
}
