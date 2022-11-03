using Domain;
using DTO.LoginUserDTO;
using DTO.UserDTO;
using Microsoft.AspNet.Identity.Owin;
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
        /// <param name="user"> Information used to log in an user (e-mail and password).</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] LoginUserDTO user)
        {
            try
            {
                User u = await userRepository.Login(user.Email, 
                                                    user.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Logs out an user.
        /// </summary>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await userRepository.Logout();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Registers an application user (better).
        /// </summary>
        /// <param name="registerApp"> Information used to register in an user (name, e-mail, password,
        /// NIF, date of birth, preferred language and coin and whether the user wants to receive 
        /// notifications or not).</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
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
        /// Registers an Administrator.
        /// </summary>
        /// <param name="registerApp"> Information used to register an admin (name, e-mail, password and language).</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
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
        /// <param name="registerApp"> Information used to register in a specialist (name, e-mail, password and language).</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
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
        /// <param name="id"> Id of the user to be retrieved.</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try { 
               User user = await userRepository.GetUser(id);
                user.GetType();
               
               return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update user general info
        /// </summary>
        /// <param name="userDTO"> Information used to update in an user.</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("user")]
        public async Task<IActionResult> UpdateAppUser([FromBody] AppUserDTO userDTO)
        {
            try { 
                await userRepository.UpdateAppUser(userDTO.Email, userDTO.Name, userDTO.Language, userDTO.Coin, userDTO.Notifications);
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
        /// <param name="userDTO"> </param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("sensitive")]
        public async Task<IActionResult> UpdateAppUserSensitive([FromBody] UpdateInfo userDTO)
        {
            try {   
                await userRepository.UpdateAppUserSensitive(userDTO.Email, userDTO.Password, userDTO.IBAN, userDTO.PhoneNumber);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Confirm sensitive info update (previously done)
        /// </summary>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("sensitive/confirm")]
        public Task<IActionResult> UpdateSensitiveConfirm()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update specialist general info
        /// </summary>
        /// <param name="userDTO">Information needed to update specialist's profile.</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("specialist")]
        public async Task<IActionResult> UpdateSpecialist([FromBody] UserDTO userDTO)
        {
            try
            {
                await userRepository.UpdateSpecialist(userDTO.Email, userDTO.Name, userDTO.Language);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update administrator general info
        /// </summary>
        /// <param name="userDTO">Information needed to update admin's profile.</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("administrator")]
        public async Task<IActionResult> UpdateAdmin([FromBody] UserDTO userDTO)
        {
            try
            {
                await userRepository.UpdateAdmin(userDTO.Email, userDTO.Name, userDTO.Language);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}