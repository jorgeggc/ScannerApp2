using System;

namespace DataLibrary.Models
{
    public class ScannerLogModel
    {
        public int AccessLogID { get; set; }
        public int AccessLocationID { get; set; }
        public string StationID { get; set; }
        public DateTime AccessDate { get; set; }
        public int IDCardNumber { get; set; }
        public string DeclineReason { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime Expiration { get; set; }

    }
}
