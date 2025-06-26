using DAL.EF;
using System;
using System.Collections.Generic;

namespace DAL.Repos
{
    public interface IOrderRepository
    {
        Order Create(Order order);
        bool Delete(int id);
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        IEnumerable<Order> GetByTable(int tableId);
        IEnumerable<Order> GetUnpaidOrders();
        bool Update(Order order);
        bool MarkAsPaid(int orderId);

        IEnumerable<Order> SearchOrders(int? tableId, bool? isPaid, DateTime? fromDate, DateTime? toDate);

    }
}