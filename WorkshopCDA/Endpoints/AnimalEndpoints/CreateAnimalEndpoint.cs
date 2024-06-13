using FastEndpoints;
using WorkshopCDA.Data;
using WorkshopCDA.DTO.Requests.AnimalRequests;
using WorkshopCDA.DTO.Responses.AnimalResponses;
using WorkshopCDA.Models;
using System.Threading.Tasks;
using WorkshopCDA.Endpoints.UserEndpoints;

namespace WorkshopCDA.Endpoints.AnimalEndpoints
{
    public class CreateAnimalEndpoint : Endpoint<CreateAnimalRequestDTO, CreateAnimalResponseDTO>
    {
        private readonly FinalWorkshopContext _dbContext;

        public CreateAnimalEndpoint(FinalWorkshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/animal/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateAnimalRequestDTO req, CancellationToken ct)
        {
            if (!LoginUserEndpoint.IsUserLogged)
            {
                Console.WriteLine("L'utilisateur doit être connecté !");
                await SendUnauthorizedAsync();
                return;
            }

            Console.WriteLine("Création de l'animal dans la base de données...");
            Console.WriteLine("Nom : {0}", req.Name);
            Console.WriteLine("Description : {0}", req.Description);

            Animal animal = Animal.CreateAnimal(req.Name, req.Description);
            animal.RaceId = req.RaceId;

            _dbContext.Animals.Add(animal);
            await _dbContext.SaveChangesAsync();

            CreateAnimalResponseDTO response = new()
            {
                Id = animal.Id,
                Name = animal.Name,
                Description = animal.Description,
            };

            await SendAsync(response);
        }
    }
}
