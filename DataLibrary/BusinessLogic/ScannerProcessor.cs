using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class ScannerProcessor
    {
        public static int CreateScannerLog(int accessLocationID, string stationID,
           DateTime accessDate, int iDCardNumber, string declineReason)
        {
            ScannerLogModel data = new ScannerLogModel
            {

                AccessLocationID = accessLocationID,
                StationID = stationID,
                AccessDate = accessDate,
                IDCardNumber = iDCardNumber,
                DeclineReason = declineReason

            };

            string strConnString = ConfigurationManager.ConnectionStrings["BuildingAccess"].ConnectionString;
            SqlCommand com;

            DateTime d = DateTime.Now;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            com = new SqlCommand("Insert_Access", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@AccessLocationID", 1);
            com.Parameters.AddWithValue("@IdentificationCardID", 1);
            com.Parameters.AddWithValue("@date", 0);
            com.Parameters.AddWithValue("@pass", "fail");
            com.Parameters.AddWithValue("@firstname", "Billy");

            com.ExecuteNonQuery();
            con.Close();
            return SqlDataAccess.SaveData(com, data);
        }

        public static List<ScannerLogModel> LoadScannerLog()
        {
            string sql = @"select AccessLogID, AccessLocationID, StationID, AccessDate, IDCardNumber, 
                            DeclineReason from dbo.AccessLogs;";

            return SqlDataAccess.LoadData<ScannerLogModel>(sql);
        }
        public static List<EmployeeModel> LoadEmployees()
        {
            string sql = @"select IdentificationCardID, Name, CourtAccessRequired, IDCardNumber from dbo.IdentificationCards;";

            return SqlDataAccess.LoadData<EmployeeModel>(sql);
        }
        public static List<LocationModel> LoadLocation()
        {
            string sql = @"select AccessLocationID, LocationDesc from dbo.AccessLocations;";

            return SqlDataAccess.LoadData<LocationModel>(sql);
        }
    }
}
