using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5eTools.Data.Entities;

[Table(nameof(Music))]
public class Music
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string FileName { get; set; }
}
