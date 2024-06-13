namespace WorkshopCDA.DTO.Requests.AnimalRequests
{
    public class UpdateAnimalByIdRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int RaceId { get; set; }
    }
}
