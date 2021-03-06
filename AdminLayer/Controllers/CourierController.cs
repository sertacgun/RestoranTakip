﻿using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace AdminLayer.Controllers
{
    public class CourierController : Controller
    {
        private RestoranEntities db = new RestoranEntities();
        // GET: Courier
        public ActionResult Index()
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            var courier = db.Courier.Where(x => x.CompanyId == user.CompanyId);
            var isim = db.Orders.GroupBy(x => x.Customers.Name).Select(x => new { isim = x.Key , sayi = x.Count()}).OrderByDescending(x => x.sayi).FirstOrDefault().isim;
            return View(courier.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Courier courier)
        {
            var user = db.AspNetUsers.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                Courier cour = new Courier();
                cour.Name = courier.Name;
                cour.PhoneNumber = courier.PhoneNumber;
                cour.CompanyId = user.CompanyId;
 
                db.Courier.Add(cour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courier);
        }

        public ActionResult Edit(int? id)
        {
            Courier courier = db.Courier.Find(id);
            return View(courier);
        }

        [HttpPost]
        public ActionResult Edit(Courier courier)
        {
            if (ModelState.IsValid)
            {
                Courier cour = db.Courier.Find(courier.Id);
                cour.Name = courier.Name;
                cour.PhoneNumber = courier.PhoneNumber;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courier);
        }

        public ActionResult Delete(int id)
        {
            Courier cour = db.Courier.Find(id);
            db.Courier.Remove(cour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            //Courier cour = db.Courier.Find(id);
            var orders = db.Orders.Where(x => x.CourierId == id);
            ViewBag.Custumers = db.Customers.ToList();
            return View(orders);
        }


    }
}