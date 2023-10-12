using System.ComponentModel.DataAnnotations;

namespace TodoListHybridApp.Models;

public class TodoItem
{
    public Guid Id { get; set; }

    [StringLength(20)]
    public string Title { get; set; }
    public bool IsDone { get; set; }
}
