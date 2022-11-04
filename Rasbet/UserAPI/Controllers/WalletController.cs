using Domain;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using DTO.UserDTO;
using BetApplication.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace UserAPI.Controllers;

    public class WalletController : BaseController
    {
        private readonly IWalletRepository walletRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly UserManager<User> userManager;
    


        public WalletController(IWalletRepository walletRepository,
                                ITransactionRepository transactionRepository,
                                UserManager<User> userManager)
        {
            this.walletRepository = walletRepository;
            this.transactionRepository = transactionRepository;
            this.userManager = userManager; 
        }

        /// <summary>
        /// Get user wallet.
        /// </summary>
        /// <param name="userId">Id of the user whose wallet we want to retrieve.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<WalletDTO> Get(string id)
        {
            WalletDTO dto = await walletRepository.Get(id);

            return dto;
        }

        /// <summary>
        /// Deposit funds to a user.
        /// </summary>
        /// <param name="value"></param>
        [HttpPut("deposit")]
        public async Task<IActionResult> DepositFunds([FromBody] TransactionDTO transaction)
        {
            try {
                AppUser user = await walletRepository.DepositFunds(transaction.UserId, transaction.Value);
                await transactionRepository.MakeDeposit(user, transaction.Value);
                return Ok();
            } catch (Exception e) {
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
            try {
                AppUser user = await walletRepository.WithdrawFunds(transaction.UserId, transaction.Value);
                await transactionRepository.WithdrawBalance(user, transaction.Value);
            return Ok();
            } catch(Exception e){
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
        public async Task<IActionResult> RegisterBetSimple([FromBody] BetDTO bet) 
        {
            try
            {
                await walletRepository.RegisterBet(bet.UserId, bet.BetId, bet.Value, bet.Odd);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update previously registered bet
        ///     - update account balance (if applies...)
        ///     - update bet status
        /// </summary>
        /// <param name="userId"> Id of the user who made the bet.</param>
        /// <param name="betId"> Id of the bet to update.</param>
        [HttpPost("bet/result")]
        public Task<IActionResult> RegisterBetResult(int userId, int betId)
        {
            walletRepository.Register

        }
}
