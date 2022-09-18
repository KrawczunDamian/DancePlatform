using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Dancers.Queries.GetById;
using DancePlatform.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DancePlatform.Server.Controllers.v1.UserProfile
{
    public class DancersController : BaseApiController<DancersController>
    {
        /// <summary>
        /// Get All Dancers
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Dancers.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _mediator.Send(new GetAllDancersQuery());
            return Ok(teams);
        }
        /// <summary>
        /// Get a Dancer By Account Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var team = await _mediator.Send(new GetDancerByAccountIdQuery() { AccountId = id });
            return Ok(team);
        }
    }
}