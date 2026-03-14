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

    // Maps
    public async Task<GameMap?> GetWorldMapAsync()
    {
        using var db = await _factory.CreateDbContextAsync();
        return await db.GameMaps.FirstOrDefaultAsync(m => m.Type == MapType.World);
    }

    public async Task<GameMap?> GetActiveLocalMapAsync()
    {
        using var db = await _factory.CreateDbContextAsync();
        return await db.GameMaps.FirstOrDefaultAsync(m => m.Type == MapType.Local && m.IsActive);
    }

    public async Task<List<GameMap>> GetAllLocalMapsAsync()
    {
        using var db = await _factory.CreateDbContextAsync();
        return await db.GameMaps
            .Where(m => m.Type == MapType.Local)
            .OrderByDescending(m => m.IsActive)
            .ThenBy(m => m.Name)
            .ToListAsync();
    }

    public async Task SaveMapAsync(GameMap map)
    {
        using var db = await _factory.CreateDbContextAsync();
        if (map.Id == 0)
        {
            // For world maps, replace existing
            if (map.Type == MapType.World)
            {
                var existing = await db.GameMaps.FirstOrDefaultAsync(m => m.Type == MapType.World);
                if (existing != null)
                {
                    existing.Name = map.Name;
                    existing.ImageData = map.ImageData;
                }
                else
                {
                    db.GameMaps.Add(map);
                }
            }
            else
            {
                db.GameMaps.Add(map);
            }
        }
        else
        {
            var existing = await db.GameMaps.FindAsync(map.Id);
            if (existing != null)
            {
                existing.Name = map.Name;
                existing.ImageData = map.ImageData;
                existing.IsActive = map.IsActive;
            }
        }
        await db.SaveChangesAsync();
    }

    public async Task SetActiveLocalMapAsync(int mapId)
    {
        using var db = await _factory.CreateDbContextAsync();
        var allLocal = await db.GameMaps.Where(m => m.Type == MapType.Local).ToListAsync();
        foreach (var m in allLocal)
        {
            m.IsActive = m.Id == mapId;
        }
        await db.SaveChangesAsync();
    }

    public async Task DeleteMapAsync(int mapId)
    {
        using var db = await _factory.CreateDbContextAsync();
        var map = await db.GameMaps.FindAsync(mapId);
        if (map != null)
        {
            db.GameMaps.Remove(map);
            await db.SaveChangesAsync();
        }
    }

    // Session Notes
    public async Task<List<SessionNote>> GetSessionNotesAsync()
    {
        using var db = await _factory.CreateDbContextAsync();
        return await db.SessionNotes
            .OrderByDescending(n => n.SessionDate)
            .ThenByDescending(n => n.UpdatedAt)
            .ToListAsync();
    }

    public async Task<SessionNote?> GetSessionNoteAsync(int id)
    {
        using var db = await _factory.CreateDbContextAsync();
        return await db.SessionNotes.FindAsync(id);
    }

    public async Task<SessionNote> SaveSessionNoteAsync(SessionNote note)
    {
        using var db = await _factory.CreateDbContextAsync();
        note.UpdatedAt = DateTime.Now;
        if (note.Id == 0)
        {
            db.SessionNotes.Add(note);
        }
        else
        {
            var existing = await db.SessionNotes.FindAsync(note.Id);
            if (existing != null)
            {
                existing.Title = note.Title;
                existing.Content = note.Content;
                existing.SessionDate = note.SessionDate;
                existing.UpdatedAt = note.UpdatedAt;
            }
        }
        await db.SaveChangesAsync();
        return note;
    }

    public async Task DeleteSessionNoteAsync(int id)
    {
        using var db = await _factory.CreateDbContextAsync();
        var note = await db.SessionNotes.FindAsync(id);
        if (note != null)
        {
            db.SessionNotes.Remove(note);
            await db.SaveChangesAsync();
        }
    }
}
