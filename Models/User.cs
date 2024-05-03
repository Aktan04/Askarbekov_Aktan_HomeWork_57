using Microsoft.AspNetCore.Identity;

namespace Hw57.Models;

public class User : IdentityUser<int>
{
    public ICollection<MyTask>? AssignedTasks { get; set; }
    public ICollection<MyTask>? CreatedTasks { get; set; }
}