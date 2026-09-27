using FluentValidation;
using TestTask.BLL.DTOs.Service;

namespace TestTask.BLL.Validators.Service;
public class UpdateServiceDtoValidator : AbstractValidator<UpdateServiceDto>
{
    public UpdateServiceDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
    }
}