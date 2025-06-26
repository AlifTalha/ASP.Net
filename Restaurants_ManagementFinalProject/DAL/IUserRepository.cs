using DAL.EF;
using System.Collections.Generic;

namespace DAL.Repos
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        User GetById(int id);
        IEnumerable<User> GetAll();
        User Register(User user);
        bool Exists(string username);
    }
}
