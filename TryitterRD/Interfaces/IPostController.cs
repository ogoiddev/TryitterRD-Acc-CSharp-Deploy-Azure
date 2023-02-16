using Microsoft.AspNetCore.Mvc;
using TryitterRD.Model;

namespace TryitterRD.Interfaces
{
    public interface IPostController
    {
        [HttpPost]
        IActionResult PostPost([FromBody] Post post);

    }

}
