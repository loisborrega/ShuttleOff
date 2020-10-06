﻿using System;
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

namespace ShuttleOffService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1
    {
        //private static string connString = ConfigurationManager.ConnectionStrings["SODB"]?.ConnectionString;
        private static string connString = ConfigurationManager.ConnectionStrings["Almer_SODB"]?.ConnectionString;

        // VERIFY LOGIN AND GET USER DETAILS        
        public string UserLogin(UserDetails userLog)
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
                        logUser.UserID = int.Parse(dr[0].ToString());
                        logUser.EmailAdd = dr[1].ToString();
                        logUser.UserPW = dr[2].ToString();
                        logUser.FName = dr[3].ToString();
                        logUser.MName = dr[4].ToString();
                        logUser.LName = dr[5].ToString();
                        logUser.Province = dr[6].ToString();
                        logUser.City = dr[7].ToString();
                        logUser.DateCreated = DateTime.Parse(dr[8].ToString());
                    }           
                                     
                    connection.Close();                    
                }

                resultVal = JsonConvert.SerializeObject(logUser);
            }
            catch (Exception ex)
            {
                resultVal = ex.Message;
            }

            return resultVal;
        }
        
        // ADD USER DETAILS
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

    }
    public static class DBUtils   // database fetch usable methods (for nullable rows)
    {
        public static string SafeGetString(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetString(index);
            return string.Empty;
        }

        public static DateTime? SafeGetDateTime(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetDateTime(index);
            return null;
        }

        public static object ToDbParameter<T>(this T? value)
        where T : struct
        {
            object dbValue = value;
            if (dbValue == null)
            {
                dbValue = DBNull.Value;
            }
            return dbValue;
        }
        public static object ToDbParameter(this object value)
        {
            object dbValue = value;
            if (dbValue == null)
            {
                dbValue = DBNull.Value;
            }
            return dbValue;
        }
    }
}
