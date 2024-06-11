using Hw57.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Hw57.Services;

public class TaskService
{
    private MyTaskContext _context;
    private IMemoryCache cache;

    public TaskService(MyTaskContext context, IMemoryCache memoryCache)
    {
        _context = context;
        cache = memoryCache;
    }
    
    public async Task AddTask(MyTask task)
    {
        _context.Tasks.Add(task);
        int n = await _context.SaveChangesAsync();
        if (n > 0)
        {
            cache.Set(task.Id, task, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }
    }

    public async Task RemoveTask(int? id)
    {
        
        MyTask task = new MyTask() { Id = id.Value };
        _context.Entry(task).State = EntityState.Deleted;
        int n = await _context.SaveChangesAsync();
        if (n > 0)
        {
            cache.Remove(id);
        }
        
    }
    
    public async Task<MyTask> GetTask(int id)
    {
        MyTask? task = null;
        if (!cache.TryGetValue(id, out task))
        {
            var tasks = await _context.Tasks.Include(u => u.Creator).Include(u => u.Executor).ToListAsync(); 
            task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                cache.Set(task.Id, task,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }
        }
        return task;
    }
    
    
}