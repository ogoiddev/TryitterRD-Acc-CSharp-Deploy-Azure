using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TryitterRD.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Status { get; set; } = default!;

        [InverseProperty("User")]
        public virtual ICollection<Post> Posts { get; } = default!;
    }
}
