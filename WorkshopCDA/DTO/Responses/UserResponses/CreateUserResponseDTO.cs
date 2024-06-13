namespace WorkshopCDA.DTO.Responses.UserResponses
{
    public class CreateUserResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
