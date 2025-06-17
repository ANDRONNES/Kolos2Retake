namespace Kol2.DTOs;

public class GetCharactersDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public List<BackPackDTO> BackpacksItems { get; set; }
    public List<TitlesDTO> Titles { get; set; }
}

public class BackPackDTO
{
    public string ItemName { get; set; }
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class TitlesDTO
{
    public string Title { get; set; }
    public DateTime AquiredAt { get; set; }
}