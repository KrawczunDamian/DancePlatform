using Blazored.FluentValidation;
using DancePlatform.Application.Features.Dancers.Queries.GetAll;
using DancePlatform.Application.Features.Teams.Commands.AddMember;
using DancePlatform.Client.Extensions;
using DancePlatform.Client.Infrastructure.Managers.Organisations.Team;
using DancePlatform.Client.Infrastructure.Managers.UserProfile;
using DancePlatform.Domain.Entities.UserProfile;
using DancePlatform.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DancePlatform.Client.Pages.Organisations.Team
{
    public partial class AddTeamMemberModal
    {
        [Inject] private ITeamManager TeamManager { get; set; }
        [Inject] private IDancerManager DancerManager { get; set; }

        [Parameter] public int TeamId { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<GetAllDancersResponse> _allDancers = new();
        private bool _loaded;

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        /*private async Task SaveAsync()
        {
            var response = await TeamManager.SaveAsync();
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
            await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
        }*/

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            //await GetAllDancersAsync();
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
    }
}