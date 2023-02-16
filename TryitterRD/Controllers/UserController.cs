
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
              try
              {
                ReadToken();

                User user = _userRepository.GetUserById(id);

                return Ok(new UserResponseDTO
                {
                      Name = user.Name,
                      Email = user.Email,
                      Status = user.Status
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

                if (getId == null || user.Email != getId?.Email)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponseDTO()
                    {
                        Description = "Wrong Body or User unauthorized to update",
                        Status = StatusCodes.Status400BadRequest
                    });
                }

                
                User userToUpdate = _userRepository.GetUserById(getId.UserId);

                if(userToUpdate == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ErrorResponseDTO()
                    {
                        Description = "User not found",
                        Status = StatusCodes.Status404NotFound
                    });
                }

                userToUpdate.Email = user.Email;
                userToUpdate.Name = user.Name;
                userToUpdate.Status = user.Status;


                User userUpdated = _userRepository.Update(userToUpdate);

                return Ok(new UserResponseDTO
                {
                    Name = userUpdated.Name,
                    Email = userUpdated.Email,
                    Status = userUpdated.Status
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
