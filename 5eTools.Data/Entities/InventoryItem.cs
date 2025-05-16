using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace _5eTools.Data.Entities;

[Table(nameof(InventoryItem))]
[PrimaryKey($"{nameof(Entities.PlayerCharacter)}{nameof(Entities.PlayerCharacter.Id)}", $"{nameof(Entities.Item)}{nameof(Entities.Item.Id)}")]
public class InventoryItem
{
    [ForeignKey($"{nameof(Entities.PlayerCharacter)}{nameof(Entities.PlayerCharacter.Id)}")]
    public virtual required PlayerCharacter PlayerCharacter { get; set; }

    [ForeignKey($"{nameof(Entities.Item)}{nameof(Entities.Item.Id)}")]
    public virtual required Item Item { get; set; }

    public int Quantity { get; set; }
}
