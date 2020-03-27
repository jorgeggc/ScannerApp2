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

            //Select Stored procedure to select the information 
            //needed to test if an ID is valid or not
            SqlCommand select_id_info = new SqlCommand("Select_Information", con);
            select_id_info.CommandType = CommandType.StoredProcedure;
            //ID entered to test
            select_id_info.Parameters.AddWithValue("@IDCardNumber", iDCardNumber);

            //The next four parameters passed are outputs that 
            //will store/return the information we to test 
            SqlParameter outID = new SqlParameter("@ReturnIDValue", SqlDbType.Int) { Direction = ParameterDirection.Output };
            select_id_info.Parameters.Add(outID);
            SqlParameter outAccess = new SqlParameter("@ReturnIDAccess", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            select_id_info.Parameters.Add(outAccess);
            SqlParameter outExpiration = new SqlParameter("@ReturnIDExpiration", SqlDbType.DateTime) { Direction = ParameterDirection.Output };
            select_id_info.Parameters.Add(outExpiration);
            SqlParameter outTermination = new SqlParameter("@ReturnIDTermination", SqlDbType.DateTime) { Direction = ParameterDirection.Output };
            select_id_info.Parameters.Add(outTermination);

            con.Open();
            select_id_info.ExecuteNonQuery();
            
            //These four variables will read the values from from the Select_Information stored procedure 
            int ID;
            string access;
            DateTime Expiration;
            DateTime Termination;

            ID = (int)outID.Value;
            access = outAccess.Value.ToString();
            Expiration = (DateTime)outExpiration.Value;
            Termination = (DateTime)outTermination.Value;
            

            //Insert Stored Procedure 
            SqlCommand Insert_sql = new SqlCommand("Insert_Accesslog", con);
            Insert_sql.CommandType = CommandType.StoredProcedure;
            //Parameters passed to the stored procedure to insert
            Insert_sql.Parameters.AddWithValue("@AccessLocationID", 1);
            Insert_sql.Parameters.AddWithValue("@StationID", "CSC");
            Insert_sql.Parameters.AddWithValue("@IDCardNumber", iDCardNumber);
            Insert_sql.Parameters.AddWithValue("@DeclineReason", Pass(ID, access, Expiration, Termination));
            Insert_sql.Parameters.AddWithValue("@OperatorLogin", 1);

            return SqlDataAccess.SaveData(Insert_sql, data);
        }
            
        //Method that tests all the conditions that need to pass in order for an ID to be considered valid
        public static string Pass(int num, string access, DateTime exp, DateTime term)
        {
            //Variable currentDate that holds the current date and time
            DateTime currentDate = DateTime.Now;

            //This variable compares the currentDate to the expiration date of the ID
            int CheckExpirationDate = DateTime.Compare(exp, currentDate);

            //This variable compares the currentDate to the termination date of the ID
            int CheckTerminationDate = DateTime.Compare(term, currentDate);

            //This if statement checks the returned value for @ReturnIDValue
            //If the @ReturnIDValue is equal to 1, it means that the ID exists.
            //It will continue testing the rest of the conditions
            if (num == 1)
            {

                //The second if statement will test if an employee has access to the building. 
                //If they do, @ReturnIDAccess will return true and will continue 
                if (access == "True")
                {

                    //This is the third if statement and will check the expiration date
                    //If the ID is not expired yet then it will continue
                    if(CheckExpirationDate > 0)
                    {
                        //The fourth if statement will check if the employee is terminated
                        //If they are it will return "Terminated"
                        if (CheckTerminationDate < 0)
                        {
                            return "Terminated";
                        }

                        //if they pass all conditions it will return "Passed All"
                        else
                        {
                            return "Passed All";
                        }
                    }

                    //If ID is expired it will return "ID Expired"
                    else{

                        return "ID Expired";
                        
                    }
                }

                //If the ID does not have access, then it will return "Access Denied"
                else{
                    return "Access Denied";
                }
            }

            //This is the first if statement and it will return "ID DNE" 
            //if the ID does not exists in the database
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
