using FinalDemo.DTOs;
using FinalDemo.EF;
using System.Linq;
using System.Web.Mvc;

namespace FinalDemo.Controllers
{
    public class AuthController : Controller
    {
        FinalDBEntities db = new FinalDBEntities();

        private User MapToUser(RegistrationDTO regObj)
        {
            return new User
            {
                UserId = regObj.UserId,
                Name = regObj.Name,
                Email = regObj.Email,
                Password = regObj.Password,
                Role = regObj.Role
            };
        }

        private RegistrationDTO MapToRegistrationDTO(User user)
        {
            return new RegistrationDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View(new RegistrationDTO());
        }

   

        [HttpPost]
        public ActionResult Registration(RegistrationDTO regObj)
        {
            if (ModelState.IsValid)
            {
                var reg = new User
                {
                    Name = regObj.Name,
                    Email = regObj.Email,
                    Password = regObj.Password,
                    Role = regObj.Role 
                };

                if (reg.Password == regObj.ConfirmPassword)
                {
                    db.Users.Add(reg);
                    db.SaveChanges();

                    return RedirectToAction("Login", "Auth");
                }

                TempData["Msg"] = "Password Mismatched";
            }

            return View(regObj);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginDTO());
        }


        [HttpPost]
        public ActionResult Login(LoginDTO logger)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Email == logger.Email && u.Password == logger.Password);

                if (user == null)
                {
                    TempData["Msg"] = "Invalid Email or Password";
                    return RedirectToAction("Login", "Auth");
                }

                Session["User"] = user;
                Session["Role"] = user.Role;
                Session["UserName"] = user.Name;

                return RedirectToAction("Index", "Tickets");
            }

            TempData["Msg"] = "Wrong Credentials!";
            return View(logger);
        }


        public ActionResult Logout()
        {
            Session["User"] = null;
            Session["UserName"] = null;
            Session["Role"] = null;

            return RedirectToAction("Login", "Auth");
        }

    }
}
