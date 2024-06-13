namespace WorkshopCDA.DTO.Responses.AnimalResponses
{
    public class GetAnimalByIdResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
