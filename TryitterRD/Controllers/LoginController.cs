
using TryitterRD.Dtos;
using TryitterRD.Model;
using TryitterRD.Repository;
using TryitterRD.Utils;
using TryitterRD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TryitterRD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IUserRepository userRepository) : base(userRepository)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO loginRequest)
        {
            try
            {
                if (!String.IsNullOrEmpty(loginRequest.Password) && !String.IsNullOrEmpty(loginRequest.Email) &&
                    !String.IsNullOrWhiteSpace(loginRequest.Password) && !String.IsNullOrWhiteSpace(loginRequest.Email))
                {
                    User user = _userRepository.GetUserByLoginDTO(loginRequest.Email.ToLower(), MD5Crypt.GenerateHashMD5(loginRequest.Password));

                    if(user != null)
                    {
                        return Ok(new LoginResponseDTO()
                        {
                            Email = user.Email,
                            Name = user.Name,
                            Token = TokenService.GenerateToken(user)
                        });

                    }
                    else
                    {
                        return BadRequest(new ErrorResponseDTO()
                        {
                            Description = "Wrong data, check your Password or Email",
                            Status = StatusCodes.Status500InternalServerError
                        });
                    }
                    
                }
                else
                {
                    return BadRequest(new ErrorResponseDTO()
                    {
                        Description = "Form fill is empty or null",
                        Status = StatusCodes.Status500InternalServerError
                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error:" + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO()
                {
                    Description = "Login error alert",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
