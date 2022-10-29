using Domain;
using DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Interfaces;

namespace UserAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("login")]
        public Task<IActionResult> Login(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterAppUserDTO registerApp)
        {
            await userRepository.RegisterUser(registerApp.Name,
                                              registerApp.Email,
                                              registerApp.Nif,
                                              registerApp.PhoneNumber,
                                              registerApp.BirthDate,
                                              registerApp.Password);
            return Ok();
        }

        [HttpPost("admin")]
        public Task<IActionResult> RegisterAdmin()
        {
            throw new NotImplementedException();
        }

        [HttpPost("specialist")]
        public Task<IActionResult> RegisterSpecialist()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve UserDTO based on user id
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public Task<IActionResult> GetUser()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update user general info
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut]
        public Task<IActionResult> Update(object userDTO)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Update sensitive info
        ///     Expects to be confirmed by a code!
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut("sensitive")]
        public Task<IActionResult> UpdateSensitive()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Confirm sensitive info update (previously done)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut("sensitive/confirm")]
        public Task<IActionResult> UpdateSensitiveConfirm()
        {
            throw new NotImplementedException();
        }
    }
}