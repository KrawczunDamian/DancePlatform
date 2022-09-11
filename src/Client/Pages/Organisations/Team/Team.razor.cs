using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using DancePlatform.Application.Features.Teams.Commands.AddMember;
using DancePlatform.Application.Features.Teams.Commands.UpdateProfilePicture;
using DancePlatform.Application.Features.Teams.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Queries.GetById;
using DancePlatform.Client.Extensions;
using DancePlatform.Client.Infrastructure.Managers.Organisations.Team;
using DancePlatform.Client.Infrastructure.Managers.UserProfile;
using DancePlatform.Domain.Entities.UserProfile;
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
        private List<Dancer> _teamMembers = new();

        private ClaimsPrincipal _currentUser;
        private bool _canCreateTeams;
        private bool _canEditTeams;
        private bool _canDeleteTeams;
        private bool _canExportTeams;
        private bool _canSearchTeams;
        private bool _loaded;
        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateTeams = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Create)).Succeeded;
            _canEditTeams = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Edit)).Succeeded;
            _canDeleteTeams = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Delete)).Succeeded;
            _canExportTeams = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Export)).Succeeded;
            _canSearchTeams = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Search)).Succeeded;


            await GetTeamAsync(teamId);
            await GetTeamMembersAsync(teamId);
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

        private async Task Delete(int id)
        {
            string deleteContent = _localizer["Delete Content"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await TeamManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }
        private async Task AddMember(int id = 0)
        {
            /*var parameters = new DialogParameters();
            if(_team != null)
            {
                parameters.Add(nameof(AddTeamMemberModal),_team.Id);
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddTeamMemberModal>(_localizer["Add Member"], parameters, options);
            var result = await dialog.Result;*/
            var parameters = new DialogParameters();
            if (_team.Id != 0)
            {
                if (_team != null)
                {
                    parameters.Add(nameof(AddEditTeamModal), new AddEditTeamCommand
                    {
                        Id = _team.Id,
                        Name = _team.Name,
                        Description = _team.Description
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditTeamModal>(_team.Id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }
        private async Task Reset()
        {
            await GetTeamMembersAsync();
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