using Microsoft.EntityFrameworkCore;

namespace Hw57.Models;

public class MyTaskContext : DbContext
{
    public DbSet<MyTask> Tasks { get; set; }
    public MyTaskContext(DbContextOptions<MyTaskContext> options) : base(options){}
}