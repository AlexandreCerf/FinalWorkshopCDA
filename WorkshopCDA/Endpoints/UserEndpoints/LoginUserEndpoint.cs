using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WorkshopCDA.Data;
using WorkshopCDA.DTO.Requests.UserRequests;
using WorkshopCDA.DTO.Responses.UserResponses;
using WorkshopCDA.Models;
using System.Threading.Tasks;

namespace WorkshopCDA.Endpoints.UserEndpoints
{
    public class LoginUserEndpoint : Endpoint<LoginUserRequestDTO, LoginUserResponseDTO>
    {
        private readonly FinalWorkshopContext _dbContext;
        public static bool IsUserLogged = false;

        public LoginUserEndpoint(FinalWorkshopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/user/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginUserRequestDTO req, CancellationToken ct)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == req.Email);

            if (user == null)
            {
                await SendAsync(new LoginUserResponseDTO
                {
                    IsAuthenticated = false,
                    Message = "Identifiants invalides et personne non trouvée."
                });
                return;
            }

            bool logger = Client.VerifyPassword(req.Password, user.Password);

            if (!logger)
            {
                await SendAsync(new LoginUserResponseDTO
                {
                    IsAuthenticated = false,
                    Message = "Identifiants invalides"
                });
                return;
            }

            IsUserLogged = true;

            await SendAsync(new LoginUserResponseDTO
            {
                IsAuthenticated = true,
                Message = "Vous êtes connecté"
            });
        }
    }
}
