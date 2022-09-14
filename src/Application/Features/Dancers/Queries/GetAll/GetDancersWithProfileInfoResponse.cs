namespace DancePlatform.Application.Features.Dancers.Queries.GetAll
{
    public class GetDancersWithProfileInfoResponse
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string CreatedById { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureDataUrl { get; set; }
    }
}