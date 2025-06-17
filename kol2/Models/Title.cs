using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kol2.Models;

namespace Kol.Models;

[Table("Title")]
public class Title
{
    [Key]
    public int TitleId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    public ICollection<CharacterTitle> CharacterTitles { get; set; }
}