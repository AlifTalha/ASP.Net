using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly UMSContext _db = new UMSContext();


        public User GetByUsername(string username) =>
            _db.Users.FirstOrDefault(u => u.Username == username);

        public User GetById(int id) =>
            _db.Users.Find(id);

        public IEnumerable<User> GetAll() =>
            _db.Users.ToList();

        public User Register(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public bool Exists(string username) =>
            _db.Users.Any(u => u.Username == username);
    }
}
