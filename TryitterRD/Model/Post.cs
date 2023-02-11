using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TryitterRD.Model
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; } = default!;
    }
}
