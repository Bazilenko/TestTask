using FluentValidation;
using TestTask.BLL.DTOs.Booking;

namespace TestTask.BLL.Validators.Booking;
public class CreateBookingDtoValidator : AbstractValidator<CreateBookingDto>
{
    public CreateBookingDtoValidator()
    {
        RuleFor(x => x.RoomId)
            .GreaterThan(0).WithMessage("Room ID must be greater than 0.");

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTimeOffset.UtcNow).WithMessage("Booking start time must be in the future.");

        RuleFor(x => x.DurationHours)
            .GreaterThan(0).WithMessage("Duration must be at least 1 hour.")
            .LessThanOrEqualTo(24).WithMessage("Duration cannot exceed 24 hours.");

        When(x => x.ServiceIds != null && x.ServiceIds.Any(), () =>
        {
            RuleForEach(x => x.ServiceIds)
                .GreaterThan(0).WithMessage("Service ID must be greater than 0.");
        });
    }
}