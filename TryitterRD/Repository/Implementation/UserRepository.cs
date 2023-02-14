using TryitterRD.Model;

namespace TryitterRD.Repository.Implementation
{
  public class UserRepository : IUserRepository
  {
    private readonly TryitterRDContext _context;
    public UserRepository(TryitterRDContext context)
    {
      _context = context;
    }

    public void Save(User user)
    {
      _context.Add(user);
      _context.SaveChanges();
    }

    public bool CheckByEmail(string email)
    {
      return _context.Users.Any(user => user.Email.ToLower() == email.ToLower());
    }

    public User GetUserByLoginDTO(string email, object password)
    {
      return _context.Users.FirstOrDefault(user => user.Email == email && user.Password == password);
    }

    public User GetUserById(int userId)
    {
      return _context.Users.FirstOrDefault(user => user.UserId == userId);
    }
  }
}
