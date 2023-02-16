using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TryitterRD.Dtos;
using TryitterRD.Model;
using TryitterRD.Repository;
using TryitterRD.Repository.Implementation;

namespace TryitterRD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;

        }


        [HttpPost]
        public IActionResult PostPost([FromBody] Post post)
        {

            if (post == null || string.IsNullOrWhiteSpace(post.Title) || string.IsNullOrEmpty(post.Title)
                || string.IsNullOrWhiteSpace(post.Content) || string.IsNullOrEmpty(post.Content))
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    Description = "User wrong or Body wrong params",
                    Status = StatusCodes.Status400BadRequest
                });
            }

            _postRepository.Save(post);

            return Ok(post);
        }

    }
}
