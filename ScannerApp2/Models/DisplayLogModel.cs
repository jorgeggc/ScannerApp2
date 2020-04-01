using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScannerApp2.Models
{
    public class DisplayLogModel
    {
        public int IDCardNumber { get; set; }
        public string DeclineReason { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime Expiration { get; set; }
        

    }
}