using Microsoft.AspNetCore.Mvc;
using TryitterRD.Model;

namespace TryitterRD.Interfaces
{
    public interface IPostController
    {
        [HttpPost]
        IActionResult PostPost([FromBody] Post post);

        [HttpGet]
        IActionResult GetPostById(int id);


        [HttpPut]
        IActionResult UpdatePost([FromBody] Post post, int id);
        
        [HttpDelete]
        IActionResult DeletePost(int id);

    }

}
