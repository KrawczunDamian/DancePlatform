using DancePlatform.Application.Features.Dancers.Queries.GetAll;
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
        /*
        /// <summary>
        /// Get a Dancer By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Dancers.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var team = await _mediator.Send(new GetDancerByIdQuery() { Id = id });
            return Ok(team);
        }

        /// <summary>
        /// Create/Update a Dancer
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Dancers.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditDancerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Dancer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Dancers.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteDancerCommand { Id = id }));
        }

        /// <summary>
        /// Search Dancers and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Dancers.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportDancersQuery(searchString)));
        }

        /// <summary>
        /// Get Profile picture by Id
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns>Status 200 OK </returns>
        [HttpGet("getProfilePicture/{teamId}")]
        [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IActionResult> GetProfilePictureAsync(int teamId)
        {
            return Ok(await _mediator.Send(new GetProfilePictureDancerQuery() { DancerId = teamId }));
        }

        /// <summary>
        /// Update team's profile picture
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Dancers.Create)]
        [HttpPost("updateProfilePicture")]
        public async Task<IActionResult> UpdateProfilePicture(UpdateProfilePictureDancerCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Add a member to team
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Dancers.Create)]
        [HttpPost("addDancerMember")]
        public async Task<IActionResult> AddDancerMember(AddDancerMemberCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Get team's members
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns>Status 200 OK </returns>
        [HttpGet("getDancerMembers/{teamId}")]
        [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IActionResult> GetDancerMembersAsync(int teamId)
        {
            return Ok(await _mediator.Send(new GetDancerMembersQuery() { DancerId = teamId }));
        }*/
    }
}