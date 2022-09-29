using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using DancePlatform.Application.Features.Teams.Commands.AddMember;
using DancePlatform.Application.Features.Teams.Commands.Delete;
using DancePlatform.Application.Features.Teams.Commands.RemoveTeamPicture;
using DancePlatform.Application.Features.Teams.Commands.UpdateProfilePicture;
using DancePlatform.Application.Features.Teams.Queries.Export;
using DancePlatform.Application.Features.Teams.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Queries.GetById;
using DancePlatform.Application.Features.Teams.Queries.GetGallery;
using DancePlatform.Application.Features.Teams.Queries.GetProfilePicture;
using DancePlatform.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DancePlatform.Server.Controllers.v1.Organisations
{
    public class TeamsController : BaseApiController<TeamsController>
    {
        /// <summary>
        /// Get All Teams
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Teams.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _mediator.Send(new GetAllTeamsQuery());
            return Ok(teams);
        }

        /// <summary>
        /// Get a Team By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Teams.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var team = await _mediator.Send(new GetTeamByIdQuery() { Id = id });
            return Ok(team);
        }

        /// <summary>
        /// Create/Update a Team
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Teams.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditTeamCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Team
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteTeamCommand { Id = id }));
        }

        /// <summary>
        /// Search Teams and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Teams.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportTeamsQuery(searchString)));
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
            return Ok(await _mediator.Send(new GetProfilePictureTeamQuery() { TeamId = teamId }));
        }

        /// <summary>
        /// Update team's profile picture
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("updateProfilePicture")]
        public async Task<IActionResult> UpdateProfilePicture(UpdateProfilePictureTeamCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Add a member to team
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Teams.Create)]
        [HttpPost("addTeamMember")]
        public async Task<IActionResult> AddTeamMember(AddTeamMemberCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Get team's members
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns>Status 200 OK </returns>
        [HttpGet("getTeamMembers/{teamId}")]
        public async Task<IActionResult> GetTeamMembersAsync(int teamId)
        {
            return Ok(await _mediator.Send(new GetTeamMembersQuery() { TeamId = teamId }));
        }
        /// <summary>
        /// Remove a member
        /// </summary>
        /// <param name="dancerId"></param>
        /// <param name="teamId"></param>
        /// <returns>Status 200 OK</returns>
        [HttpDelete("removeMember/{teamId}/{dancerId}")]
        public async Task<IActionResult> RemoveMemberAsync(int teamId, int dancerId)
        {
            return Ok(await _mediator.Send(new RemoveTeamMemberCommand { DancerId = dancerId, TeamId = teamId }));
        }
        /// <summary>
        /// Upload picture to team's gallery
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [HttpPost("uploadTeamPicture")]
        public async Task<IActionResult> UploadTeamPicture(UploadPictureTeamCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Get team's gallery
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns>Status 200 OK </returns>
        [HttpGet("getTeamGallery/{teamId}")]        
        public async Task<IActionResult> GetTeamGalleryAsync(int teamId)
        {
            return Ok(await _mediator.Send(new GetTeamGalleryQuery() { TeamId = teamId }));
        }
        /// <summary>
        /// Remove a picture from gallery
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Teams.Edit)]
        [HttpPost("removeTeamPicture")]
        public async Task<IActionResult> RemoveMemberAsync(RemoveTeamPictureCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}