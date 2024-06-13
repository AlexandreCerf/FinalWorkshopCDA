using FastEndpoints;
using WorkshopCDA.Data;
using WorkshopCDA.DTO.Requests.AnimalRequests;
using WorkshopCDA.DTO.Responses.AnimalResponses;
using WorkshopCDA.Endpoints.UserEndpoints;
using WorkshopCDA.Models;

namespace WorkshopCDA.Endpoints.AnimalEndpoints
{
    public class UpdateAnimalByIdEndpoint : Endpoint<UpdateAnimalByIdRequestDTO, UpdateAnimalByIdResponseDTO>
    {
        private readonly FinalWorkshopContext _dbContext;

        public UpdateAnimalByIdEndpoint(FinalWorkshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Put("/animal/update/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateAnimalByIdRequestDTO req, CancellationToken ct)
        {
            if (!LoginUserEndpoint.IsUserLogged)
            {
                Console.WriteLine("L'utilisateur doit être connecté !");
                await SendUnauthorizedAsync();
                return;
            }

            Console.WriteLine("Mise à jour de l'animal dans la base de données...");
            Console.WriteLine("Id : {0}", req.Id);
            Console.WriteLine("Nom : {0}", req.Name);
            Console.WriteLine("Description : {0}", req.Description);
            Console.WriteLine("RaceId : {0}", req.RaceId);

            Animal? animal = await Animal.UpdateAnimal(_dbContext, req.Id, req.Name, req.Description, req.RaceId);

            if (animal == null)
            {
                await SendNotFoundAsync();
                return;
            }

            UpdateAnimalByIdResponseDTO response = new()
            {
                Id = animal.Id,
                Name = animal.Name,
                Description = animal.Description,
                RaceId = animal.RaceId
            };

            await SendAsync(response);
        }
    }
}
