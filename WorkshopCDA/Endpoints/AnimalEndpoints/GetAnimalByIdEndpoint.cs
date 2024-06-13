using FastEndpoints;
using WorkshopCDA.Data;
using WorkshopCDA.Models;
using System.Threading.Tasks;
using WorkshopCDA.DTO.Requests.AnimalRequests;
using WorkshopCDA.DTO.Responses.AnimalResponses;
using WorkshopCDA.Endpoints.UserEndpoints;

namespace WorkshopCDA.Endpoints.AnimalEndpoints
{
    public class GetAnimalByIdEndpoint : Endpoint<GetAnimalByIdRequestDTO, GetAnimalByIdResponseDTO>
    {
        private readonly FinalWorkshopContext _dbContext;

        public GetAnimalByIdEndpoint(FinalWorkshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/animal/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAnimalByIdRequestDTO req, CancellationToken ct)
        {
            if (!LoginUserEndpoint.IsUserLogged)
            {
                Console.WriteLine("L'utilisateur doit être connecté !");
                await SendUnauthorizedAsync();
                return;
            }

            Animal? animal = await Animal.GetAnimalById(_dbContext, req.Id);

            if (animal == null)
            {
                await SendNotFoundAsync();
                return;
            }

            GetAnimalByIdResponseDTO response = new()
            {
                Id = animal.Id,
                Name = animal.Name,
                Description = animal.Description,
            };

            await SendAsync(response);
        }
    }
}
