using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using DancePlatform.Application.Features.Teams.Queries.GetAll;
using DancePlatform.Client.Extensions;
using DancePlatform.Client.Infrastructure.Managers.Organisations.Team;
using DancePlatform.Shared.Constants.Application;
using DancePlatform.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DancePlatform.Client.Pages.Organisations.Team
{
    public partial class Teams
    {
        [Inject] private ITeamManager TeamManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllTeamsResponse> _teamList = new();
        private GetAllTeamsResponse _team = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateTeams;
        private bool _canEditTeams;
        private bool _canDeleteTeams;
        private bool _canExportTeams;
        private bool _canSearchTeams;
        private bool _loaded;

        private MudTable<GetAllTeamsResponse> _teamsTable;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateTeams = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Create)).Succeeded;
            _canEditTeams = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Edit)).Succeeded;
            _canDeleteTeams = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Delete)).Succeeded;
            _canExportTeams = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Export)).Succeeded;
            _canSearchTeams = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Teams.Search)).Succeeded;

            await GetTeamsAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetTeamsAsync()
        {
            var response = await TeamManager.GetAllAsync();
            if (response.Succeeded)
            {
                _teamList = response.Data.ToList();
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

        private async Task ExportToExcel()
        {
            var response = await TeamManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Teams).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Teams exported"]
                    : _localizer["Filtered Teams exported"], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _team = _teamList.FirstOrDefault(c => c.Id == id);
                if (_team != null)
                {
                    parameters.Add(nameof(AddEditTeamModal.AddEditTeamModel), new AddEditTeamCommand
                    {
                        Id = _team.Id,
                        Name = _team.Name,
                        Description = _team.Description,
                        Country = _team.Country,
                        City = _team.City,
                        PhoneNumber = _team.PhoneNumber
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditTeamModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _team = new GetAllTeamsResponse();
            await GetTeamsAsync();
        }

        private bool Search(GetAllTeamsResponse team)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (team.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (team.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}