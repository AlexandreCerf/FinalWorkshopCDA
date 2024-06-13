using Microsoft.EntityFrameworkCore;
using WorkshopCDA.Data;

namespace WorkshopCDA.Models;

public partial class Race(string name, string description)
{
    public int RaceId { get; set; }

    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public static Race CreateRace(string name, string description)
    {
        return new Race(name, description);
    }

    public static async Task<Race?> UpdateRace(FinalWorkshopContext context, int id, string name, string description)
    {
        Race? race = await context.Races.FindAsync(id);
        if (race == null)
        {
            return null;
        }

        race.Name = name;
        race.Description = description;
        await context.SaveChangesAsync();
        return race;
    }

    public static async Task<List<Race>> GetAllRaces(FinalWorkshopContext context)
    {
        return await context.Races.ToListAsync();
    }
}
