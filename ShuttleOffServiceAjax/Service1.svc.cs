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

        [OperationContract]
        public List<CourtDetails> DisplayNameProvinceCity(string search)
        {
            List<CourtDetails> log = new List<CourtDetails>();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[Main].[GetCourtNameByDetails]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("name", search);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CourtDetails p = new CourtDetails();
                    p.CourtId = rdr.SafeGetInt32(0);
                    p.Name = rdr.SafeGetString(1);
                    p.Province = rdr.SafeGetString(2);
                    p.City = rdr.SafeGetString(3);

                    log.Add(p);
                }
                con.Close();
            }

            return log;
        }

        [OperationContract]
        public List<CourtDetails> DisplayCourtDetails(int court_id)
        {
            List<CourtDetails> log = new List<CourtDetails>();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[CourtOwner].[GetCourtDetailsByCourtID]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("court", court_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TimeSpan? time = new TimeSpan();
                    DateTime date = new DateTime();
                    CourtDetails p = new CourtDetails();

                    p.Owner = rdr.SafeGetString(0);
                    p.Name = rdr.SafeGetString(1);
                    p.Capacity = rdr.SafeGetByte(2);
                    p.Description = rdr.SafeGetString(3);
                    p.Address = rdr.SafeGetString(4);
                    p.Province = rdr.SafeGetString(5);
                    p.City = rdr.SafeGetString(6);

                    time = rdr.SafeGetTime(7);
                    p.StartTime = time.ToString();

                    time = rdr.SafeGetTime(8);
                    p.EndTime = time.ToString();

                    p.Status = rdr.SafeGetString(9);

                    date = rdr.SafeGetDateTime(10);
                    p.DateEffective = date.ToString("d");

                    p.SchedId = rdr.SafeGetInt32(11);

                    log.Add(p);
                }
                con.Close();

            }

            return log;
        }


        [OperationContract]
        public List<FeedbackDetails> DisplayCourtFeedbacks(int court_id)
        {
            List<FeedbackDetails> log = new List<FeedbackDetails>();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[Main].[GetUserFeedbacksByCourtID]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("court", court_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DateTime? date = new DateTime();
                    FeedbackDetails p = new FeedbackDetails();

                    p.Stars = rdr.SafeGetByte(0);
                    p.Name = rdr.SafeGetString(1);
                    p.Comments = rdr.SafeGetString(2);
                    date = rdr.SafeGetDateTime(3);
                    p.Date = date.ToString();


                    log.Add(p);
                }
                con.Close();

            }


            return log;
        }

        [OperationContract]
        public string AddReserveDetails(ReservationDetails res)
        {
            string msg ;

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            SqlCommand command = new SqlCommand("[Main].[AddReservationDetailsAndUpdateCourtSchedule]", con);
            command.CommandType = CommandType.StoredProcedure;
            var date = DateTime.Now;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@sched", SqlDbType.NVarChar, 50).Value = res.schedId.ToDbParameter();
            command.Parameters.Add("@user", SqlDbType.NVarChar, 15).Value = res.userId.ToDbParameter();
            command.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = date;

            command.ExecuteNonQuery();

            con.Close();


            msg = "You have Reserve a Court!";

            return msg ;
        }


        [OperationContract]
        public List<ReservationDetails> DisplayReservation(int user_id)
        {
            List<ReservationDetails> log = new List<ReservationDetails>();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[Main].[GetReserveDetailsByUserID]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("user", user_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TimeSpan? time = new TimeSpan();
                    DateTime date = new DateTime();
                    ReservationDetails p = new ReservationDetails();
                    p.Name = rdr.SafeGetString(0);
                    p.Province = rdr.SafeGetString(1);
                    p.City = rdr.SafeGetString(2);

                    time = rdr.SafeGetTime(3);
                    p.StartTime = time.ToString();

                    time = rdr.SafeGetTime(4);
                    p.EndTime = time.ToString();

                    date = rdr.SafeGetDateTime(5);
                    p.DateEffective = date.ToString("d");

                    p.Address = rdr.SafeGetString(6);
                    p.Status = rdr.SafeGetString(7);
                    p.reserveId = rdr.SafeGetInt32(8);

                    log.Add(p);
                }
                con.Close();

            }
            return log;
        }

        [OperationContract]
        public string DeleteReserveDetails(int reserve_id)
        {
            string msg = "";

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            SqlCommand command = new SqlCommand("[Main].[DeleteReserveDetailsAndUpdateCourtSchedule]", con);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@reserve", SqlDbType.Int).Value = reserve_id;

            command.ExecuteNonQuery();

            con.Close();

            return msg = "Reservation Removed!";
        }

        [OperationContract]
        public string DeleteHistoryDetails(int reserve_id)
        {
            string msg = "";

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            SqlCommand command = new SqlCommand("[Main].[DeleteHistoryDetailsByReservationID]", con);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@reserve", SqlDbType.Int).Value = reserve_id;

            command.ExecuteNonQuery();

            con.Close();

            return msg = "Past Reservation Removed!";
        }


        [OperationContract]
        public List<CourtDetails> DisplayCourtOwnerDetails(string user_id)
        {
            List<CourtDetails> log = new List<CourtDetails>();
            int user_id1 = 1020;
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[CourtOwner].[GetCourtDetailsByUserID]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("user", user_id1);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CourtDetails p = new CourtDetails();

                    p.CourtId = rdr.SafeGetInt32(0);
                    p.Name = rdr.SafeGetString(1);
                    p.Province = rdr.SafeGetString(2);
                    p.City = rdr.SafeGetString(3);
                    p.Capacity = rdr.SafeGetByte(4);
                    p.Address = rdr.SafeGetString(5);
                    p.Description = rdr.SafeGetString(6);

                    log.Add(p);
                }
                con.Close();

            }

            return log;
        }

        [OperationContract]
        public string AddCourtDetails(CourtDetails courtInfo, string userID)
        {
            
                return "hello";
            
        }
        //[OperationContract]

        //[OperationContract]

        //[OperationContract]

        //[OperationContract]

        //[OperationContract]

        //[OperationContract]

        //[OperationContract]

        //[OperationContract]

        //[OperationContract]



        // Add more operations here and mark them with [OperationContract]
    }
}
