using Domain;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Interfaces;
using DTO.UserDTO;
using Microsoft.AspNetCore.Identity;
using DTO.BetDTO;

namespace UserAPI.Controllers;

public class WalletController : BaseController
{
    private readonly IWalletRepository walletRepository;
    private readonly UserManager<User> userManager;



    public WalletController(IWalletRepository walletRepository,
                            UserManager<User> userManager)
    {
        this.walletRepository = walletRepository;
        this.userManager = userManager;
    }

    /// <summary>
    /// Get user wallet.
    /// </summary>
    /// <param name="userId">Id of the user whose wallet we want to retrieve.</param>
    /// <returns></returns>
    [HttpGet("{userId}")]
    public async Task<WalletDTO> Get(string userId)
    {
        WalletDTO dto = await walletRepository.Get(userId);

        return dto;
    }

    /// <summary>
    /// Deposit funds to a user.
    /// </summary>
    /// <param name="value"></param>
    [HttpPut("deposit")]
    public async Task<IActionResult> DepositFunds([FromBody] TransactionDTO transaction)
    {
        try
        {
            double value = double.Parse(transaction.Value);
            await walletRepository.DepositFunds(transaction.UserId, value);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Withdraw money from current user
    /// </summary>
    /// <param name="value"> Value to withdraw. </param>
    [HttpPut("withdraw")]
    public async Task<IActionResult> WithdrawFunds([FromBody] TransactionDTO transaction)
    {
        try
        {
            double value = double.Parse(transaction.Value);
            await walletRepository.WithdrawFunds(transaction.UserId, value);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    /// <summary>
    /// Register a bet to users wallet history
    ///     - update account balance
    ///     - insert into wallet history
    ///     - keep the bet in open state until POST bet/result
    /// </summary>
    /// <param name="userId"> Id of the user who made the bet.</param>
    /// <param name="betId"> Id of the bet.</param>
    /// <param name="value"> Value of the bet.</param>
    /// <param name="odd"> Odd of the bet.</param>
    [HttpPost("bet/simple")]
    public async Task<IActionResult> RegisterBetSimple([FromBody] CreateSimpleBetDTO bet)
    {
        try
        {
            await walletRepository.RegisterBetSimple(bet);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Register a bet to users wallet history
    ///     - update account balance
    ///     - insert into wallet history
    ///     - keep the bet in open state until POST bet/result
    /// </summary>
    /// <param name="userId"> Id of the user who made the bet.</param>
    /// <param name="betId"> Id of the bet.</param>
    /// <param name="value"> Value of the bet.</param>
    /// <param name="odd"> Odd of the bet.</param>
    [HttpPost("bet/multiple")]
    public async Task<IActionResult> RegisterBetMult([FromBody] CreateMultipleBetDTO bet)
    {
        try
        {
            await walletRepository.RegisterBetMult(bet);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Get all transactions between 2 dates
    /// </summary>
    /// <param name="userId">Id of the user</param>
    /// <param name="start">The date that starts the period of time of the transactions.</param>
    /// <param name="end">The date that ends the period of time of the transactions.</param>
    /// <returns>The transactions made on that period of  time by the given user.</returns>
    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions(string userId, DateTime start, DateTime end)
    {
        try
        {
            ICollection<TransactionDTO> transactions = await walletRepository.GetTransactions(userId, start, end);
            return Ok(transactions);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
