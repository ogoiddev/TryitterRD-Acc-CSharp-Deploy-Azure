using TryitterRD.Model;

namespace TryitterRD.Repository
{
    public interface IUserRepository
    {
        public void Save(User user);
        public bool CheckByEmail(string email);
        User GetUserByLoginDTO(string email, object password);
        User GetUserById(int id);
    }

}
