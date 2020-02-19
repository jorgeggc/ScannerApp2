using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScannerApp2.Models
{
    public class EmployeeModel
    {
       // public int IdentifictionCardID { get; set; }
        public string Name { get; set; }
        public int CourtAccessRequired { get; set; }
        public int IDCardNumber { get; set; }
    }
}