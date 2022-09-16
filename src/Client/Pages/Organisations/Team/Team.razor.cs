using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Commands.UpdateProfilePicture;
using DancePlatform.Application.Features.Teams.Queries.GetById;
using DancePlatform.Client.Extensions;
using DancePlatform.Client.Infrastructure.Managers.Identity.Account;
using DancePlatform.Client.Infrastructure.Managers.Organisations.Team;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DancePlatform.Client.Pages.Organisations.Team
{
    public partial class Team
    {
        [Inject] private ITeamManager TeamManager { get; set; }

        [Parameter] public int teamId { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private GetTeamByIdResponse _team = new();
        private char _firstLetterOfName;
        private List<GetDancersWithProfileInfoResponse> _teamMembers = new();
        private List<string> _teamGallery = new();

        private ClaimsPrincipal _currentUser;
        private bool _canEditTeam;
        private bool _loaded;
        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();

            await GetTeamAsync(teamId);
            _canEditTeam = ((await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Edit)).Succeeded || _team.CreatedBy == _currentUser.GetUserId());
            await GetTeamMembersAsync(teamId);
            await GetTeamGalleryAsync(teamId);
            var data = await TeamManager.GetProfilePictureAsync(teamId);
            if (data.Succeeded)
            {
                ImageDataUrl = data.Data;
            }
            if (_team.Name.Length > 0)
            {
                _firstLetterOfName = _team.Name[0];
            }
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetTeamAsync(int id = 0)
        {
            var response = await TeamManager.GetByIdAsync(id);
            if (response.Succeeded)
            {
                _team = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private async Task GetTeamMembersAsync(int id = 0)
        {
            var response = await TeamManager.GetTeamMembersAsync(id);
            if (response.Succeeded)
            {
                _teamMembers = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private async Task GetTeamGalleryAsync(int id = 0)
        {
            var response = await TeamManager.GetTeamGalleryAsync(id);
            if (response.Succeeded)
            {
                _teamGallery = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private async Task AddMember()
        {
            var parameters = new DialogParameters()
            {
                ["teamId"] = _team.Id,
                ["teamsDancers"] = _teamMembers
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddTeamMemberModal>(_localizer["Add Member"], parameters, options);
            var result = await dialog.Result;
            await Reset();
        }
        private async Task AddPhoto(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var fileName = $"{teamId}-{Guid.NewGuid()}{extension}";
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                var request = new UploadPictureTeamCommand { TeamId = teamId, Data = buffer, FileName = fileName, Extension = extension, UploadType = Application.Enums.UploadType.TeamGalleryPictures };
                var result = await TeamManager.UploadTeamPicutreAsync(request);
                if (result.Succeeded)
                {
                    await Reset();
                    _snackBar.Add(_localizer["Photo Uploaded"], Severity.Success);
                }
                else
                {
                    foreach (var error in result.Messages)
                    {
                        _snackBar.Add(error, Severity.Error);
                    }
                }
            }
        }
        private async Task RemoveMember(int dancerId)
        {
            string deleteContent = _localizer["Remove Member"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, dancerId)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Remove"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await TeamManager.RemoveMemberAsync(_team.Id, dancerId);
                if (response.Succeeded)
                {
                    await Reset();
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);

                }
                else
                {
                    await Reset();
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }

            }
        }
        private async Task Reset()
        {
            _teamMembers = new List<GetDancersWithProfileInfoResponse>();
            _team = new GetTeamByIdResponse();
            _teamGallery = new List<string>();
            await GetTeamAsync(teamId);
            await GetTeamMembersAsync(teamId);
            await GetTeamGalleryAsync(teamId);

        }
        private IBrowserFile _file;

        [Parameter]
        public string ImageDataUrl { get; set; }

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var fileName = $"{teamId}-{Guid.NewGuid()}{extension}";
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                var request = new UpdateProfilePictureTeamCommand { TeamId = teamId, Data = buffer, FileName = fileName, Extension = extension, UploadType = Application.Enums.UploadType.TeamProfilePicture };
                var result = await TeamManager.UpdateProfilePictureAsync(request);
                if (result.Succeeded)
                {
                    _snackBar.Add(_localizer["Profile picture added."], Severity.Success);
                    _navigationManager.NavigateTo($"/organisations/team/{teamId}", true);
                }
                else
                {
                    foreach (var error in result.Messages)
                    {
                        _snackBar.Add(error, Severity.Error);
                    }
                }
            }
        }
    }
}