using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kol2.Models;

[Table("Backpack")]
[PrimaryKey(nameof(CharacterId), nameof(ItemId))]
public class BackPack
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    
    [Required]
    public int Amount { get; set; }
    
    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public Character Character { get; set; }
}