namespace WorkshopCDA.DTO.Requests.AnimalRequests
{
    public class CreateAnimalRequestDTO
    {
        public int RaceId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
