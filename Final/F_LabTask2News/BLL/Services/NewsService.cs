
using AutoMapper;
using BLL.DTOs;
using DAL.EF;
using DAL.Repos;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class NewsService
    {
        private static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<News, NewsDTO>().ReverseMap();
        });

        private static Mapper mapper = new Mapper(config);

        public static List<NewsDTO> GetAll()
        {
            var repo = new NewsRepo();
            var data = repo.GetAll();
            return mapper.Map<List<NewsDTO>>(data);
        }

        public static List<NewsDTO> GetByTitle(string title)
        {
            var repo = new NewsRepo();
            var data = repo.GetByTitle(title);
            return mapper.Map<List<NewsDTO>>(data);
        }

        public static List<NewsDTO> GetByCategory(string category)
        {
            var repo = new NewsRepo();
            var data = repo.GetByCategory(category);
            return mapper.Map<List<NewsDTO>>(data);
        }

        public static List<NewsDTO> GetByDate(DateTime date)
        {
            var repo = new NewsRepo();
            var data = repo.GetByDate(date);
            return mapper.Map<List<NewsDTO>>(data);
        }

        public static List<NewsDTO> GetByDateAndCategory(DateTime date, string category)
        {
            var repo = new NewsRepo();
            var data = repo.GetByDateAndCategory(date, category);
            return mapper.Map<List<NewsDTO>>(data);
        }

        public static List<NewsDTO> GetByDateAndTitle(DateTime date, string title)
        {
            var repo = new NewsRepo();
            var data = repo.GetByDateAndTitle(date, title);
            return mapper.Map<List<NewsDTO>>(data);
        }

        public static bool Create(NewsDTO newsDto)
        {
            var repo = new NewsRepo();
            var news = mapper.Map<News>(newsDto);
            return repo.Create(news);
        }

        public static bool Update(int id, NewsDTO newsDto)
        {
            var repo = new NewsRepo();
            var news = mapper.Map<News>(newsDto);
            return repo.Update(id, news);
        }

        public static bool Delete(int id)
        {
            var repo = new NewsRepo();
            return repo.Delete(id);
        }

    }
}
