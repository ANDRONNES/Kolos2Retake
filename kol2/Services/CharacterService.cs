using Kol2.DAL;
using Kol2.DTOs;
using kol2.Exceptions;
using Kol2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kol2.Services;

public class CharacterService : ICharacterService
{
    private readonly DatabaseContext _context;

    public CharacterService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<GetCharactersDTO> GetCharacterAsync(int id)
    {
        var character = await _context.Characters
            .Where(c => c.CharacterId == id)
            .Select(c => new GetCharactersDTO
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                CurrentWeight = c.CurrentWeight,
                MaxWeight = c.MaxWeight,
                BackpacksItems = c.BackPacks.Select(bp => new BackPackDTO
                {
                    ItemName = bp.Item.Name,
                    ItemWeight = bp.Item.Weight,
                    Amount = bp.Amount,
                }).ToList(),
                Titles = c.CharacterTitles.Select(ct => new TitlesDTO()
                {
                    Title = ct.Title.Name,
                    AquiredAt = ct.AcquiredAt
                }).ToList(),
            }).FirstOrDefaultAsync();
        if (character == null)
        {
            throw new NotFoundException("Character with id: "+ id +" not found");
        } 

        return character;
    }
    
    public async Task AddItemsToBackPackAsync(int characterId, List<int> itemsToAddIds)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            int generalWeightOfNewItems = 0;
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.CharacterId == characterId);
            if (character == null)
            {
                throw new NotFoundException("Character with id: " + characterId + " not found");
            }
            foreach (var itemId in itemsToAddIds)
            {
                var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);
                if (item == null)
                {
                    throw new NotFoundException("Item with id: " + itemId + " not found");
                }
                generalWeightOfNewItems += item.Weight;
            }

            if (character.CurrentWeight + generalWeightOfNewItems > character.MaxWeight)
            {
                throw new ConflictException("Weight of adding items is too big");
            }
            
            character.CurrentWeight += generalWeightOfNewItems;

            foreach (var item in itemsToAddIds)
            {
                var existingItemInBackpack = await _context.BackPacks
                    .FirstOrDefaultAsync(bp => bp.CharacterId == characterId && bp.ItemId == item);

                if (existingItemInBackpack != null)
                {
                    existingItemInBackpack.Amount++;
                }
                else
                    await _context.BackPacks.AddAsync(new BackPack
                    {
                        CharacterId = characterId,
                        ItemId = item,
                        Amount = 1
                    });
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
        
    }
}