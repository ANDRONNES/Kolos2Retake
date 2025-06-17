using Kol2.DAL;
using Kol2.DTOs;
using Kol2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kol.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharactersController(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacters(int id)
    {
        var result = await _characterService.GetCharacterAsync(id);
        return Ok(result);
    }

    [HttpPost("{id}/backpacks")]
    public async Task<IActionResult> AddItemToBackPack(int id, [FromBody] List<int> itemsToAddIds)
    {
        await _characterService.AddItemsToBackPackAsync(id,itemsToAddIds);
        return Ok();
    }
}