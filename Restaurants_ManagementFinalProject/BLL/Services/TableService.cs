using AutoMapper;
using BLL.DTOs;
using DAL.EF;
using DAL.Repos;
using System.Collections.Generic;

namespace BLL.Services
{
    public class TableService
    {
        private static IMapper _mapper;

        static TableService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Table, TableDTO>();
                cfg.CreateMap<TableDTO, Table>();
            });
            _mapper = new Mapper(config);
        }

        public static List<TableDTO> GetAll()
        {
            var repo = new TableRepository();
            var data = repo.GetAll();
            return _mapper.Map<List<TableDTO>>(data);
        }

        public static TableDTO GetById(int id)
        {
            var repo = new TableRepository();
            var data = repo.GetById(id);
            return _mapper.Map<TableDTO>(data);
        }

        public static TableDTO GetByTableNumber(int tableNumber)
        {
            var repo = new TableRepository();
            var data = repo.GetByTableNumber(tableNumber);
            return _mapper.Map<TableDTO>(data);
        }

        public static bool Create(TableDTO table)
        {
            var repo = new TableRepository();
            var data = _mapper.Map<Table>(table);
            return repo.Create(data) != null;
        }

        public static bool Update(TableDTO table)
        {
            var repo = new TableRepository();
            var data = _mapper.Map<Table>(table);
            return repo.Update(data);
        }

        public static bool Delete(int id)
        {
            var repo = new TableRepository();
            return repo.Delete(id);
        }

        public static List<TableDTO> GetAvailableTables()
        {
            var repo = new TableRepository();
            var data = repo.GetAvailableTables();
            return _mapper.Map<List<TableDTO>>(data);
        }
    }
}