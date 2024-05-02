using System.ComponentModel.DataAnnotations;

namespace Hw57.Models;

public class MyTask
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Заполните ячейку")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Заполните ячейку")]
    public string Priority { get; set; }
    public string Status { get; set; }
    [Required(ErrorMessage = "Заполните ячейку")]
    public string ExecutorName { get; set; }
    [Required(ErrorMessage = "Заполните ячейку")]
    public DateTime? DateOfCreation { get; set; }
    public DateTime? DateOfOpening { get; set; }
    public DateTime? DateOfClosing { get; set; }
    public string Description { get; set; }
}