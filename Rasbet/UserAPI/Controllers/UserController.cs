using Domain;
using Domain.UserDomain;
using DTO.GameOddDTO;
using DTO.LoginUserDTO;
using DTO.UserDTO;
using Microsoft.AspNetCore.Authorization;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO user)
        {
            try
            {
                UserDTO u = await userRepository.Login(user.Email, 
                                                    user.Password);
                return Ok(u);
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
                                                     registerApp.PasswordRepeated,
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
        [HttpGet("appuser/{id}")]
        public async Task<AppUserDTO> GetAppUser(string id)
        {
            try { 
               AppUser user = await userRepository.GetAppUser(id);
               AppUserDTO dto = new AppUserDTO(user.Name, 
                                               user.Language, 
                                               user.Email, 
                                               user.IBAN, 
                                               user.NIF, 
                                               user.DOB,
                                               user.PhoneNumber,
                                               user.Notifications,
                                               user.Coin);
               return dto;
            }
            catch (Exception e)
            {
                return null;
                //throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retrieve UserDTO based on user id
        /// </summary>
        /// <param name="id"> Id of the user to be retrieved.</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpGet("specialist/{id}")]
        public async Task<UserDTO> GetSpecialist(string id)
        {
            try
            {
                Specialist user = await userRepository.GetSpecialist(id);
                UserDTO dto = new UserDTO(user.Id, user.Name, user.Email, user.Language);
                return dto;
            }
            catch (Exception e)
            {
                return null;
                //throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retrieve UserDTO based on user id
        /// </summary>
        /// <param name="id"> Id of the user to be retrieved.</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpGet("admin/{id}")]
        public async Task<UserDTO> GetAdmin(string id)
        {
            try
            {
                Admin user = await userRepository.GetAdmin(id);
                UserDTO dto = new UserDTO(user.Id, user.Name, user.Email, user.Language);
                return dto;
            }
            catch (Exception e)
            {
                return null;
                //throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Update user general info
        /// </summary>
        /// <param name="userDTO"> Information used to update in an user.</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("update/user")]
        public async Task<IActionResult> UpdateAppUser([FromBody] UpdateAppUserDTO userDTO)
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
        /// Update sensitive info from AppUser.
        /// </summary>
        /// <param name="updateInfo"> </param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("sensitive/user")]
        [Authorize(Roles ="AppUser")]
        public async Task<IActionResult> UpdateAppUserSensitive([FromBody] SensitiveAppUserDTO updateInfo){
            try {   
                await userRepository.UpdateAppUserSensitive(updateInfo.Email, updateInfo.Password, updateInfo.IBAN, updateInfo.PhoneNumber);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update sensitive info from Admin.
        /// </summary>
        /// <param name="updateInfo"> </param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("sensitive/admin")]
        public async Task<IActionResult> UpdateAdminSensitive([FromBody] UpdatePasswordDTO updateInfo)
        {
            try {   
                await userRepository.UpdateAdminSensitive(updateInfo.Email, updateInfo.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update sensitive info from Specialist.
        /// </summary>
        /// <param name="updateInfo"> </param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("sensitive/specialist")]
        public async Task<IActionResult> UpdateSpecialistSensitive([FromBody] UpdatePasswordDTO updateInfo)
        {
            try {   
                await userRepository.UpdateSpecialistSensitive(updateInfo.Email, updateInfo.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Confirm sensitive info update (previously done) from AppUser.
        /// </summary>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("sensitive/user/confirm")]
        public async Task<IActionResult> UpdateAppUserSensitiveConfirm([FromBody] ConfirmationDTO c)
        {
            try{
                await userRepository.UpdateAppUserSensitiveConfirm(c.Email, c.Code);
                return Ok();
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Confirm sensitive info update (previously done) from Admin.
        /// </summary>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("sensitive/admin/confirm")]
        public async Task<IActionResult> UpdateAdminSensitiveConfirm([FromBody] ConfirmationDTO c)
        {
            try{
                await userRepository.UpdateAdminSensitiveConfirm(c.Email, c.Code);
                return Ok();
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Confirm sensitive info update (previously done) from Specialist.
        /// </summary>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("sensitive/specialist/confirm")]
        public async Task<IActionResult> UpdateSpecialistSensitiveConfirm([FromBody] ConfirmationDTO c)
        {
            try{
                await userRepository.UpdateSpecialistSensitiveConfirm(c.Email, c.Code);
                return Ok();
            } catch(Exception e) {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update specialist general info
        /// </summary>
        /// <param name="userDTO">Information needed to update specialist's profile.</param>
        /// <returns>Ok(), if everything worked as planned. BadRequest(), otherwise.</returns>
        [HttpPut("update/specialist")]
        public async Task<IActionResult> UpdateSpecialist([FromBody] UpdateSpecialistDTO userDTO)
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
        [HttpPut("update/admin")]
        public async Task<IActionResult> UpdateAdmin([FromBody] UpdateAdminDTO userDTO)
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