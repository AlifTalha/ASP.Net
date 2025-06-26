using DAL.EF;
using System.Collections.Generic;

namespace DAL.Repos
{
    public interface IMenuItemRepository
    {
        MenuItem Create(MenuItem item);
        bool Delete(int id);
        IEnumerable<MenuItem> GetAll();
        MenuItem GetById(int id);
        IEnumerable<MenuItem> GetByCategory(string category);
        IEnumerable<MenuItem> GetAvailableItems();
        bool Update(MenuItem item);

        IEnumerable<MenuItem> Search(string name, decimal? minPrice, decimal? maxPrice);

    }
}