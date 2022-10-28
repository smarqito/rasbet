using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAPI.Controllers;

public class WalletController : BaseController
{
    /// <summary>
    /// Get user wallet
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("{userId}")]
    public string Get(int userId)
    {
        return "value";
    }

    /// <summary>
    /// Deposit funds to a user
    ///     use HttpAccessor to access user token and retrieve current user
    /// </summary>
    /// <param name="value"></param>
    [HttpPut("deposit")]
    public void DepositFunds(double value)
    {
    }

    /// <summary>
    /// Withdraw money from current user
    /// </summary>
    /// <param name="value"></param>
    [HttpPut("withdraw")]
    public void WitdhdrawFunds(double value)
    {
    }

    /// <summary>
    /// Register a bet to users wallet history
    ///     - update account balance
    ///     - insert into wallet history
    ///     - keep the bet in open state until POST bet/result
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="betId"></param>
    /// <param name="value"></param>
    /// <param name="odd"></param>
    [HttpPost("bet")]
    public void RegisterBet(int userId, int betId, double value, double odd)
    {
    }

    /// <summary>
    /// Update previously registered bet
    ///     - update account balance (if applies...)
    ///     - update bet status
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="betId"></param>
    /// <param name="win"></param>
    [HttpPost("bet/result")]
    public void RegisterBetResult(int userId, int betId, bool win)
    {
    }
}
