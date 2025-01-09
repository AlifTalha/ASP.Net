using DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repos
{
    public class NewsRepo
    {
        private readonly UMSContext db;

        public NewsRepo()
        {
            db = new UMSContext();
        }

     
        public List<News> GetAll()
        {
            return db.News.ToList();
        }

       
        public List<News> GetByTitle(string title)
        {
            return db.News.Where(n => n.Title.Contains(title)).ToList();
        }

       
        public List<News> GetByCategory(string category)
        {
            return db.News.Where(n => n.Category.Contains(category)).ToList();
        }

     
        public List<News> GetByDate(DateTime date)
        {
            return db.News
                     .Where(n => DbFunctions.TruncateTime(n.Date) == date.Date)
                     .ToList(); 
        }

    


        public List<News> GetByDateAndCategory(DateTime date, string category)
        {
            return db.News
                     .Where(n => DbFunctions.TruncateTime(n.Date) == date.Date && n.Category.Contains(category))
                     .ToList();
        }


    



        public List<News> GetByDateAndTitle(DateTime date, string title)
        {
            return db.News
                     .Where(n => DbFunctions.TruncateTime(n.Date) == date.Date && n.Title.Contains(title))
                     .ToList();
        }


        public bool Create(News news)
        {
            try
            {
                db.News.Add(news);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Update(int id, News updatedNews)
        {
            try
            {
                var news = db.News.FirstOrDefault(n => n.Id == id);
                if (news != null)
                {
                    news.Title = updatedNews.Title;
                    news.Category = updatedNews.Category;
                    news.Date = updatedNews.Date;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        public bool Delete(int id)
        {
            try
            {
                var news = db.News.FirstOrDefault(n => n.Id == id);
                if (news != null)
                {
                    db.News.Remove(news);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }



    }
}
