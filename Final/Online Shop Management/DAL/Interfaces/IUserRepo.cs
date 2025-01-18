using DAL.EF.Tables;

namespace DAL.Interfaces
{
    public interface IUserRepo
    {
        User GetByUsername(string username);
        bool Create(User user);
    }
}
