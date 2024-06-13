using FastEndpoints;
using WorkshopCDA.Data;
using WorkshopCDA.DTO.Responses.RaceResponses;
using WorkshopCDA.Endpoints.UserEndpoints;
using WorkshopCDA.Models;

namespace WorkshopCDA.Endpoints.RaceEndpoints
{
    public class GetAllRacesEndpoint : EndpointWithoutRequest<List<GetAllRacesResponseDTO>>
    {
        private readonly FinalWorkshopContext _dbContext;

        public GetAllRacesEndpoint(FinalWorkshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/races");
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

            Console.WriteLine("Récupération de toutes les races dans la base de données...");

            List<Race> races = await Race.GetAllRaces(_dbContext);

            List<GetAllRacesResponseDTO> response = races.ConvertAll(r => new GetAllRacesResponseDTO
            {
                Id = r.RaceId,
                Name = r.Name,
                Description = r.Description,
            });

            await SendAsync(response);
        }
    }
}
