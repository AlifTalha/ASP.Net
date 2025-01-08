using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class PersonRepo
    {
        private readonly UMSContext db;

        // Constructor initializes the context
        public PersonRepo()
        {
            db = new UMSContext();
        }

        // Fetch all Persons from the database
        public List<Person> GetAll()
        {
            return db.Persons.ToList();
        }

        // Fetch a single Person by ID
        public Person GetById(int id)
        {
            return db.Persons.Find(id);
        }

        // Add a new Person to the database
        public void Create(Person person)
        {
            db.Persons.Add(person);
            db.SaveChanges();
        }

        // Delete a Person by ID
        public void Delete(int id)
        {
            var person = db.Persons.Find(id);
            if (person != null)
            {
                db.Persons.Remove(person);
                db.SaveChanges();
            }
        }
    }
}
