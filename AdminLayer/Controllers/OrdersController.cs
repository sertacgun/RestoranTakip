using EntityLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLayer.Controllers
{
    public class OrdersController : Controller
    {
        private RestoranEntities db = new RestoranEntities();
        // GET: Orders
        public ActionResult Index()
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var orders = db.Orders.Where(x => x.Customers.CompanyId == user.CompanyId).ToList().OrderByDescending(x => x.OrderDate);
            ViewBag.Customers = db.Customers.ToList();
            ViewBag.Courier = db.Courier.ToList();
            return View(orders);
        }

    }
}