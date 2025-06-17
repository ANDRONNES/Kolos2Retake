using Kol2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Kol2.Services;

public interface ICharacterService
{
    public Task<GetCharactersDTO> GetCharacterAsync(int id);
    public Task AddItemsToBackPackAsync(int id, List<int> itemsToAddIds);
}