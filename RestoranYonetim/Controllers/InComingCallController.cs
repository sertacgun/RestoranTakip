using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer;
namespace RestoranYonetim.Controllers
{
    public class InComingCallController : Controller
    {
        private RestoranEntities db = new RestoranEntities();
        // GET: InComingCall
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCall(int companyId, string phoneNumber) {
            if(phoneNumber.Trim() == "" || phoneNumber == null || phoneNumber.Length < 9) return View(new { result = "OK" });
            db.IncomingCallLog.Add(new IncomingCallLog
            {
                CompanyId = companyId,
                PhoneNumber = phoneNumber,
                CreateDate = DateTime.Now,
                IsReaded = false
            });
            db.SaveChanges();
            return View(new { result = "OK"});
        }
    }
}