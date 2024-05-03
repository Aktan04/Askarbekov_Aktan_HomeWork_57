using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hw57.Models;

public class MyTaskContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<MyTask> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public MyTaskContext(DbContextOptions<MyTaskContext> options) : base(options){}
}