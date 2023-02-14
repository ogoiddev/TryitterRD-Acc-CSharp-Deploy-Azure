
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
        public LoginController(IUserRepository userRepository) : base(userRepository)
        {

        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO loginRequest)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginRequest.Password) && !string.IsNullOrEmpty(loginRequest.Email) &&
                !string.IsNullOrWhiteSpace(loginRequest.Password) && !string.IsNullOrWhiteSpace(loginRequest.Email))
                {
                    User user = _userRepository.GetUserByLoginDTO(loginRequest.Email, MD5Crypt.GenerateHashMD5(loginRequest.Password));
                    if (user != null)
                    {
                        User userStatus = new() 
                        { 
                            Name = user.Name, 
                            Email = user.Email,
                            Password = user.Password,
                            Status = "online" 
                        };

                        _userRepository.Update(userStatus, user.UserId);


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

                return Ok(loginRequest);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO()
                {
                    Description = "Login error alert",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
