using DancePlatform.Client.Extensions;
using DancePlatform.Client.Infrastructure.Managers.Organisations.Team;
using DancePlatform.Client.Infrastructure.Managers.UserProfile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System.Threading.Tasks;

namespace DancePlatform.Client.Pages.Organisations.Team
{
    public partial class AddPhotosToTeamModal
    {
        [Inject] private ITeamManager TeamManager { get; set; }

        [Parameter] public int teamId { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        public void Cancel()
        {
            MudDialog.Cancel();
        }


        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
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
    }
}