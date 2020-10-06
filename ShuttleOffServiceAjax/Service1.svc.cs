using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.ServiceModel.Activation;
using Newtonsoft.Json;

namespace ShuttleOffServiceAjax
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1
    {
        //private static string connString = ConfigurationManager.ConnectionStrings["SODB"]?.ConnectionString;
        private static string connString = ConfigurationManager.ConnectionStrings["Almer_SODB"]?.ConnectionString;


        // VERIFY LOGIN AND GET USER DETAILS  
        [OperationContract]
        public UserDetails UserLogin(UserDetails userLog)
        {
            UserDetails logUser = new UserDetails();
            string resultVal;
            try
            {
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("Main.VerifyLogin", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("EmailAdd", userLog.EmailAdd);
                    cmd.Parameters.AddWithValue("UserPW", userLog.UserPW);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        logUser.UserID = dr.SafeGetInt32(0);
                        logUser.EmailAdd = dr.SafeGetString(1);
                        logUser.UserPW = dr.SafeGetString(2);
                        logUser.FName = dr.SafeGetString(3);
                        logUser.MName = dr.SafeGetString(4);
                        logUser.LName = dr.SafeGetString(5);
                        logUser.Province = dr.SafeGetString(6);
                        logUser.City = dr.SafeGetString(7);
                        logUser.DateCreated = dr.SafeGetDateTime(8);
                    }

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                resultVal = ex.Message;
            }

            return logUser;
        }

        // ADD USER DETAILS
        [OperationContract]
        public string AddUserDetails(UserDetails userInfo)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand("Main.AddUserDetails", connection))
            {
                string message;
                var date = DateTime.Now;
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("EmailAdd", SqlDbType.NVarChar, 50).Value = userInfo.EmailAdd.ToDbParameter();
                command.Parameters.Add("UserPW", SqlDbType.NVarChar, 15).Value = userInfo.UserPW.ToDbParameter();
                command.Parameters.Add("FName", SqlDbType.NVarChar, 50).Value = userInfo.FName.ToDbParameter();
                command.Parameters.Add("MName", SqlDbType.NVarChar, 50).Value = userInfo.MName.ToDbParameter();
                command.Parameters.Add("LName", SqlDbType.NVarChar, 50).Value = userInfo.LName.ToDbParameter();
                command.Parameters.Add("Province", SqlDbType.NVarChar, 90).Value = userInfo.Province.ToDbParameter();
                command.Parameters.Add("City", SqlDbType.NVarChar, 50).Value = userInfo.City.ToDbParameter();
                command.Parameters.AddWithValue("Date", SqlDbType.DateTime).Value = date.ToDbParameter();

                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    message = "SUCCESSFUL";
                }
                else
                {
                    message = "ERROR";
                }
                connection.Close();
                return message;
            }
        }


        //UPDATE USER DETAILS
        [OperationContract]
        public string UpdateUserDetails(UserDetails userInfo)
        {
            string message;
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                using (SqlCommand command = new SqlCommand("Main.UpdateUserDetailsByUserID", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("UserId", SqlDbType.Int).Value = userInfo.UserID.ToDbParameter();
                    command.Parameters.Add("FName", SqlDbType.NVarChar, 50).Value = userInfo.FName.ToDbParameter();
                    command.Parameters.Add("MName", SqlDbType.NVarChar, 50).Value = userInfo.MName.ToDbParameter();
                    command.Parameters.Add("LName", SqlDbType.NVarChar, 50).Value = userInfo.LName.ToDbParameter();
                    command.Parameters.Add("Province", SqlDbType.NVarChar, 90).Value = userInfo.Province.ToDbParameter();
                    command.Parameters.Add("City", SqlDbType.NVarChar, 50).Value = userInfo.City.ToDbParameter();

                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        message = "UPDATED";
                    }
                    else
                    {
                        message = "ERROR";
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

        //UPDATE USER PASSWORD
        [OperationContract]
        public string UpdateUserPassword(UserDetails userInfo)
        {
            string message;
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                using (SqlCommand command = new SqlCommand("Main.UpdateUserPasswordByUserID", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("UserId", SqlDbType.Int).Value = userInfo.UserID.ToDbParameter();
                    command.Parameters.Add("UserPW", SqlDbType.NVarChar, 15).Value = userInfo.UserPW.ToDbParameter();

                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        message = "UPDATED";
                    }
                    else
                    {
                        message = "ERROR";
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }





        // Add more operations here and mark them with [OperationContract]
    }
}
