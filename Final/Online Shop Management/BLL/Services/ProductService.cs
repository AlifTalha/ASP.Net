using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();
            });
            return new Mapper(config);
        }
        public static List<ProductDTO> Get()
        {
            var repo = DataAccessFactory.ProductData();
            return GetMapper().Map<List<ProductDTO>>(repo.Get());
        }
        public static ProductDTO Get(int id)
        {
            var repo = DataAccessFactory.ProductData();

            var Product = repo.Get(id);
            var ret = GetMapper().Map<ProductDTO>(Product);
            return ret;

        }
        public static List<ProductDTO> SearchByName(string name)
        {
            //var repo = DataAccessFactory.ProductData();
            //var data = repo.Get().Where(p => p.Name.Contains(name)).ToList();
            //return GetMapper().Map<List<ProductDTO>>(data);
            var repo = DataAccessFactory.ProductFeatures();
            return GetMapper().Map<List<ProductDTO>>(repo.SearchByName(name));

        }
    }
}
