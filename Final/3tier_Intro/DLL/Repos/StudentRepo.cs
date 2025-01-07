using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class StudentRepo
    {
        UMSContext db;
        public StudentRepo()
        {
            db = new UMSContext();
        }

        public void Create(Student s)
        {
            db.Students.Add(s);
            db.SaveChanges();
        }

 
 

        public Student Get(int id)
        {
            return db.Students.Find(id);
        }

        public List<Student> Get()
        {
            return db.Students.ToList();
        }

        public void Delete(int id)
        {
            var exobj = db.Students.Find(id);
            if (exobj != null)
            {
                db.Students.Remove(exobj);
                db.SaveChanges();
            }
        }

    }
}