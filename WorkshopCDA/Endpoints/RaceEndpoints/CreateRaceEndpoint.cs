using FastEndpoints;
using WorkshopCDA.Data;
using WorkshopCDA.DTO.Requests.RaceRequests;
using WorkshopCDA.DTO.Responses.RaceResponses;
using WorkshopCDA.Endpoints.UserEndpoints;
using WorkshopCDA.Models;

namespace WorkshopCDA.Endpoints.RaceEndpoints
{
    public class CreateRaceEndpoint : Endpoint<CreateRaceRequestDTO, CreateRaceResponseDTO>
    {
        private readonly FinalWorkshopContext _dbContext;

        public CreateRaceEndpoint(FinalWorkshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/race/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateRaceRequestDTO req, CancellationToken ct)
        {
            if (!LoginUserEndpoint.IsUserLogged)
            {
                Console.WriteLine("L'utilisateur doit être connecté !");
                await SendUnauthorizedAsync();
                return;
            }

            Console.WriteLine("Création de la race dans la base de données...");
            Console.WriteLine("Nom : {0}", req.Name);
            Console.WriteLine("Description : {0}", req.Description);

            Race race = Race.CreateRace(req.Name, req.Description);

            _dbContext.Races.Add(race);
            await _dbContext.SaveChangesAsync();

            CreateRaceResponseDTO response = new()
            {
                Id = race.RaceId,
                Name = race.Name,
                Description = race.Description,
            };

            await SendAsync(response);
        }
    }
}
