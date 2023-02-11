
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryitterRD.Dtos;
using TryitterRD.Model;
using TryitterRD.Repository;
using TryitterRD.Utils;

namespace TryitterRD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public readonly ILogger<UserController> _logger;
        public readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
                User user = BaseController.;

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro Login wrong");
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponseDTO()
                {
                    Description = ex.Message,
                    Status = StatusCodes.Status404NotFound
                });
            }
            
        }    

    [HttpPost]
        [AllowAnonymous]
        public IActionResult PostUser([FromBody] User user)
        {
            try
            {
                if (user != null)
                {
                    var errors = new List<string>();

                    if(string.IsNullOrEmpty(user.Name) || string.IsNullOrWhiteSpace(user.Name))
                    {
                        errors.Add("Invalid Name");
                    }
                    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
                    {
                        errors.Add("Invalid Email");
                    }
                    if (string.IsNullOrEmpty(user.Password) || string.IsNullOrWhiteSpace(user.Password))
                    {
                        errors.Add("Invalid Password");
                    }

                    if (errors.Count > 0)
                    {
                        return BadRequest(new ErrorResponseDTO()
                        {
                            Errors = errors,
                            Status = StatusCodes.Status400BadRequest
                        });
                    }

                    if (!_userRepository.CheckByEmail(user.Email))
                    {
                        _userRepository.Save(user);
                    }
                    else
                    {
                        return BadRequest(new ErrorResponseDTO()
                        {
                            Description = "User already exist",
                            Status = StatusCodes.Status400BadRequest
                        });
                    }

                    user.Password = MD5Crypt.GenerateHashMD5(user.Password);
                }

                user.Password = "Usuario salvo com sucesso";
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao salvar new User");
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponseDTO()
                {
                    Description = "Ocorreu um erro:" + ex.Message,
                    Status = StatusCodes.Status404NotFound
                });

            }
        }
    }

}
