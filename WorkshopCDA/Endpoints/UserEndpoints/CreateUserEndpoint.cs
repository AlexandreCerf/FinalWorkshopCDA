using FastEndpoints;
using WorkshopCDA.Data;
using WorkshopCDA.DTO.Requests.UserRequests;
using WorkshopCDA.DTO.Responses.UserResponses;
using WorkshopCDA.Models;

namespace WorkshopCDA.Endpoints.UserEndpoints
{
    public class CreateUserEndpoint : Endpoint<CreateUserRequestDTO, CreateUserResponseDTO>
    {
        private readonly FinalWorkshopContext _dbContext;

        public CreateUserEndpoint(FinalWorkshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/user/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUserRequestDTO req, CancellationToken ct)
        {
            Console.WriteLine("Création de l'utilisateur dans la base de données...");
            Console.WriteLine("Email : {0}", req.Email);
            Console.WriteLine("Mot de Passe : {0}", req.Password);

            Client user = Client.CreateUser(req.Email, req.Password);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            CreateUserResponseDTO response = new()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
            };

            await SendAsync(response);
        }
    }
}
