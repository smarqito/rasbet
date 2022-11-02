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

        /// <summary>
        /// Logs in an user.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("login")] // id????
        public async Task<IActionResult> Login(int id, [FromBody] LoginUserDTO user)
        {
            SignInStatus sign_in_status = await userRepository.Login(user.Email, user.Password);

            switch (sign_in_status)
            {
                case SignInStatus.Success:
                    return Ok();
                case SignInStatus.Failure:
                    return BadRequest(); 
                default:
                    return Ok();
            } 

            return Ok()
        }
        
        /// <summary>
        /// Register an application user (better).
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("user")]
        public async Task<IActionResult> RegisterAppUser([FromBody] RegisterAppUserDTO registerApp)
        {
            try
            {
                await userRepository.RegisterAppUser(registerApp.Name,
                                                    registerApp.Email,
                                                    registerApp.Password,
                                                    registerApp.NIF,
                                                    registerApp.DOB,
                                                    registerApp.Notifications,
                                                    registerApp.Language);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        
        /// <summary>
        /// Register an Administrator.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDTO registerApp)
        {
            try
            {
                await userRepository.RegisterAdmin(registerApp.Name,
                                                registerApp.Email,
                                                registerApp.Password,
                                                registerApp.Language);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Register a Specialist.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("specialist")]
        public async Task<IActionResult> RegisterSpecialist([FromBody] RegisterSpecialistDTO registerApp)
        {
            try
            {
                await userRepository.RegisterSpecialist(registerApp.Name,
                                                          registerApp.Email,
                                                          registerApp.Password,
                                                          registerApp.Language);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieve UserDTO based on user id
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{id}")]
        public Task<UserDTO> GetUser(int id)
        {
            try { 
                User user = userRepository.GetUser(id);

                return userDTO(user.Name, user.Email, user.Language);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Update user general info
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut("user")]
        public async Task<IActionResult> UpdateAppUser([FromBody] AppUserDTO userDTO)
        {
            try { 
                await userRepository.UpdateAppUser(userDTO.email, userDTO.name, userDTO.language, userDTO.coin, userDTO.notifications);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update sensitive info
        ///     Expects to be confirmed by a code!
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut("sensitive")]
        public async Task<IActionResult> UpdateSensitive([FromBody] AppUserDTO userDTO)
        {
            try {   
                await userRepository.UpdateSensitive(userDTO.email, userDTO.IBAN, userDTO.NIF, userDTO.DOB, userDTO.PhoneNumber);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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