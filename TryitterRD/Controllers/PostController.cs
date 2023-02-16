using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TryitterRD.Dtos;
using TryitterRD.Interfaces;
using TryitterRD.Model;
using TryitterRD.Repository;
using System.Security.Claims;
using TryitterRD.Repository.Implementation;

namespace TryitterRD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase, IPostController
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
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

        [HttpPost]
        public IActionResult PostPost([FromBody] Post post) 
        {
            var userChecked = ReadToken();

            if (post == null || userChecked == null || string.IsNullOrWhiteSpace(post.Title) || string.IsNullOrEmpty(post.Title)
                || string.IsNullOrWhiteSpace(post.Content) || string.IsNullOrEmpty(post.Content))
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    Description = "User wrong or Body wrong params",
                    Status = StatusCodes.Status400BadRequest
                });
            }

            Post newPost = new()
            {
                Content = post.Content,
                Title = post.Title,
                User = userChecked,
            };

            _postRepository.Save(newPost);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // outras opções, se necessário
            };

            var json = JsonSerializer.Serialize(newPost, options);

            return Ok(post);
        }

    }
}
