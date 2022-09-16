namespace DancePlatform.Application.Features.Teams.Queries.GetById
{
    public class GetTeamByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProfilePictureURL { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
    }
}