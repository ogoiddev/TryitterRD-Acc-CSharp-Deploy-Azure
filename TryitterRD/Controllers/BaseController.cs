using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryitterRD.Model;
using TryitterRD.Repository;

namespace TryitterRD.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected readonly IUserRepository _userRepository;

        public BaseController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected User? ReadToken()
        {
            var userSidId = User.Claims.Where(claim => claim.Type == ClaimTypes.Sid).Select(claim => claim.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(userSidId))
            {
                return null;
            }
            else
            {
                return _userRepository.GetUserById(int.Parse(userSidId));
            }
        }
    }
}
