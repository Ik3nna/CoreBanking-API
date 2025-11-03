using CoreBanking.Application.Accounts.Commands.TransferMoney;
using FluentValidation;

namespace CoreBanking.API.Models.Requests;



public record CreateAccountRequest

{

    public Guid CustomerId { get; init; }

    public string AccountType { get; init; } = string.Empty;

    public decimal InitialDeposit { get; init; }

    public string Currency { get; init; } = "NGN";

}



// CoreBanking.API/Models/Requests/TransferMoneyRequest.cs

public record TransferMoneyRequest

{

    public string DestinationAccountNumber { get; init; } = string.Empty;

    public decimal Amount { get; init; }

    public string Currency { get; init; } = "NGN";

    public string Reference { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

}



// CoreBanking.API/Models/Requests/CreateCustomerRequest.cs

public record CreateCustomerRequest

{

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string Address { get; init; } = string.Empty;

    public DateTime DateOfBirth { get; init; }

}



// CoreBanking.Application/Accounts/Commands/TransferMoney/TransferMoneyCommandValidator.cs

public class TransferMoneyCommandValidator : AbstractValidator<TransferMoneyCommand>

{

    public TransferMoneyCommandValidator()

    {

        RuleFor(x => x.SourceAccountNumber.Value)

            .NotEmpty().WithMessage("Source account number is required")

            .Length(10).WithMessage("Source account number must be 10 digits")

            .Matches(@"^\d+$").WithMessage("Source account number must contain only digits");



        RuleFor(x => x.DestinationAccountNumber.Value)

            .NotEmpty().WithMessage("Destination account number is required")

            .Length(10).WithMessage("Destination account number must be 10 digits")

            .Matches(@"^\d+$").WithMessage("Destination account number must contain only digits")

            .NotEqual(cmd => cmd.SourceAccountNumber).WithMessage("Cannot transfer to the same account");



        RuleFor(x => x.Amount.Amount)

            .GreaterThan(0).WithMessage("Transfer amount must be greater than 0")

            .LessThanOrEqualTo(500000).WithMessage("Single transfer cannot exceed ₦500,000");



        RuleFor(x => x.Amount.Currency)

            .NotEmpty().WithMessage("Currency is required")

            .Length(3).WithMessage("Currency must be 3 characters");



        RuleFor(x => x.Reference)

            .MaximumLength(50).WithMessage("Reference cannot exceed 50 characters");



        RuleFor(x => x.Description)

            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters");

    }

}