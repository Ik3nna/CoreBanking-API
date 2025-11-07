public record CSCreditScoreResponse

{

    public string CustomerId { get; init; } = string.Empty;

    public int Score { get; init; }

    public string Band { get; init; } = string.Empty; // "Poor", "Fair", "Good", "Excellent"

    public DateTime GeneratedAt { get; init; }

    public string[] Factors { get; init; } = Array.Empty<string>();

    public bool IsSuccess { get; init; }

    public string ErrorMessage { get; init; } = string.Empty;



    public static CSCreditScoreResponse Failed(string error) => new()

    {

        IsSuccess = false,

        ErrorMessage = error

    };

}



// CoreBanking.Application/External/DTOs/CSCreditReportResponse.cs

public record CSCreditReportResponse

{

    public string CustomerId { get; init; } = string.Empty;

    public decimal TotalDebt { get; init; }

    public int ActiveAccounts { get; init; }

    public int LatePayments { get; init; }

    public string Status { get; init; } = string.Empty;

    public bool IsSuccess { get; init; }

    public string ErrorMessage { get; init; } = string.Empty;



    public static CSCreditReportResponse Failed(string error) => new()

    {

        IsSuccess = false,

        ErrorMessage = error

    };

}



// CoreBanking.Application/External/DTOs/CSCustomerValidationRequest.cs

public record CSCustomerValidationRequest

{

    public string CustomerId { get; init; } = string.Empty;

    public string FullName { get; init; } = string.Empty;

    public DateTime DateOfBirth { get; init; }

    public string BVN { get; init; } = string.Empty; // BVN, SSN, etc.

}



// CoreBanking.Application/External/DTOs/CSValidationResponse.cs

public record CSValidationResponse

{

    public bool IsValid { get; init; }

    public string Reason { get; init; } = string.Empty;

}