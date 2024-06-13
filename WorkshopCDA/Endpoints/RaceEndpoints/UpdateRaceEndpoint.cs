using FastEndpoints;
using WorkshopCDA.Data;
using WorkshopCDA.DTO.Requests.RaceRequests;
using WorkshopCDA.DTO.Responses.RaceResponses;
using WorkshopCDA.Endpoints.UserEndpoints;
using WorkshopCDA.Models;

namespace WorkshopCDA.Endpoints.RaceEndpoints
{
    public class UpdateRaceEndpoint : Endpoint<UpdateRaceByIdRequestDTO, UpdateRaceByIdResponseDTO>
    {
        private readonly FinalWorkshopContext _dbContext;

        public UpdateRaceEndpoint(FinalWorkshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Put("/race/update/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateRaceByIdRequestDTO req, CancellationToken ct)
        {
            if (!LoginUserEndpoint.IsUserLogged)
            {
                Console.WriteLine("L'utilisateur doit être connecté !");
                await SendUnauthorizedAsync();
                return;
            }

            Console.WriteLine("Mise à jour de la race dans la base de données...");
            Console.WriteLine("Id : {0}", req.Id);
            Console.WriteLine("Nom : {0}", req.Name);
            Console.WriteLine("Description : {0}", req.Description);

            Race? race = await Race.UpdateRace(_dbContext, req.Id, req.Name, req.Description);

            if (race == null)
            {
                await SendNotFoundAsync();
                return;
            }

            UpdateRaceByIdResponseDTO response = new()
            {
                Id = race.RaceId,
                Name = race.Name,
                Description = race.Description,
            };

            await SendAsync(response);
        }
    }
}
