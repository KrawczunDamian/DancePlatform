namespace DancePlatform.Application.Features.Teams.Queries.GetAll
{
    public class GetAllTeamsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
    }
}