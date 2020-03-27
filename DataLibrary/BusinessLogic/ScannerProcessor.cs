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
            SqlConnection con = new SqlConnection(strConnString);


            con.Open();
            SqlCommand id = new SqlCommand("Select_Information", con);
            id.CommandType = CommandType.StoredProcedure;
            id.Parameters.AddWithValue("@IDCardNumber", iDCardNumber);
            SqlParameter outID = new SqlParameter("@ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.Output };
            id.Parameters.Add(outID);
            SqlParameter outAccess = new SqlParameter("@ReturnAccess", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            id.Parameters.Add(outAccess);
            SqlParameter outExpiration = new SqlParameter("@ReturnExpiration", SqlDbType.DateTime) { Direction = ParameterDirection.Output };
            id.Parameters.Add(outExpiration);
            SqlParameter outTermination = new SqlParameter("@ReturnTermination", SqlDbType.DateTime) { Direction = ParameterDirection.Output };
            id.Parameters.Add(outTermination);
            id.ExecuteNonQuery();
            
            int ID;
            string access;
            DateTime Expiration;
            DateTime Termination;
            Expiration = (DateTime)outExpiration.Value;
            Termination = (DateTime)outTermination.Value;
            ID = (int)outID.Value;
            access = outAccess.Value.ToString();

            SqlCommand sql = new SqlCommand("Insert_Accesslog", con);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@AccessLocationID", 1);
            sql.Parameters.AddWithValue("@StationID", "CSC");
            sql.Parameters.AddWithValue("@IDCardNumber", iDCardNumber);
            sql.Parameters.AddWithValue("@DeclineReason", Pass(ID, access, Expiration, Termination));
            sql.Parameters.AddWithValue("@OperatorLogin", 1);

            return SqlDataAccess.SaveData(sql, data);
        }
            
        public static string Pass(int num, string access, DateTime exp, DateTime term)
        {
            DateTime currentDate = DateTime.Now;
            int CheckExperiationDate = DateTime.Compare(exp, currentDate);
            int CheckTerminationDate = DateTime.Compare(term, currentDate);
            if (num == 1)
            { 
                if(access == "True")
                {
                    if(CheckExperiationDate > 0)
                    {
                        if (CheckTerminationDate < 0)
                        {
                            return "Terminated";
                        }
                        else
                        {
                            return "Passed All";
                        }
                    }
                    else{

                        return "ID Expired";
                        
                    }
                }

                else{
                    return "Access Denied";
                }
            }

            else{
                return "ID DNE";
            }

        
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
