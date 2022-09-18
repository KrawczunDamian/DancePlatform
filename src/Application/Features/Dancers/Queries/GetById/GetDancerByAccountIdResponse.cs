using System;

namespace DancePlatform.Application.Features.Dancers.Queries.GetById
{
    public class GetDancerByAccountIdResponse
    {
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Nickname { get; set; }
        public bool IsDeleted { get; set; }
    }
}