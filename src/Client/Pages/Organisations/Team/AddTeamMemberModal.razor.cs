using Blazored.FluentValidation;
using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Commands.AddMember;
using DancePlatform.Application.Features.Teams.Queries.GetById;
using DancePlatform.Client.Extensions;
using DancePlatform.Client.Infrastructure.Managers.Organisations.Team;
using DancePlatform.Client.Infrastructure.Managers.UserProfile;
using DancePlatform.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DancePlatform.Client.Pages.Organisations.Team
{
    public partial class AddTeamMemberModal
    {
        [Inject] private ITeamManager TeamManager { get; set; }
        [Inject] private IDancerManager DancerManager { get; set; }

        [Parameter] public int teamId { get; set; } = new();
        [Parameter] public List<GetDancersWithProfileInfoResponse> teamsDancers { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<GetDancersWithProfileInfoResponse> _allDancers = new();
        private bool _loaded;

        public void Cancel()
        {
            MudDialog.Cancel();
        }


        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            await GetAllDancersAsync();
            //_loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            await Task.CompletedTask;
        }
        private async Task GetAllDancersAsync()
        {
            var response = await DancerManager.GetAllAsync();
            if (response.Succeeded)
            {
                _allDancers = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
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
                var response = await TeamManager.RemoveMemberAsync(teamId, dancerId);
                if (response.Succeeded)
                {
                    teamsDancers.Remove(teamsDancers.FirstOrDefault(x => x.Id == dancerId));
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
        private async Task AddMember(int dancerId)
        {
            var command = new AddTeamMemberCommand()
            {
                DancerId = dancerId,
                TeamId = teamId
            };
            var response = await TeamManager.AddTeamMemberAsync(command);
            if (response.Succeeded)
            {
                teamsDancers.Add(_allDancers.FirstOrDefault(x => x.Id == dancerId));
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
        private async Task Reset()
        {
            await GetAllDancersAsync();
        }
    }
}