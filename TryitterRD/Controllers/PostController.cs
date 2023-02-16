using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TryitterRD.Dtos;
using TryitterRD.Interfaces;
using TryitterRD.Model;
using TryitterRD.Repository;
using System.Security.Claims;

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

        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            var post = _postRepository.GetPostById(id);

            if (post == null)
            {
                return NotFound(new ErrorResponseDTO()
                {
                    Description = "Post not found",
                    Status = StatusCodes.Status404NotFound
                });
            }

            return Ok(post);
        }

        [HttpPut]
        public IActionResult UpdatePost([FromBody] Post post)
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

            var postToUpdate = _postRepository.GetPostById(userChecked.UserId);

            if (postToUpdate == null)
            {
                return NotFound(new ErrorResponseDTO()
                {
                    Description = "Post not found",
                    Status = StatusCodes.Status404NotFound
                });
            }

            if (postToUpdate.UserId != userChecked.UserId)
            {
                return Unauthorized(new ErrorResponseDTO()
                {
                    Description = "User unauthorized to update this post",
                    Status = StatusCodes.Status401Unauthorized
                });
            }

            postToUpdate.Content = post.Content;
            postToUpdate.Title = post.Title;

            _postRepository.Update(postToUpdate);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize(postToUpdate, options);

            return Ok(json.ToString());

        }
    }
}
