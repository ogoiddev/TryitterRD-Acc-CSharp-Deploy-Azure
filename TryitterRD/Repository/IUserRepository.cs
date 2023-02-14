using TryitterRD.Model;

namespace TryitterRD.Repository
{
    public interface IUserRepository
    {
        public void Save(User user);
        public void Delete(User user);
        public void Update(User user, int id);
        public bool CheckByEmail(string email);
        User GetUserByLoginDTO(string email, object password);
        User GetUserById(int userId);
    }

}
