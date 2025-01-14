using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class CategoryRepo : Repo, IRepo<Category, int, Category>
    {
        public Category Create(Category obj)
        {
            db.Categories.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var ex = Get(id);
            db.Categories.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public List<Category> Get()
        {
            return db.Categories.ToList();
        }

        public Category Update(Category obj)
        {
            var ex = Get(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            db.SaveChanges();
            return ex;
        }
    }
}
