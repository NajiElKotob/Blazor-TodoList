using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoListMauiApp.Models;
public class TodoItem
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

    [StringLength(20)]
	[JsonPropertyName("title")]
	public string Title { get; set; }

	[JsonPropertyName("isdone")]
	public bool IsDone { get; set; }
}
