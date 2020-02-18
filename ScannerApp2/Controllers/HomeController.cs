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
        public ActionResult ScannerLog()
        {
            ViewBag.Message = "View a list of Scanned ID's.";

            var data = LoadScannerLog();
            List<DataLibrary.Models.ScannerLogModel> accessLog = new List<DataLibrary.Models.ScannerLogModel>();

            foreach (var row in data)
            {
                accessLog.Add(new DataLibrary.Models.ScannerLogModel
                {
                    AccessLogID = row.AccessLogID,
                    AccessLocationID = row.AccessLocationID,
                    StationID = row.StationID,
                    AccessDate = row.AccessDate,
                    IDCardNumber = row.IDCardNumber,
                    DeclineReason = row.DeclineReason
                });
            }

            return View();
        }
        public ActionResult LocationSelection()
        {
            ViewBag.Message = "A selection of your current location.";

            return View();
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
                    IdentifictionCardID = row.IdentifictionCardID,
                    Name = row.Name,
                    CourtAccessRequired = row.CourtAccessRequired,
                    IDCardNumber = row.IDCardNumber
                });
            }
            return View(employees);
        }
    }
}