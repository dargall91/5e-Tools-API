using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Item))]
public class Item
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }
}
