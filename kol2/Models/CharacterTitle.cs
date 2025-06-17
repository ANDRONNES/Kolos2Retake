using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kol.Models;
using Microsoft.EntityFrameworkCore;

namespace Kol2.Models;

[Table("Character_Title")]
[PrimaryKey(nameof(CharacterId),nameof(TitleId))]
public class CharacterTitle
{
    
    public int CharacterId { get; set; }
    public int TitleId { get; set; }
    
    [Required] 
    public DateTime AcquiredAt { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public Character Character { get; set; }
    
    [ForeignKey(nameof(TitleId))]
    public Title Title { get; set; }
}