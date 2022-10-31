using Domain;
using DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Interfaces;

namespace UserAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IAdminRepository adminRepository;
        private readonly IAppUserRepository appUserRepository;
        private readonly ISpecialistRepository specialistRepository;
        private readonly IUserRepository userRepository;


        public UserController(IAdminRepository adminRepository, IAppUserRepository appUserRepository, ISpecialistRepository specialistRepository, IUserRepository userRepository)
        {
            this.adminRepository = adminRepository;
            this.appUserRepository = appUserRepository;
            this.specialistRepository = specialistRepository;
            this.userRepository = userRepository;
        }

        [HttpGet("login")] // id????
        public async Task<IActionResult> Login(int id, [FromBody] LoginUserDTO user)
        {
            await userRepository.Login(user.Email, user.Password);

            return Ok();
        }

        [HttpPost("user")]
        public async Task<IActionResult> RegisterAppUser([FromBody] RegisterAppUserDTO registerApp)
        {
            await appUserRepository.RegisterAppUser(registerApp.Name,
                                                    registerApp.Email,
                                                    registerApp.Password, 
                                                    registerApp.NIF,
                                                    registerApp.DOB,
                                                    registerApp.Notifications,
                                                    registerApp.Language);
            return Ok();
        }

        [HttpPost("admin")]
        public Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDTO registerApp)
        {
            await adminRepository.RegisterAdmin(registerApp.Name,
                                                registerApp.Email,
                                                registerApp.Password,
                                                registerApp.Language);
            return Ok();
        }

        [HttpPost("specialist")]
        public Task<IActionResult> RegisterSpecialist([FromBody] RegisterSpecialistDTO registerApp)
        {
            await specialistRepository.RegisterSpecialist(registerApp.Name,
                                                          registerApp.Email,
                                                          registerApp.Password,
                                                          registerApp.Language);
            return Ok();
        }

        /// <summary>
        /// Retrieve UserDTO based on user id
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet()]
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
        [HttpPut("user")]
        public async Task<IActionResult> Update(object userDTO)
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