using System;
using CasestudyAPI.DAL;
using CasestudyAPI.DAL.DAO;
using CasestudyAPI.DAL.DomainClasses;
using CasestudyAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasestudyAPI.Controllers
{
    [Authorize]
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        AppDbContext _ctx;
        public OrderController(AppDbContext context) // injected here
        {
            _ctx = context;
            //Console.WriteLine("Here");
        }
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        public ActionResult<string> Index(OrderHelper helper)
        {
            string retVal = "";
            try
            {
                // ----    TESTING ADDITION   to be removed  -------

                if (helper.Email == null)
                {
                    helper.Email = "jjlortez.jl@gmail.com";
                }

                // ----    TESTING ADDITION   to be removed  -------
                CustomerDAO uDao = new CustomerDAO(_ctx);
                Customer orderOwner = uDao.GetByEmail(helper.Email);
                OrderDAO tDao = new OrderDAO(_ctx);
                int orderId = tDao.AddOrder(orderOwner.Id, helper.Selections);
                if (orderId > 0)
                {
                    retVal = "Order " + orderId + " saved!";
                }
                else
                {
                    retVal = "Order not saved";
                }
            }
            catch (Exception ex)
            {
                retVal = "Order not saved " + ex.Message;
            }
            return retVal;
        }
    }
}
