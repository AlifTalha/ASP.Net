using FinalDemo.EF;
using FinalDemo.DTOs;
using System.Linq;
using System.Web.Mvc;

namespace FinalDemo.Controllers
{
    public class TicketsController : Controller
    {
        private FinalDBEntities db = new FinalDBEntities();

        private static TicketDTO ConvertToDTO(Ticket ticket)
        {
            return new TicketDTO
            {
                TicketId = ticket.TicketId,
                Issue = ticket.Issue,
                Status = ticket.Status,
                UserId = ticket.UserId
            };
        }

        private static Ticket ConvertToEntity(TicketDTO dto)
        {
            return new Ticket
            {
                TicketId = dto.TicketId,
                Issue = dto.Issue,
                Status = dto.Status,
                UserId = dto.UserId
            };
        }

        [AdminAccess]
        public ActionResult Index()
        {
            var tickets = db.Tickets.ToList().Select(ConvertToDTO).ToList();
            return View(tickets);
        }

        [AdminAccess]
        public ActionResult Details(int id)
        {
            var ticket = db.Tickets.Find(id);
            if (ticket == null) return HttpNotFound();

            var ticketDto = ConvertToDTO(ticket);
            return View(ticketDto);
        }









        [SupportStaffAccess]
        public ActionResult StaffTickets()
        {
            var user = (User)Session["User"];
            var tickets = db.Tickets
                            .Where(t => t.UserId == user.UserId && t.Status != "Resolved")
                            .ToList()
                            .Select(ConvertToDTO)
                            .ToList();

            return View(tickets);
        }

        [AdminAccess]
        [HttpGet]
        public ActionResult Add()
        {
            return View(new TicketDTO());
        }

        [AdminAccess]
        [HttpPost]
        public ActionResult Add(TicketDTO ticketDto)
        {
            if (ModelState.IsValid)
            {
                var ticket = ConvertToEntity(ticketDto);
                ticket.UserId = ((User)Session["User"]).UserId; 
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketDto);
        }

        [AdminAccess]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ticket = db.Tickets.Find(id);
            if (ticket == null) return HttpNotFound();

            var ticketDto = ConvertToDTO(ticket);
            return View(ticketDto);
        }

        [AdminAccess]
        [HttpPost]
        public ActionResult Edit(TicketDTO ticketDto)
        {
            if (ModelState.IsValid)
            {
                var ticket = db.Tickets.Find(ticketDto.TicketId);
                if (ticket == null) return HttpNotFound();

                ticket.Issue = ticketDto.Issue;
                ticket.Status = ticketDto.Status;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketDto);
        }

        [AdminAccess]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var ticket = db.Tickets.Find(id);
            if (ticket == null) return HttpNotFound();

            var ticketDto = ConvertToDTO(ticket);
            return View(ticketDto);
        }

        [AdminAccess]
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var ticket = db.Tickets.Find(id);
            if (ticket != null)
            {
                db.Tickets.Remove(ticket);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }





      
     
        [SupportStaffAccess]
        [HttpPost]
        public ActionResult ResolveTicket(int id)
        {
            var ticket = db.Tickets.Find(id);
            if (ticket == null) return HttpNotFound();

            ticket.Status = "Resolved";
            db.SaveChanges();
            return RedirectToAction("StaffTickets");
        }



        [HttpPost]
        public ActionResult Search(string search)
        {
            var user = (User)Session["User"];
            var tickets = db.Tickets.AsQueryable();

            if (user.Role == "Admin")
            {
                tickets = tickets.Where(t => t.Issue.Contains(search) || t.Status.Contains(search));
            }
            else if (user.Role == "Support Staff")
            {
                tickets = tickets.Where(t => t.UserId == user.UserId && (t.Issue.Contains(search) || t.Status.Contains(search)));
            }

            var ticketDtos = tickets.ToList().Select(ConvertToDTO).ToList();
            return View("Index", ticketDtos);
        }
    }
}
