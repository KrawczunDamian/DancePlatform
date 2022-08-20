using DancePlatform.Application.Features.Teams.Commands.AddEdit;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DancePlatform.Application.Validators.Features.Teams.Commands.AddEdit
{
    public class AddEditTeamCommandValidator : AbstractValidator<AddEditTeamCommand>
    {
        public AddEditTeamCommandValidator(IStringLocalizer<AddEditTeamCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.Description)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Description is required!"]);
            RuleFor(request => request.Tax)
                .GreaterThan(0).WithMessage(x => localizer["Tax must be greater than 0"]);
        }
    }
}