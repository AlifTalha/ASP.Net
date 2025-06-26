using AutoMapper;
using BLL.DTOs;
using DAL.EF;
using DAL.Repos;
using System.Collections.Generic;

namespace BLL.Services
{
    public class MenuItemService
    {
        private static IMapper _mapper;

        static MenuItemService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MenuItem, MenuItemDTO>();
                cfg.CreateMap<MenuItemDTO, MenuItem>();
              

            });
            _mapper = new Mapper(config);
        }

        public static List<MenuItemDTO> GetAll()
        {
            var repo = new MenuItemRepository();
            var data = repo.GetAll();
            return _mapper.Map<List<MenuItemDTO>>(data);
        }

        public static MenuItemDTO GetById(int id)
        {
            var repo = new MenuItemRepository();
            var data = repo.GetById(id);
            return _mapper.Map<MenuItemDTO>(data);
        }

        public static bool Create(MenuItemDTO item)
        {
            var repo = new MenuItemRepository();
            var data = _mapper.Map<MenuItem>(item);
            return repo.Create(data) != null;
        }

        public static bool Update(MenuItemDTO item)
        {
            var repo = new MenuItemRepository();
            var data = _mapper.Map<MenuItem>(item);
            return repo.Update(data);
        }

        public static bool Delete(int id)
        {
            var repo = new MenuItemRepository();
            return repo.Delete(id);
        }

        public static List<MenuItemDTO> GetByCategory(string category)
        {
            var repo = new MenuItemRepository();
            var data = repo.GetByCategory(category);
            return _mapper.Map<List<MenuItemDTO>>(data);
        }

        public static List<MenuItemDTO> GetAvailableItems()
        {
            var repo = new MenuItemRepository();
            var data = repo.GetAvailableItems();
            return _mapper.Map<List<MenuItemDTO>>(data);
        }

        public static List<MenuItemDTO> Search(string name, decimal? minPrice, decimal? maxPrice)
        {
            var repo = new MenuItemRepository();
            var data = repo.Search(name, minPrice, maxPrice);
            return _mapper.Map<List<MenuItemDTO>>(data);
        }

    }
}