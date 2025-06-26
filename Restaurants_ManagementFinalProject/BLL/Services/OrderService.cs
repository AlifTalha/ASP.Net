using AutoMapper;
using BLL.DTOs;
using DAL.EF;
using DAL.Repos;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class OrderService
    {
        private static IMapper _mapper;

        static OrderService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<OrderDTO, Order>();
            });
            _mapper = new Mapper(config);
        }

        public static List<OrderDTO> GetAll()
        {
            var repo = new OrderRepository();
            var data = repo.GetAll();
            return _mapper.Map<List<OrderDTO>>(data);
        }

        public static OrderDTO GetById(int id)
        {
            var repo = new OrderRepository();
            var data = repo.GetById(id);
            return _mapper.Map<OrderDTO>(data);
        }

        public static List<OrderDTO> GetByTable(int tableId)
        {
            var repo = new OrderRepository();
            var data = repo.GetByTable(tableId);
            return _mapper.Map<List<OrderDTO>>(data);
        }

        public static List<OrderDTO> GetUnpaidOrders()
        {
            var repo = new OrderRepository();
            var data = repo.GetUnpaidOrders();
            return _mapper.Map<List<OrderDTO>>(data);
        }

        public static bool Create(OrderDTO order)
        {
            var repo = new OrderRepository();
            var data = _mapper.Map<Order>(order);
            return repo.Create(data) != null;
        }

        public static bool Update(OrderDTO order)
        {
            var repo = new OrderRepository();
            var data = _mapper.Map<Order>(order);
            return repo.Update(data);
        }

        public static bool Delete(int id)
        {
            var repo = new OrderRepository();
            return repo.Delete(id);
        }

        public static bool MarkAsPaid(int orderId)
        {
            var repo = new OrderRepository();
            return repo.MarkAsPaid(orderId);
        }

        public static List<OrderDTO> SearchOrders(int? tableId, bool? isPaid, DateTime? fromDate, DateTime? toDate)
        {
            var repo = new OrderRepository();
            var data = repo.SearchOrders(tableId, isPaid, fromDate, toDate);
            return _mapper.Map<List<OrderDTO>>(data);
        }

    }
}