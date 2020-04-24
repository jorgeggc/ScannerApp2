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
                return RedirectToAction("Index");
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
        public ActionResult Scanner1()
        {
            ViewBag.Message = "View a list of Scanned Logs.";

            var data = LoadLastThreeAccessLog()[2];
            List<Models.ScannerLogModel> accessLog = new List<Models.ScannerLogModel>();

            accessLog.Add(new Models.ScannerLogModel
            {
                AccessLogID = data.AccessLogID,
                AccessLocationID = data.AccessLocationID,
                StationID = data.StationID,
                AccessDate = data.AccessDate,
                IDCardNumber = data.IDCardNumber,
                DeclineReason = data.DeclineReason
            });
            return View(accessLog);
        }
        public ActionResult Scanner2()
        {
            ViewBag.Message = "View a list of Scanned Logs.";

            var data = LoadLastThreeAccessLog()[1];
            List<Models.ScannerLogModel> accessLog = new List<Models.ScannerLogModel>();

            accessLog.Add(new Models.ScannerLogModel
            {
                AccessLogID = data.AccessLogID,
                AccessLocationID = data.AccessLocationID,
                StationID = data.StationID,
                AccessDate = data.AccessDate,
                IDCardNumber = data.IDCardNumber,
                DeclineReason = data.DeclineReason
            });
            return View(accessLog);
        }
        public ActionResult Scanner3()
        {
            ViewBag.Message = "View a list of Scanned Logs.";

            var data = LoadLastThreeAccessLog()[0];
            List<Models.ScannerLogModel> accessLog = new List<Models.ScannerLogModel>();
            
                accessLog.Add(new Models.ScannerLogModel
                {
                    AccessLogID = data.AccessLogID,
                    AccessLocationID = data.AccessLocationID,
                    StationID = data.StationID,
                    AccessDate = data.AccessDate,
                    IDCardNumber = data.IDCardNumber,
                    DeclineReason = data.DeclineReason
                });
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
        //public enum ScannerLocations { Kane County Court House, Elgin Branch Court, Kane County Judicial Center, Aurora Brance Court, Kane County Branch Court, ITD };
    }
}