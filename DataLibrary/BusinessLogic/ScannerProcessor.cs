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


            //string sql = @"[dbo].[Insert_Accesslog] @AccessLocationID = 2, @StationID = CSC, @IDCardNumber = 99991,	@DeclineReason = Good, @OperatorLogin = 1";

            string strConnString = ConfigurationManager.ConnectionStrings["BuildingAccess"].ConnectionString;
            SqlCommand sql ;

            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            sql = new SqlCommand("Insert_Accesslog", con);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@AccessLocationID", 1);
            sql.Parameters.AddWithValue("@StationID", "CSC");
            sql.Parameters.AddWithValue("@IDCardNumber", 99991);
            sql.Parameters.AddWithValue("@DeclineReason", Pass());
            sql.Parameters.AddWithValue("@OperatorLogin", 1);

            return SqlDataAccess.SaveData(sql, data);
        }
       
        public static string Pass()
        {
            return "good method";
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
