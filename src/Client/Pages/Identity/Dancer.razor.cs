using Blazored.FluentValidation;
using DancePlatform.Application.Requests.Identity;
using DancePlatform.Client.Extensions;
using DancePlatform.Client.Infrastructure.Managers.UserProfile;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace DancePlatform.Client.Pages.Identity
{
    public partial class Dancer
    {
        [Inject] private IDancerManager DancerManager { get; set; }
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly UpdateDancerRequest _dancerModel = new();

        public string UserId { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }
        private async Task UpdateDancerAsync()
        {
            var response = await _accountManager.UpdateDancerAsync(_dancerModel, UserId);
            if (response.Succeeded)
            {
                await _authenticationManager.Logout();
                _snackBar.Add(_localizer["Your Profile has been updated. Please Login to Continue."], Severity.Success);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private async Task LoadDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            UserId = user.GetUserId();
            //_profileModel.Email = user.GetEmail();
        }
    }
}