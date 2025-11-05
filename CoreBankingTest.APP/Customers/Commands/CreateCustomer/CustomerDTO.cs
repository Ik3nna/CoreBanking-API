using CoreBanking.Core.Enums;

namespace CoreBanking.Application.Customers.Commands.CreateCustomer
{
    public record CustomerDTO
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public List<AccountDTO> Accounts { get; set; } = new();
        public DateTime DateCreated { get; set; }
    }

    public class AccountDTO
    {
        public string AccountNumber { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
    }
}
