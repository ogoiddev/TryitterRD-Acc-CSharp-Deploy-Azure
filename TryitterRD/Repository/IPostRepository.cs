using TryitterRD.Model;

namespace TryitterRD.Repository
{
    public interface IPostRepository
    {
        public void Save(Post post);
        public Post GetPostById(int id);
        public void Delete(int id);
        public void Update(Post post);
    }
}
