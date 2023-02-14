
using System.ComponentModel.DataAnnotations.Schema;

namespace TryitterRD.Model
{
  public class User
  {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Status { get; set; } = "offline";

 
        public List<Post>? Posts { get; set; }
  }
}
