using DAL.EF;
using System.Collections.Generic;

namespace DAL.Repos
{
    public interface ITableRepository
    {
        Table Create(Table table);
        bool Delete(int id);
        IEnumerable<Table> GetAll();
        Table GetById(int id);
        Table GetByTableNumber(int tableNumber);
        IEnumerable<Table> GetAvailableTables();
        bool Update(Table table);
    }
}