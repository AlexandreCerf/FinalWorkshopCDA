namespace WorkshopCDA.DTO.Responses.AnimalResponses
{
    public class GetAllAnimalsResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int RaceId { get; set; }
    }
}
