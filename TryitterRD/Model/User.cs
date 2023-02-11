using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TryitterRD.Model
{
  public class User
  {
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Post> Posts { get; }
  }
}
