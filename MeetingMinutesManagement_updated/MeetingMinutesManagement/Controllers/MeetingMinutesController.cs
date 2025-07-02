using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetingMinutesManagement.Data;
using MeetingMinutesManagement.Models;
using MeetingMinutesManagement.Services;

namespace MeetingMinutesManagement.Controllers
{
    public class MeetingMinutesController : Controller
    {
        private readonly IMeetingMinutesService _meetingMinutesService;

        public MeetingMinutesController(IMeetingMinutesService meetingMinutesService)
        {
            _meetingMinutesService = meetingMinutesService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _meetingMinutesService.GetMeetingMinutesViewModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MeetingMinutesMaster master, int[] productServiceIds, int[] quantities)
        {
            if (ModelState.IsValid)
            {
                var result = await _meetingMinutesService.SaveMeetingMinutesAsync(master, productServiceIds, quantities);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            var viewModel = await _meetingMinutesService.GetMeetingMinutesViewModelAsync();
            viewModel.MeetingMaster = master;

            if (productServiceIds != null && quantities != null)
            {
                viewModel.MeetingDetails = new List<MeetingMinutesDetails>();
                for (int i = 0; i < productServiceIds.Length; i++)
                {
                    var product = await _meetingMinutesService.GetProductServiceByIdAsync(productServiceIds[i]);
                    viewModel.MeetingDetails.Add(new MeetingMinutesDetails
                    {
                        ProductServiceId = productServiceIds[i],
                        Quantity = quantities[i],
                        ProductService = product
                    });
                }
            }

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetCustomers(string customerType)
        {
            var customers = await _meetingMinutesService.GetCustomersByTypeAsync(customerType);
            return Json(customers);
        }

        [HttpGet]
        public async Task<JsonResult> GetProductServices()
        {
            var products = await _meetingMinutesService.GetProductServicesAsync();
            return Json(products);
        }
    }
}