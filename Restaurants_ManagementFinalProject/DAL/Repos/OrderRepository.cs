using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private readonly UMSContext _db;

        public OrderRepository()
        {
            _db = new UMSContext();
        }

        public Order Create(Order order)
        {
            order.OrderTime = DateTime.Now;
            _db.Orders.Add(order);
            _db.SaveChanges();
            return order;
        }

        public bool Delete(int id)
        {
            var order = _db.Orders.Find(id);
            if (order == null) return false;

            _db.Orders.Remove(order);
            return _db.SaveChanges() > 0;
        }

        public IEnumerable<Order> GetAll()
        {
            return _db.Orders.Include("Table").ToList();
        }

        public Order GetById(int id)
        {
            return _db.Orders.Include("Table").FirstOrDefault(o => o.OrderId == id);
        }

        public IEnumerable<Order> GetByTable(int tableId)
        {
            return _db.Orders.Include("Table")
                            .Where(o => o.TableId == tableId)
                            .ToList();
        }

        public IEnumerable<Order> GetUnpaidOrders()
        {
            return _db.Orders.Include("Table")
                            .Where(o => !o.IsPaid)
                            .ToList();
        }

        public bool Update(Order order)
        {
            var existing = _db.Orders.Find(order.OrderId);
            if (existing == null) return false;

            _db.Entry(existing).CurrentValues.SetValues(order);
            return _db.SaveChanges() > 0;
        }

        public bool MarkAsPaid(int orderId)
        {
            var order = _db.Orders.Find(orderId);
            if (order == null) return false;

            order.IsPaid = true;
            return _db.SaveChanges() > 0;
        }

        public IEnumerable<Order> SearchOrders(int? tableId, bool? isPaid, DateTime? fromDate, DateTime? toDate)
        {
            var query = _db.Orders.Include("Table").AsQueryable();

            if (tableId.HasValue)
                query = query.Where(o => o.TableId == tableId.Value);

            if (isPaid.HasValue)
                query = query.Where(o => o.IsPaid == isPaid.Value);

            if (fromDate.HasValue)
                query = query.Where(o => o.OrderTime >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(o => o.OrderTime <= toDate.Value);

            return query.ToList();
        }

    }
}