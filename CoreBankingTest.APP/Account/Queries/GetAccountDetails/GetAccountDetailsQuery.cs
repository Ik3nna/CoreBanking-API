using CoreBanking.Application.Common.Interfaces;
using CoreBanking.Application.Common.Models;
using CoreBanking.Core.Interfaces;
using CoreBanking.Core.ValueObjects;
using MediatR;
using static CoreBanking.Application.Accounts.Queries.GetAccountDetails.GetAccountDetailsQuery;

namespace CoreBanking.Application.Accounts.Queries.GetAccountDetails;

public record GetAccountDetailsQuery : IQuery<AccountDetailsDto>
{
    public AccountNumber AccountNumber { get; init; }

    public record AccountDetailsDto
    {
        public AccountNumber AccountNumber { get; init; }

        public string AccountType { get; init; }

        public Money Balance { get; init; } = new Money(0);
        public DateTime DateOpened { get; init; }
        public bool IsActive { get; init; }
        public string CustomerName { get; init; } = string.Empty;
    }

    public class GetAccountDetailsQueryHandler : IRequestHandler<GetAccountDetailsQuery, Result<AccountDetailsDto>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountDetailsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<AccountDetailsDto>> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)

        {
            if (request.AccountNumber is null)
                return Result<AccountDetailsDto>.Failure("Account number is required");

            var account = await _accountRepository.GetByAccountNumberAsync(request.AccountNumber);

            if (account == null)
                return Result<AccountDetailsDto>.Failure("Account not found");

            var dto = new AccountDetailsDto
            {
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType.ToString(),
                Balance = new Money(account.Balance.Amount, account.Balance.Currency),
                DateOpened = account.DateOpened,
                IsActive = account.IsActive,
                CustomerName = $"{account.Customer.FirstName} {account.Customer.LastName}"
            };

            return Result<AccountDetailsDto>.Success(dto);
        }
    }
}