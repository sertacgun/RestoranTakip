using AdminLayer.Models;
using EntityLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLayer.Controllers
{
    public class CustomerController : Controller
    {
        private RestoranEntities db = new RestoranEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IncomingCallModel callModel)
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            Customers cus = new Customers();
            cus.Address = callModel.customerAddress;
            cus.AddressDesc = callModel.customerAddressDesc;
            cus.CompanyId = user.CompanyId;
            cus.Name = callModel.customerName;
            cus.PhoneNumber = callModel.phoneNumber;
            db.Customers.Add(cus);

            Orders o = new Orders();
            o.CustomersId = callModel.customerId ?? default(int);
            o.OrderStatus = "Sipariş Alındı";
            o.OrganizerId = 8; //DÜZENLENECEK
            o.OrderDetails = callModel.customerOrder;
            o.CourierId = callModel.courier;
            o.OrderDate = DateTime.Now.AddHours(0);

            db.Orders.Add(o);

            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Edit(IncomingCallModel callModel)
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var oldCus = db.Customers.Find(callModel.customerId);

            oldCus.Address = callModel.customerAddress;
            oldCus.AddressDesc = callModel.customerAddressDesc;

            Orders o = new Orders();
            o.CustomersId = callModel.customerId ?? default(int);
            o.OrderStatus = "Sipariş Alındı";
            o.OrganizerId = 8; //DÜZENLENECEK
            o.OrderDetails = callModel.customerOrder;
            o.CourierId = callModel.courier;
            o.OrderDate = DateTime.Now.AddHours(0);
            db.Orders.Add(o);

            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}