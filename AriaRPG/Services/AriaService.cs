using Microsoft.EntityFrameworkCore;
using AriaRPG.Data;
using AriaRPG.Models;

namespace AriaRPG.Services;

public class AriaService
{
    private readonly IDbContextFactory<AriaDbContext> _factory;

    public AriaService(IDbContextFactory<AriaDbContext> factory)
    {
        _factory = factory;
    }

    // Players
    public async Task<List<Player>> GetPlayersAsync()
    {
        using var db = await _factory.CreateDbContextAsync();
        return await db.Players
            .Include(p => p.Character)
            .ThenInclude(c => c!.Skills)
            .Include(p => p.Character)
            .ThenInclude(c => c!.SpecialSkills)
            .Include(p => p.Character)
            .ThenInclude(c => c!.Inventory)
            .Include(p => p.Character)
            .ThenInclude(c => c!.Weapons)
            .ToListAsync();
    }

    public async Task<Player?> GetPlayerAsync(int id)
    {
        using var db = await _factory.CreateDbContextAsync();
        return await db.Players
            .Include(p => p.Character)
            .ThenInclude(c => c!.Skills)
            .Include(p => p.Character)
            .ThenInclude(c => c!.SpecialSkills)
            .Include(p => p.Character)
            .ThenInclude(c => c!.Inventory)
            .Include(p => p.Character)
            .ThenInclude(c => c!.Weapons)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task UpdatePlayerNameAsync(int playerId, string name)
    {
        using var db = await _factory.CreateDbContextAsync();
        var player = await db.Players.FindAsync(playerId);
        if (player != null)
        {
            player.Name = name;
            await db.SaveChangesAsync();
        }
    }

    // Characters
    public async Task<Character> CreateCharacterAsync(int playerId, Character character)
    {
        using var db = await _factory.CreateDbContextAsync();
        character.PlayerId = playerId;
        db.Characters.Add(character);
        await db.SaveChangesAsync();
        return character;
    }

    public async Task UpdateCharacterAsync(Character character)
    {
        using var db = await _factory.CreateDbContextAsync();
        var existing = await db.Characters.FindAsync(character.Id);
        if (existing != null)
        {
            db.Entry(existing).CurrentValues.SetValues(character);
            await db.SaveChangesAsync();
        }
    }

    public async Task DeleteCharacterAsync(int characterId)
    {
        using var db = await _factory.CreateDbContextAsync();
        var character = await db.Characters.FindAsync(characterId);
        if (character != null)
        {
            db.Characters.Remove(character);
            await db.SaveChangesAsync();
        }
    }

    // Skills
    public async Task AddSkillAsync(CharacterSkill skill)
    {
        using var db = await _factory.CreateDbContextAsync();
        db.CharacterSkills.Add(skill);
        await db.SaveChangesAsync();
    }

    public async Task UpdateSkillAsync(CharacterSkill skill)
    {
        using var db = await _factory.CreateDbContextAsync();
        var existing = await db.CharacterSkills.FindAsync(skill.Id);
        if (existing != null)
        {
            db.Entry(existing).CurrentValues.SetValues(skill);
            await db.SaveChangesAsync();
        }
    }

    public async Task DeleteSkillAsync(int skillId)
    {
        using var db = await _factory.CreateDbContextAsync();
        var skill = await db.CharacterSkills.FindAsync(skillId);
        if (skill != null)
        {
            db.CharacterSkills.Remove(skill);
            await db.SaveChangesAsync();
        }
    }

    // Special Skills
    public async Task AddSpecialSkillAsync(CharacterSpecialSkill skill)
    {
        using var db = await _factory.CreateDbContextAsync();
        db.CharacterSpecialSkills.Add(skill);
        await db.SaveChangesAsync();
    }

    public async Task UpdateSpecialSkillAsync(CharacterSpecialSkill skill)
    {
        using var db = await _factory.CreateDbContextAsync();
        var existing = await db.CharacterSpecialSkills.FindAsync(skill.Id);
        if (existing != null)
        {
            db.Entry(existing).CurrentValues.SetValues(skill);
            await db.SaveChangesAsync();
        }
    }

    public async Task DeleteSpecialSkillAsync(int skillId)
    {
        using var db = await _factory.CreateDbContextAsync();
        var skill = await db.CharacterSpecialSkills.FindAsync(skillId);
        if (skill != null)
        {
            db.CharacterSpecialSkills.Remove(skill);
            await db.SaveChangesAsync();
        }
    }

    // Inventory
    public async Task AddInventoryItemAsync(InventoryItem item)
    {
        using var db = await _factory.CreateDbContextAsync();
        db.InventoryItems.Add(item);
        await db.SaveChangesAsync();
    }

    public async Task UpdateInventoryItemAsync(InventoryItem item)
    {
        using var db = await _factory.CreateDbContextAsync();
        var existing = await db.InventoryItems.FindAsync(item.Id);
        if (existing != null)
        {
            db.Entry(existing).CurrentValues.SetValues(item);
            await db.SaveChangesAsync();
        }
    }

    public async Task DeleteInventoryItemAsync(int itemId)
    {
        using var db = await _factory.CreateDbContextAsync();
        var item = await db.InventoryItems.FindAsync(itemId);
        if (item != null)
        {
            db.InventoryItems.Remove(item);
            await db.SaveChangesAsync();
        }
    }

    // Weapons
    public async Task AddWeaponAsync(Weapon weapon)
    {
        using var db = await _factory.CreateDbContextAsync();
        db.Weapons.Add(weapon);
        await db.SaveChangesAsync();
    }

    public async Task UpdateWeaponAsync(Weapon weapon)
    {
        using var db = await _factory.CreateDbContextAsync();
        var existing = await db.Weapons.FindAsync(weapon.Id);
        if (existing != null)
        {
            db.Entry(existing).CurrentValues.SetValues(weapon);
            await db.SaveChangesAsync();
        }
    }

    public async Task DeleteWeaponAsync(int weaponId)
    {
        using var db = await _factory.CreateDbContextAsync();
        var weapon = await db.Weapons.FindAsync(weaponId);
        if (weapon != null)
        {
            db.Weapons.Remove(weapon);
            await db.SaveChangesAsync();
        }
    }
}
