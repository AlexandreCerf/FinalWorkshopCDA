using FastEndpoints;
using WorkshopCDA.Data;
using WorkshopCDA.Models;
using WorkshopCDA.DTO.Responses.AnimalResponses;
using WorkshopCDA.Endpoints.UserEndpoints;

namespace WorkshopCDA.Endpoints.AnimalEndpoints
{
    public class GetAllAnimalsEndpoint : EndpointWithoutRequest<List<GetAllAnimalsResponseDTO>>
    {
        private readonly FinalWorkshopContext _dbContext;

        public GetAllAnimalsEndpoint(FinalWorkshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/animals");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            if (!LoginUserEndpoint.IsUserLogged)
            {
                Console.WriteLine("L'utilisateur doit être connecté !");
                await SendUnauthorizedAsync();
                return;
            }

            Console.WriteLine("Récupération de tous les animaux dans la base de données...");

            List<Animal> animals = await Animal.GetAllAnimals(_dbContext);

            List<GetAllAnimalsResponseDTO> response = animals.ConvertAll(a => new GetAllAnimalsResponseDTO
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                RaceId = a.RaceId
            });

            await SendAsync(response);
        }
    }
}
