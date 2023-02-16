
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TryitterRD.Model
{
  public class User
  {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Status { get; set; } = "offline";


        [InverseProperty("User")]
        public virtual ICollection<Post>? Posts { get; } = default!;
    }
}
