using MediatR;
using CoreBanking.Application.Common.Interfaces;
//using CoreBanking.Core.Common;
using CoreBanking.Core.ValueObjects;
using CoreBanking.Application.Common.Models;

namespace CoreBanking.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand : ICommand<CustomerId>, IRequest<Result<CustomerId>>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string BVN { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
}
