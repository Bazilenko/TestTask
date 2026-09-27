using FluentValidation;
using TestTask.BLL.DTOs.Room;

namespace TestTask.BLL.Validators.Room;

public class CreateRoomDtoValidator : AbstractValidator<CreateRoomDto>
{
    public CreateRoomDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Room name is required.")
            .MaximumLength(100).WithMessage("Room name cannot exceed 100 characters.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Room capacity must be greater than 0 (minimum 1 person).");

        RuleFor(x => x.HourlyRate)
            .GreaterThanOrEqualTo(0).WithMessage("Hourly rate cannot be negative.");

        When(x => x.ServiceIds != null, () =>
        {
            RuleForEach(x => x.ServiceIds)
                .GreaterThan(0).WithMessage("Service ID must be greater than 0.");
        });
    }
}