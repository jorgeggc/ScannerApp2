using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScannerApp2.Models
{
    public class EmployeeModel
    {
        public int IdentifictionCardID { get; set; }
        public string Name { get; set; }
        public string OrgStructure { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public int WorkerTypeID { get; set; }
        public string Company { get; set; }
        public int CourtAccessRequired { get; set; }
        public int IDCardNumber { get; set; }
    }
}