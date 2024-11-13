using LabTask.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabTask.DTOs;


namespace LabTask.Controllers
{
    public class CustomerController : Controller
    {
        CustomerDBEntities db = new CustomerDBEntities();

        public static Customer Convert(CustomerDTO c)
        {
            return new Customer
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = int.Parse(c.Phone),
                Address = c.Address,
                DateJoined = c.DateJoined
            };
        }

        public static CustomerDTO Convert(Customer c)
        {
            return new CustomerDTO
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone.ToString(), 
                Address = c.Address,
                DateJoined = c.DateJoined
            };
        }


        public static List<CustomerDTO> Convert(List<Customer> data)
        {
            var list = new List<CustomerDTO>();
            foreach (var c in data)
            {
                list.Add(Convert(c));
            }
            return list;
        }

     
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            var data = db.Customers.ToList();
            return View(Convert(data));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CustomerDTO());

        }
        [HttpPost]
        public ActionResult Create(CustomerDTO c)
        {

            if (ModelState.IsValid)
            {
                db.Customers.Add(Convert(c));
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }

        public ActionResult Details(int Id)
        {
            var exobj = db.Customers.Find(Id);

            if (exobj == null)
            {
                return HttpNotFound();
            }

            return View(Convert(exobj)); 
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var exobj = db.Customers.Find(Id);

            if (exobj == null)
            {
                return HttpNotFound();
            }

            return View(Convert(exobj)); 
        }


        [HttpPost]
        public ActionResult Edit(CustomerDTO c)
        {
            if (!ModelState.IsValid)
            {
                return View(c);
            }
            var exobj = db.Customers.Find(c.CustomerId);

            if (exobj == null)
            {
                return HttpNotFound();
            }

            exobj.FirstName = c.FirstName;
            exobj.LastName = c.LastName;
            exobj.Email = c.Email;

            if (int.TryParse(c.Phone, out int phone))
            {
                exobj.Phone = phone;
            }
            else
            {
                ModelState.AddModelError("Phone", "Invalid phone number format.");
                return View(c); 
            }

            exobj.Address = c.Address;
            exobj.DateJoined = c.DateJoined;
            db.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var exobj = db.Customers.Find(Id);

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
            var exobj = db.Customers.Find(Id);
            if (exobj == null)
            {
                return HttpNotFound();
            }
            db.Customers.Remove(exobj);
            db.SaveChanges();

            return RedirectToAction("List");
        }




    }
}