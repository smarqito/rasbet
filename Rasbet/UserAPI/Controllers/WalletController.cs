using Domain;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Interfaces;
using DTO.UserDTO;
using Microsoft.AspNetCore.Identity;
using DTO.BetDTO;
using Microsoft.AspNetCore.Authorization;
using DTO.GameOddDTO;

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
    [HttpGet]
    [Authorize(Roles ="AppUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WalletDTO))]
    public async Task<IActionResult> Get([FromQuery]string userId)
    {
        WalletDTO dto = await walletRepository.Get(userId);

        return Ok(dto);
    }

    /// <summary>
    /// Deposit funds to a user.
    /// </summary>
    /// <param name="value"></param>
    [HttpPut("deposit")]
    [Authorize(Roles = "AppUser")]
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
    [Authorize(Roles = "AppUser")]
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
    /// Get all transactions between 2 dates
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("transactions")]
    public Task<IActionResult> GetTransactions(string userId, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

}
