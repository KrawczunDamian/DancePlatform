using System;

namespace DancePlatform.Application.Features.Dancers.Queries.GetById
{
    public class GetDancerByAccountIdResponse
    {
        public int? Id { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Nickname { get; set; }
    }
}