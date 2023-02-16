
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

    public UserController(IUserRepository userRepository) : base(userRepository)
    {

    }

        [HttpGet]
        public IActionResult GetUser()
        {
              try
              {
                User user = ReadToken();

                return Ok(new UserResponseDTO
                {
                      Name = user.Name,
                      Email = user.Email
                });
              }
              catch
              {
                return StatusCode(StatusCodes.Status404NotFound);
              }

        }

        [HttpDelete]
        public IActionResult DeleteUser()
        {
            try
            {
                User user = ReadToken();

                if (user == null) throw new Exception();
                
                _userRepository.Delete(user.UserId);

                return Ok(new UserResponseDTO
                {
                    Name = user.Name,
                    Email = user.Email,
                    Status = "Usuario deletado"
                });

            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponseDTO()
                {
                    Description = "Ocorreu um erro ao deletar",
                    Status = StatusCodes.Status404NotFound
                });

            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            try
            {
                var getId = ReadToken();
                
                if (user.Email == null || getId == null) throw new Exception();

                user.Password = MD5Crypt.GenerateHashMD5(user.Password);

                User userToUpdate = new()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Status = "online"
                };

                _userRepository.Update(userToUpdate, getId.UserId);

                return Ok(new UserResponseDTO
                {
                    Name = userToUpdate.Name,
                    Email = userToUpdate.Email,
                    Status = "Usuario alterado",
                });

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponseDTO()
                {
                    Description = "Ocorreu um erro - Usuario nao alterado" + ex,
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

                    if (string.IsNullOrEmpty(user.Name) || string.IsNullOrWhiteSpace(user.Name))
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
                        user.Password = MD5Crypt.GenerateHashMD5(user.Password);
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

                }

                user.Password = "Usuario salvo com sucesso";
                return Ok(user);
            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponseDTO()
                {
                    Description = "Ocorreu um erro:",
                    Status = StatusCodes.Status404NotFound
                });

            }
        }
    }



}
