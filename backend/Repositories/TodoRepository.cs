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

    public async Task<TodoItem?> GetByIdAsync(int id) =>
        await _context.TodoItems.FindAsync(id);

    public async Task AddAsync(TodoItem item) {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem item) {
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) {
        var item = await _context.TodoItems.FindAsync(id);
        if (item != null) {
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
