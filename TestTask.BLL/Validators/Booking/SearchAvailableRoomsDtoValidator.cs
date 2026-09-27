using FluentValidation;
using TestTask.BLL.DTOs.Booking;

namespace TestTask.BLL.Validators.Booking;
public class SearchAvailableRoomsDtoValidator : AbstractValidator<SearchAvailableRoomsDto>
{
    public SearchAvailableRoomsDtoValidator()
    {
        RuleFor(x => x.StartTime)
            .GreaterThan(DateTimeOffset.UtcNow).WithMessage("Start time must be in the future.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime).WithMessage("End time must be strictly after start time.");

        RuleFor(x => x.MinimumCapacity)
            .GreaterThan(0).WithMessage("Minimum capacity must be greater than 0.");
    }
}