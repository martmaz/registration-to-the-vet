using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Models.DB;

namespace WebApplication5.Controllers
{
    public class WaitingVisitsController : Controller
    {

        WaitingVisitsMenager _waitingVisitsMenager;
        public WaitingVisitsController()
        {
            _waitingVisitsMenager = new WaitingVisitsMenager();
        }

        public ActionResult Index()
        {
            var waitingVisits = _waitingVisitsMenager.GetAllWaitingVisits();

            return View(waitingVisits);
        }

        public ActionResult DeleteWaitingVisit(int id)
        {
            _waitingVisitsMenager.RemoveWaitingVisit(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult AddVisit()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddVisit(WaitingVisits waitingVisits)
        {

            if (ModelState.IsValid)
            {
                bool success = _waitingVisitsMenager.AddWaitingVisit(waitingVisits);
                if (!success)
                {
                    return View(waitingVisits);
                }
                return RedirectToAction("Index");
            }

            return View(waitingVisits);
        }
    }
}