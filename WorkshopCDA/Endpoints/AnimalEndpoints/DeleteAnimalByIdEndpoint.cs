using FastEndpoints;
using WorkshopCDA.Data;
using WorkshopCDA.Models;
using WorkshopCDA.DTO.Requests.AnimalRequests;
using WorkshopCDA.DTO.Responses.AnimalResponses;
using WorkshopCDA.Endpoints.UserEndpoints;

namespace WorkshopCDA.Endpoints.AnimalEndpoints
{
    public class DeleteAnimalEndpoint : Endpoint<DeleteAnimalByIdRequestDTO, DeleteAnimalByIdResponseDTO>
    {
        private readonly FinalWorkshopContext _dbContext;

        public DeleteAnimalEndpoint(FinalWorkshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Delete("/animal/delete/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteAnimalByIdRequestDTO req, CancellationToken ct)
        {
            if (!LoginUserEndpoint.IsUserLogged)
            {
                Console.WriteLine("L'utilisateur doit être connecté !");
                await SendUnauthorizedAsync();
                return;
            }

            Console.WriteLine("Suppression de l'animal dans la base de données...");
            Console.WriteLine("Id : {0}", req.Id);

            bool isDeleted = await Animal.DeleteAnimal(_dbContext, req.Id);

            if (!isDeleted)
            {
                await SendNotFoundAsync();
                return;
            }

            DeleteAnimalByIdResponseDTO response = new()
            {
                Id = req.Id,
                IsDeleted = true
            };

            await SendAsync(response);
        }
    }
}
