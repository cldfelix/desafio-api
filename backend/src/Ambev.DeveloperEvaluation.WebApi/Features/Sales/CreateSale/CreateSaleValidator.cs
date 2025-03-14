using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
{
    private readonly IUserRepository _userRepository;

    public CreateSaleValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        // customer deve ter um guid valido
        RuleFor(sale => sale.Customer).NotEmpty()
            .Must(BeAValidGuid).WithMessage("Invalid guid")
            .Must(BeAValidUser).WithMessage("Invalid user");
        RuleFor(sale => sale.Branch).NotEmpty().Length(3, 50);
        RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 100);
    }

    private bool BeAValidGuid(Guid guid) => guid != Guid.Empty;
    private bool BeAValidUser(Guid userId)
    => _userRepository.GetByIdAsync(userId).Result != null;
}
 