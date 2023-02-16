
using Microsoft.EntityFrameworkCore;
using TryitterRD.Model;

namespace TryitterRD.Repository.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly TryitterRDContext _context;

        public PostRepository(TryitterRDContext context)
        {
            _context = context;
        }
        
        public void Save(Post post)
        {
            _context.Add(post);
            _context.SaveChanges();
        }

        public Post GetPostById(int postId)
        {
            return _context.Posts.FirstOrDefault(post => post.PostId == postId);
        }

        public void Delete(int id)
        {
            var postToDelete = _context.Posts.FirstOrDefault(postData => postData.UserId == id);
            
            if (postToDelete == null) throw new Exception();

            _context.Posts.Remove(postToDelete);
            _context.SaveChanges();
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
    }
}
