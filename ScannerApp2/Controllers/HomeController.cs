using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScannerApp2.Models;
using DataLibrary;
using DataLibrary.Models;
using static DataLibrary.BusinessLogic.ScannerProcessor;

namespace ScannerApp2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(Models.ScannerLogModel model)
        {
            ViewBag.Message = "Insert Scanned ID's to DB.";

            if (ModelState.IsValid)
            {
                int recordCreated = CreateScannerLog(model.AccessLocationID,
                    model.StationID,
                    model.AccessDate,
                    model.IDCardNumber,
                    model.DeclineReason);
                return RedirectToAction("IndexData");
            }

            return View();
        }
        public ActionResult ScannerLog()
        {
            ViewBag.Message = "View a list of Scanned Logs.";

            var data = LoadScannerLog();
            List<Models.ScannerLogModel> accessLog = new List<Models.ScannerLogModel>();

            foreach (var row in data)
            {
                accessLog.Add(new Models.ScannerLogModel
                {
                    AccessLogID = row.AccessLogID,
                    AccessLocationID = row.AccessLocationID,
                    StationID = row.StationID,
                    AccessDate = row.AccessDate,
                    IDCardNumber = row.IDCardNumber,
                    DeclineReason = row.DeclineReason
                });
            }

            return View(accessLog);
        }
        
        
        public ActionResult LocationSelection()
        {
            ViewBag.Message = "A selection of your current location.";

            var data = LoadLocation();

            List<Models.LocationModel> location = new List<Models.LocationModel>();

            foreach (var row in data)
            {
                location.Add(new Models.LocationModel
                {
                    LocationDesc = row.LocationDesc
                });
            }

            return View(location);
        }     
        public ActionResult EmployeeList()
        {
            ViewBag.Message = "View a list of Employee's.";

            var data = LoadEmployees();

            List<Models.EmployeeModel> employees = new List<Models.EmployeeModel>();

            foreach (var row in data)
            {
                employees.Add(new Models.EmployeeModel
                {
                    Name = row.Name,
                    CourtAccessRequired = row.CourtAccessRequired,
                    IDCardNumber = row.IDCardNumber
                });
            }
            return View(employees);
        }
    }
}