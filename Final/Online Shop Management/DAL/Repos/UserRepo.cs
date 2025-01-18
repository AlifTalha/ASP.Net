using DAL.EF.Tables;
using DAL.Interfaces;
using System.Linq;

namespace DAL.Repos
{
    internal class UserRepo : Repo, IUserRepo
    {
        public User GetByUsername(string username)
        {
            return db.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool Create(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges() > 0;
        }
    }
}
