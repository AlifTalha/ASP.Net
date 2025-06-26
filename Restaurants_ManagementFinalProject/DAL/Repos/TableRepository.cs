using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class TableRepository : ITableRepository
    {
        private readonly UMSContext _db;

        public TableRepository()
        {
            _db = new UMSContext();
        }

        public Table Create(Table table)
        {
            _db.Tables.Add(table);
            _db.SaveChanges();
            return table;
        }

        public bool Delete(int id)
        {
            var table = _db.Tables.Find(id);
            if (table == null) return false;

            _db.Tables.Remove(table);
            return _db.SaveChanges() > 0;
        }

        public IEnumerable<Table> GetAll()
        {
            return _db.Tables.ToList();
        }

        public Table GetById(int id)
        {
            return _db.Tables.Find(id);
        }

        public Table GetByTableNumber(int tableNumber)
        {
            return _db.Tables.FirstOrDefault(t => t.TableNumber == tableNumber);
        }

        public IEnumerable<Table> GetAvailableTables()
        {
            return _db.Tables.Where(t => !t.IsOccupied).ToList();
        }

        public bool Update(Table table)
        {
            var existing = _db.Tables.Find(table.TableId);
            if (existing == null) return false;

            _db.Entry(existing).CurrentValues.SetValues(table);
            return _db.SaveChanges() > 0;
        }
    }
}