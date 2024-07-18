using FluentValidation;
using MediatR;

namespace Customer.Application.Usecases.Customer.Query.GetCustomer;

public class GetCustomerQuery : IRequest<GetCustomerResult>
{
    public string Document { get; set; }
    public string Email { get; set; }
}

public class GetCustomerQueryAbstractValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryAbstractValidator()
    {
        RuleFor(x => x).Must(x => !string.IsNullOrEmpty(x.Document) || !string.IsNullOrEmpty(x.Email))
            .WithMessage("Either Document or Email is required");
    }
}
