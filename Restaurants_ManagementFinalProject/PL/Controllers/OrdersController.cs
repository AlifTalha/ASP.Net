using BLL.DTOs;
using BLL.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IntroTier.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        //[HttpGet]
        //[Route("")]
        //public HttpResponseMessage GetAll()
        //{
        //    var orders = OrderService.GetAll();
        //    return Request.CreateResponse(HttpStatusCode.OK, orders);
        //}

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["user"] == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "You must be logged in to access orders.");
            }

            var orders = OrderService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }



        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetById(int id)
        {
            var order = OrderService.GetById(id);
            if (order == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, order);
        }

        [HttpGet]
        [Route("table/{tableId}")]
        public HttpResponseMessage GetByTable(int tableId)
        {
            var orders = OrderService.GetByTable(tableId);
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        [HttpGet]
        [Route("unpaid")]
        public HttpResponseMessage GetUnpaidOrders()
        {
            var orders = OrderService.GetUnpaidOrders();
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        //[HttpPost]
        //[Route("")]
        //public HttpResponseMessage Create(OrderDTO order)
        //{
        //    if (!ModelState.IsValid)
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

        //    var created = OrderService.Create(order);
        //    if (created)
        //        return Request.CreateResponse(HttpStatusCode.Created);

        //    return Request.CreateResponse(HttpStatusCode.InternalServerError);
        //}
        [HttpPost]
        [Route("")]
        public HttpResponseMessage Create(OrderDTO order)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            var created = OrderService.Create(order);
            if (created)
                return Request.CreateResponse(HttpStatusCode.Created, "Created Successfully");

            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Creation failed");
        }


        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update(OrderDTO order)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            var updated = OrderService.Update(order);
            if (updated)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var deleted = OrderService.Delete(id);
            if (deleted)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpPut]
        [Route("pay/{orderId}")]
        public HttpResponseMessage MarkAsPaid(int orderId)
        {
            var paid = OrderService.MarkAsPaid(orderId);
            if (paid)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("search")]
        public HttpResponseMessage SearchOrders(int? tableId = null, bool? isPaid = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["user"] == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "You must be logged in to search orders.");
            }

            var orders = OrderService.SearchOrders(tableId, isPaid, fromDate, toDate);
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

    }
}