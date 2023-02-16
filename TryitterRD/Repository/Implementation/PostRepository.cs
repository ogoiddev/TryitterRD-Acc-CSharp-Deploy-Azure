
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
        void IPostRepository.Save(Post post)
        {
            _context.Add(post);
            _context.SaveChanges();
        }

        public Post GetPostById(int postId)
        {
            return _context.Posts.FirstOrDefault(post => post.PostId == postId);
        }

        void IPostRepository.Delete(int id)
        {
            var postToDelete = _context.Posts.FirstOrDefault(postData => postData.UserId == id);
            
            if (postToDelete == null) throw new Exception();

            _context.Posts.Remove(postToDelete);
            _context.SaveChanges();
        }

        void IPostRepository.Update(Post post, int id)
        {
            var postToUpdate = _context.Posts.FirstOrDefault(postData => postData.UserId == id);

            if (postToUpdate == null) throw new Exception();

            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            postToUpdate.UserId = id;

            _context.SaveChanges();
        }
    }
}
