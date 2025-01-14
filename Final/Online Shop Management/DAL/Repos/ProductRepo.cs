using DAL.Interfaces;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class ProductRepo : Repo, IRepo<Product, int, bool>, IProductFeatures
    {
        public bool Create(Product obj)
        {
            db.Products.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var ex = Get(id);
            db.Products.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public List<Product> Get()
        {
            return db.Products.ToList();
        }

        public List<Product> SearchByName(string name)
        {
            return db.Products.Where(x => x.Name.Contains(name)).ToList();
        }

        public bool Update(Product obj)
        {
            var ex = Get(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;

        }
    }
}
