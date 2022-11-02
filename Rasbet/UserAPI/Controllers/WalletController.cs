using Domain;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Interfaces;
using Microsoft.AspNet.Identity.Owin;


namespace UserAPI.Controllers;

    public class WalletController : BaseController
    {
        private readonly IWalletRepository walletRepository;


        public WalletController(IWalletRepository walletRepository)
        {
            this.walletRepository = walletRepository;
        }

        /// <summary>
        /// Get user wallet.
        /// </summary>
        /// <param name="userId">Id of the user whose wallet we want to retrieve.</param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public string Get(int userId)
        {
            Wallet wallet = walletRepository.Get(userId);
            return wallet.Balance.ToString();
        }

        /// <summary>
        /// Deposit funds to a user.
        /// </summary>
        /// <param name="value"></param>
        [HttpPut("deposit")]
        public void DepositFunds(double value)
        {
            walletRepository.DepositFunds(value);
        }

        /// <summary>
        /// Withdraw money from current user
        /// </summary>
        /// <param name="value"> Value to withdraw. </param>
        [HttpPut("withdraw")]
        public void WithdrawFunds(double value)
        {
            walletRepository.WithdrawFunds(value);
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
        [HttpPost("bet")]
        public void RegisterBet(int userId, int betId, double value, double odd)
        {

        }

        /// <summary>
        /// Update previously registered bet
        ///     - update account balance (if applies...)
        ///     - update bet status
        /// </summary>
        /// <param name="userId"> Id of the user who made the bet.</param>
        /// <param name="betId"> Id of the bet to update.</param>
        /// <param name="win"> Indicates whether the user won the bet or not.</param>
        [HttpPost("bet/result")]
        public void RegisterBetResult(int userId, int betId, bool win)
        {
        }
    }
