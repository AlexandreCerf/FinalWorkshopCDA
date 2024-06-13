namespace WorkshopCDA.DTO.Responses.UserResponses
{
    public class LoginUserResponseDTO
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; } = null!;
    }
}
