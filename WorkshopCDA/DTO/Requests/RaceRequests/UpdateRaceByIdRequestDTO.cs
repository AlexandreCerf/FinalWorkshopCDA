namespace WorkshopCDA.DTO.Requests.RaceRequests
{
    public class UpdateRaceByIdRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
