using TryitterRD.Model;

namespace TryitterRD.Repository
{
    public interface IUserRepository
    {
        public void Save(User user);
        public void Delete(int id);
   
        public User Update(User user);
        public bool CheckByEmail(string email);
        User GetUserByLoginDTO(string email, object password);
        User GetUserById(int userId);
    }

}
