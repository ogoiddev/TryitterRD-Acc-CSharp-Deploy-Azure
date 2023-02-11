using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryitterRD.Controllers;
using TryitterRD.Model;
using TryitterRD.Repository;

namespace TryitterRD.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public BaseController(ILogger<LoginController> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected User ReadToken(string token)
        {
            var userId = User.Claims.Where(claim => claim.Type == ClaimTypes.Sid).Select(claim => claim.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            else
            {
                return _userRepository.GetUserById(int.Parse(userId));
            }
        }
    }
}
