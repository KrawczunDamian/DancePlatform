using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Dancers.Queries.GetById;
using DancePlatform.Application.Features.Teams.Commands.AddEdit;
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
            var dancers = await _mediator.Send(new GetAllDancersQuery());
            return Ok(dancers);
        }
        /// <summary>
        /// Get a Dancer By Account Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var dancer = await _mediator.Send(new GetDancerByAccountIdQuery() { AccountId = id });
            return Ok(dancer);
        }
        /// <summary>
        /// Create/Update a dancer profile
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPut]
        public async Task<IActionResult> Post(AddEditDancerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}