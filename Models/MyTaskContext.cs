using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hw57.Models;

public class MyTaskContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public MyTaskContext(DbContextOptions<MyTaskContext> options) : base(options){}
    public DbSet<MyTask> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MyTask>()
            .HasOne(t => t.Creator)
            .WithMany(u => u.CreatedTasks) 
            .HasForeignKey(t => t.CreatorId);

        modelBuilder.Entity<MyTask>()
            .HasOne(t => t.Executor)
            .WithMany(u => u.AssignedTasks) 
            .HasForeignKey(t => t.ExecutorId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.AssignedTasks)
            .WithOne(t => t.Executor)
            .HasForeignKey(t => t.ExecutorId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}