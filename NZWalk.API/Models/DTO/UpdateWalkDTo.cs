namespace NZWalk.API.Models.DTO
{
    public class UpdateWalkDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
    }
}
