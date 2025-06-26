using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly UMSContext _db;

        public MenuItemRepository()
        {
            _db = new UMSContext();
        }

        public MenuItem Create(MenuItem item)
        {
            _db.MenuItems.Add(item);
            _db.SaveChanges();
            return item;
        }

        public bool Delete(int id)
        {
            var item = _db.MenuItems.Find(id);
            if (item == null) return false;

            _db.MenuItems.Remove(item);
            return _db.SaveChanges() > 0;
        }

        public IEnumerable<MenuItem> GetAll()
        {
            return _db.MenuItems.ToList();
        }

        public MenuItem GetById(int id)
        {
            return _db.MenuItems.Find(id);
        }

        public IEnumerable<MenuItem> GetByCategory(string category)
        {
            return _db.MenuItems
                .Where(m => m.Category == category && m.IsAvailable)
                .ToList();
        }

        public IEnumerable<MenuItem> GetAvailableItems()
        {
            return _db.MenuItems.Where(m => m.IsAvailable).ToList();
        }

        public bool Update(MenuItem item)
        {
            var existing = _db.MenuItems.Find(item.MenuItemId);
            if (existing == null) return false;

            _db.Entry(existing).CurrentValues.SetValues(item);
            return _db.SaveChanges() > 0;
        }

        public IEnumerable<MenuItem> Search(string name, decimal? minPrice, decimal? maxPrice)
        {
            var query = _db.MenuItems.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(m => m.Name.Contains(name));

            if (minPrice.HasValue)
                query = query.Where(m => m.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(m => m.Price <= maxPrice.Value);

            return query.ToList();
        }

    }
}