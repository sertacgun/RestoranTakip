﻿using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Data.Entity;

namespace AdminLayer.Controllers
{
    public class CompanyCustomerController : Controller
    {
        private RestoranEntities db = new RestoranEntities();

        // GET: CompanyCustomer
        public ActionResult Index()
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var customers = db.Customers.Where(x => x.CompanyId == user.CompanyId);

            return View(customers.ToList());
        }

        public ActionResult Orders(int? id)
        {
            var orders = db.Orders.Where(x => x.CustomersId == id);
            return View(orders.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customers customers)
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                Customers cus = new Customers();
                cus.Name = customers.Name;
                cus.PhoneNumber = customers.PhoneNumber;
                cus.Company = customers.Company;
                cus.CompanyId = user.CompanyId;
                cus.Address = customers.Address;
                cus.AddressDesc = customers.AddressDesc;


                db.Customers.Add(cus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customers);
        }

        public ActionResult Edit(int? id)
        {
            Customers customers = db.Customers.Find(id);
            return View(customers);
        }

        [HttpPost]
        public ActionResult Edit(Customers customers)
        {
            if (ModelState.IsValid)
            {
                Customers cus = db.Customers.Find(customers.Id);
                cus.Name = customers.Name;
                cus.PhoneNumber = customers.PhoneNumber;
                cus.Address = customers.Address;
                cus.AddressDesc = customers.AddressDesc;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customers);
        }

        public ActionResult Delete(int id)
        {
            Customers customers = db.Customers.Find(id);
            var customerOrders = db.Orders.Where(x => x.CustomersId == customers.Id);
            db.Orders.RemoveRange(customerOrders);
            db.SaveChanges();
            db.Customers.Remove(customers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}