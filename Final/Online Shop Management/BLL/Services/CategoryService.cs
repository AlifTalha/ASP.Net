using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF.Tables;
using System.Collections.Generic;

namespace BLL.Services
{
    public class CategoryService
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
            });
            return new Mapper(config);
        }

        public static List<CategoryDTO> Get()
        {
            var repo = DataAccessFactory.CategoryData();
            return GetMapper().Map<List<CategoryDTO>>(repo.Get());
        }

        public static CategoryDTO Get(int id)
        {
            var repo = DataAccessFactory.CategoryData();
            var category = repo.Get(id);
            return GetMapper().Map<CategoryDTO>(category);
        }

        public static CategoryDTO GetwithProducts(int id)
        {
            var repo = DataAccessFactory.CategoryData();
            var category = repo.Get(id);
            return GetMapper().Map<CategoryDTO>(category);
        }
    }
}
