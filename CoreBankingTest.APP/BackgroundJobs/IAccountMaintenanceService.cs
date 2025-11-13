
namespace CoreBanking.Application.BackgroundJobs;
using CoreBanking.Application.Common.Models;

public interface IAccountMaintenanceService

{

    Task<AccountMaintenanceResult> CleanupInactiveAccountsAsync(CancellationToken cancellationToken = default);

    Task<AccountMaintenanceResult> ArchiveOldTransactionsAsync(DateTime cutoffDate, CancellationToken cancellationToken = default);

    Task<AccountMaintenanceResult> ValidateAccountDataAsync(CancellationToken cancellationToken = default);

    Task<AccountMaintenanceResult> UpdateAccountStatusesAsync(CancellationToken cancellationToken = default);

}


