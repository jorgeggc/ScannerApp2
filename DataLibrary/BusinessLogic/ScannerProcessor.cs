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
            SqlParameter outID = new SqlParameter("@ReturnValue", SqlDbType.Int, 1) { Direction = ParameterDirection.Output };
            id.Parameters.Add(outID);
            SqlParameter parameterReturnAccess = new SqlParameter("@ReturnAccess", SqlDbType.Bit, 10) { Direction = ParameterDirection.Output };
            parameterReturnAccess.Direction = ParameterDirection.ReturnValue;
            id.Parameters.Add(parameterReturnAccess);
            //id.Parameters.Add(outAccess);
            id.ExecuteNonQuery();
            
            int ID;
            string access;
            ID = (int)outID.Value;
            access = id.Parameters["ReturnAccess"].Value.ToString();
            /**id.CommandType = CommandType.StoredProcedure;
            id.Parameters.AddWithValue("@IDCardNumber", iDCardNumber);
            SqlParameter returnParameter = id.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            con.Open();
            id.ExecuteNonQuery();
            int returnID = (int)returnParameter.Value;**/

            SqlCommand sql = new SqlCommand("Insert_Accesslog", con);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@AccessLocationID", 1);
            sql.Parameters.AddWithValue("@StationID", access);
            sql.Parameters.AddWithValue("@IDCardNumber", iDCardNumber);
            sql.Parameters.AddWithValue("@DeclineReason", Pass(ID, access));
            sql.Parameters.AddWithValue("@OperatorLogin", 1);

            return SqlDataAccess.SaveData(sql, data);

            
        }
            
        public static string Pass(int num, string access)
        {
            if (num == 1)
            { 
                if(access == "1")
                {
                    return "good ID";
                }
                else{
                    return "bad ID";
                }
            }else if (num == 0)
            {
                return "ID DNE";
            }
            else
            {
                return "broken";
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
