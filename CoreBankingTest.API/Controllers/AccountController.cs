using AutoMapper;
using CoreBanking.Application.Accounts.Commands.CreateAccount;
using CoreBanking.Application.Accounts.Commands.TransferMoney;
using CoreBanking.Application.Accounts.Queries.GetAccountDetails;
using CoreBanking.Application.Accounts.Queries.GetTransactionHistory;
using CoreBanking.Core.ValueObjects;
using MediatR;
using CoreBanking.API.Models;
using CoreBanking.API.Models.Requests;

//using CoreBanking.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace CoreBanking.API.Controllers;



[ApiController]

[Route("api/[controller]")]

[Produces("application/json")]

public class AccountsController : ControllerBase

{

    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    private readonly ILogger<AccountsController> _logger;



    public AccountsController(IMediator mediator, IMapper mapper, ILogger<AccountsController> logger)

    {

        _mediator = mediator;

        _mapper = mapper;

        _logger = logger;

    }



    [HttpGet("{accountNumber}")]

    [ProducesResponseType(typeof(ApiResponse<CoreBanking.Application.Accounts.Queries.GetAccountDetails.GetAccountDetailsQuery.AccountDetailsDto>), StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ApiResponse<CoreBanking.Application.Accounts.Queries.GetAccountDetails.GetAccountDetailsQuery.AccountDetailsDto>>> GetAccountDetails(string accountNumber)

    {

        _logger.LogInformation("Retrieving account details for {AccountNumber}", accountNumber);



        var query = new GetAccountDetailsQuery { AccountNumber = AccountNumber.Create(accountNumber) };

        var result = await _mediator.Send(query);



        if (!result.IsSuccess)

            return NotFound(ApiResponse.CreateFailure(result.Errors));



        return Ok(ApiResponse<CoreBanking.Application.Accounts.Queries.GetAccountDetails.GetAccountDetailsQuery.AccountDetailsDto>.CreateSuccess(result.Data!));

    }



    [HttpPost]

    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ApiResponse<Guid>>> CreateAccount([FromBody] CreateAccountRequest request)

    {

        _logger.LogInformation("Creating new account for customer {CustomerId}", CustomerId.Create(request.CustomerId));



        var command = _mapper.Map<CreateAccountCommand>(request);

        var result = await _mediator.Send(command);



        if (!result.IsSuccess)

            return BadRequest(ApiResponse.CreateFailure(result.Errors));



        return CreatedAtAction(

            nameof(GetAccountDetails),

            new { accountNumber = "TEMPORARY" }, // Would need account number here

            ApiResponse<Guid>.CreateSuccess(result.Data!));

    }



    [HttpPost("{accountNumber}/transfer")]

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]

    public async Task<ActionResult<ApiResponse>> TransferMoney(

        string accountNumber,

        [FromBody] TransferMoneyRequest request)

    {

        _logger.LogInformation("Processing transfer from {AccountNumber}", accountNumber);



        var command = new TransferMoneyCommand

        {

            SourceAccountNumber = AccountNumber.Create(accountNumber),

            DestinationAccountNumber = AccountNumber.Create(request.DestinationAccountNumber),

            Amount = new Money(request.Amount, request.Currency),

            Reference = request.Reference,

            Description = request.Description

        };



        var result = await _mediator.Send(command);



        if (!result.IsSuccess)

        {

            return result.Errors.Any(e => e.Contains("insufficient", StringComparison.OrdinalIgnoreCase))

                ? Conflict(ApiResponse.CreateFailure(result.Errors))

                : BadRequest(ApiResponse.CreateFailure(result.Errors));

        }



        return Ok(ApiResponse.CreateSuccess("Transfer completed successfully"));

    }



    [HttpGet("{accountNumber}/transactions")]

    [ProducesResponseType(typeof(ApiResponse<TransactionHistoryDto>), StatusCodes.Status200OK)]

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ApiResponse<TransactionHistoryDto>>> GetTransactionHistory(

        string accountNumber,

        [FromQuery] DateTime? startDate = null,

        [FromQuery] DateTime? endDate = null,

        [FromQuery] int page = 1,

        [FromQuery] int pageSize = 50)

    {

        var query = new GetTransactionHistoryQuery

        {

            AccountNumber = AccountNumber.Create(accountNumber),

            StartDate = startDate,

            EndDate = endDate,

            Page = page,

            PageSize = pageSize

        };



        var result = await _mediator.Send(query);



        if (!result.IsSuccess)

            return NotFound(ApiResponse.CreateFailure(result.Errors));



        return Ok(ApiResponse<TransactionHistoryDto>.CreateSuccess(result.Data!));

    }

}