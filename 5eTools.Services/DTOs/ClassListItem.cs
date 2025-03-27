namespace _5eTools.Services.DTOs;

public class ClassListItem
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required IEnumerable<ListItem> Subclasses { get; set; }
}
