using Microsoft.EntityFrameworkCore;
using WorkshopCDA.Data;

namespace WorkshopCDA.Models
{
    public partial class Animal
    {
        public int Id { get; set; }
        public int RaceId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public virtual Race Race { get; set; } = null!;

        public Animal(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Animal CreateAnimal(string name, string description)
        {
            return new Animal(name, description);
        }

        public static async Task<Animal?> GetAnimalById(FinalWorkshopContext context, int id)
        {
            return await context.Animals.Include(a => a.Race).FirstOrDefaultAsync(a => a.Id == id);
        }

        public static async Task<bool> DeleteAnimal(FinalWorkshopContext context, int id)
        {
            Animal? animal = await context.Animals.FindAsync(id);
            if (animal == null)
            {
                return false;
            }
            context.Animals.Remove(animal);
            await context.SaveChangesAsync();
            return true;
        }

        public static async Task<Animal?> UpdateAnimal(FinalWorkshopContext context, int id, string name, string description, int raceId)
        {
            Animal? animal = await context.Animals.FindAsync(id);
            if (animal == null)
            {
                return null;
            }

            animal.Name = name;
            animal.Description = description;
            animal.RaceId = raceId;
            await context.SaveChangesAsync();
            return animal;
        }

        public static async Task<List<Animal>> GetAllAnimals(FinalWorkshopContext context)
        {
            return await context.Animals.Include(a => a.Race).ToListAsync();
        }
    }
}
