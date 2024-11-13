using LabTask.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabTask.DTOs;

namespace LabTask.Controllers
{
    public class ProductController : Controller
    {
        CustomerDBEntities db = new CustomerDBEntities();

        public static Product Convert(ProductDTO p)
        {
            return new Product
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                Category = p.Category
            };
        }

        public static ProductDTO Convert(Product p)
        {
            return new ProductDTO
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                Category = p.Category
            };
        }

        public static List<ProductDTO> Convert(List<Product> data)
        {
            var list = new List<ProductDTO>();
            foreach (var p in data)
            {
                list.Add(Convert(p));
            }
            return list;
        }

        
        public ActionResult List()
        {
            var data = db.Products.ToList();
            return View(Convert(data));
        }


        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(new List<string> { "Electronics", "Clothing", "Sports", "Tech" });
            return View(new ProductDTO());
        }

        [HttpPost]
        public ActionResult Create(ProductDTO p)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(new List<string> { "Electronics", "Clothing", "Sports", "Tech" });
                return View(p);
            }

            db.Products.Add(Convert(p));
            db.SaveChanges();
            return RedirectToAction("List");
        }


        public ActionResult Details(int Id)
        {
            var exobj = db.Products.Find(Id);
            if (exobj == null)
            {
                return HttpNotFound();
            }
            return View(Convert(exobj));
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var exobj = db.Products.Find(Id);
            if (exobj == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = new SelectList(new List<string> { "Sports", "Tech", "Clothing" }, exobj.Category);

            return View(Convert(exobj));
        }

        [HttpPost]
        public ActionResult Edit(ProductDTO p)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(new List<string> { "Sports", "Tech", "Clothing" }, p.Category);
                return View(p);
            }

            var exobj = db.Products.Find(p.ProductId);
            if (exobj == null)
            {
                return HttpNotFound();
            }

            exobj.Name = p.Name;
            exobj.Description = p.Description;
            exobj.Price = p.Price;
            exobj.StockQuantity = p.StockQuantity;
            exobj.Category = p.Category;

            db.SaveChanges();
            return RedirectToAction("List");
        }


        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var exobj = db.Products.Find(Id);
            if (exobj == null)
            {
                return HttpNotFound();
            }
            return View(Convert(exobj));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int Id)
        {
            var exobj = db.Products.Find(Id);
            if (exobj == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(exobj);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
