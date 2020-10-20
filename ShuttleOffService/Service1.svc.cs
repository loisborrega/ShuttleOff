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

                    SqlCommand command = new SqlCommand("Main.VerifyLogin", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("EmailAdd", userLog.EmailAdd);
                    command.Parameters.AddWithValue("UserPW", userLog.UserPW);

                    SqlDataReader dr = command.ExecuteReader();
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

        // ADD USER DETAILS
        public string AddCourtDetails(CourtDetails courtInfo, string userID)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand("CourtOwner.AddCourtDetails", connection))
            {
                string message;
                var date = DateTime.Now;
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("UserId", SqlDbType.Int).Value = userID.ToDbParameter();
                command.Parameters.Add("Court_Name", SqlDbType.NVarChar, 90).Value = courtInfo.CName.ToDbParameter();
                command.Parameters.Add("Court_Desc", SqlDbType.NVarChar, 250).Value = courtInfo.CDesc.ToDbParameter();
                command.Parameters.Add("Court_Capacity", SqlDbType.TinyInt).Value = courtInfo.CCapacity.ToDbParameter();
                command.Parameters.Add("Court_Address", SqlDbType.NVarChar, 120).Value = courtInfo.CAddress.ToDbParameter();
                command.Parameters.Add("Court_Province", SqlDbType.NVarChar, 90).Value = courtInfo.CProvince.ToDbParameter();
                command.Parameters.Add("Court_City", SqlDbType.NVarChar, 50).Value = courtInfo.CCity.ToDbParameter();
                command.Parameters.AddWithValue("Court_Date", SqlDbType.DateTime).Value = date.ToDbParameter();

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

        public void AddCourtDetailsOptions()
        { }

        // GET COURT ID TO CREATE SCHEDULE UPON COURT REGISTRATION
        public string GetCourtID(CourtDetails courtInfo, string userID)
        {
            CourtDetails GetCourtID = new CourtDetails();
            string resultVal;
            try
            {
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("CourtOwner.GetCourtIdForSchedule", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("UserId", userID);
                    command.Parameters.AddWithValue("Court_Name", courtInfo.CName);
                    command.Parameters.AddWithValue("Court_Desc", courtInfo.CDesc);
                    command.Parameters.AddWithValue("Court_Capacity", courtInfo.CCapacity);
                    command.Parameters.AddWithValue("Court_Address", courtInfo.CAddress);
                    command.Parameters.AddWithValue("Court_Province", courtInfo.CProvince);
                    command.Parameters.AddWithValue("Court_City", courtInfo.CCity);

                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        GetCourtID.CourtID = int.Parse(dr[0].ToString());                      
                    }

                    connection.Close();
                }

                resultVal = JsonConvert.SerializeObject(GetCourtID);
            }
            catch (Exception ex)
            {
                resultVal = ex.Message;
            }

            return resultVal;
        }

        public void GetCourtIDOptions()
        { }

        // ADD COURT SCHEDULE UPON COURT CREATION
        public string AddCourtSchedUponCourtReg(int CourtID, string StartTime, string EndTime, string SchedDate)
        {
            //int startTime = Convert.ToInt32(StartTime);
            //int endTime = Convert.ToInt32(EndTime);
            //DateTime schedDate = Convert.ToDateTime(SchedDate);

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand("CourtOwner.AddCourtSchedules", connection))
            {
                string message;

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("CourtId", SqlDbType.Int).Value = CourtID.ToDbParameter();
                command.Parameters.Add("Starttime", SqlDbType.NVarChar, 15).Value = StartTime.ToDbParameter();
                command.Parameters.Add("Endtime", SqlDbType.NVarChar, 15).Value = EndTime.ToDbParameter();
                command.Parameters.Add("Date", SqlDbType.NVarChar, 15).Value = SchedDate.ToDbParameter();

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

        public void AddCourtSchedUponCourtRegOptions()
        { }

        //==============================================================================================
        public string DisplayCourtOwner(string userID)
        {
            string json;
            List<CourtDetails> log = new List<CourtDetails>();

            int user_id1 = int.Parse(userID);
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

                    p.CourtID = rdr.SafeGetInt32(0);

                    p.CName = rdr.SafeGetString(1);
                    p.CProvince = rdr.SafeGetString(2);
                    p.CCity = rdr.SafeGetString(3);
                    int a = rdr.SafeGetByte(4);
                    p.CCapacity = a.ToString();
                    p.CAddress = rdr.SafeGetString(5);
                    p.CDesc = rdr.SafeGetString(6);
                    log.Add(p);
                }

                con.Close();

            }
            json = JsonConvert.SerializeObject(log);

            return json;
        }

        public void DisplayCourtOwnerOptions()
        { }

        public string DisplaySchedDetails(string court_id)
        {
            string json;
            List<CourtDetails> log = new List<CourtDetails>();

            int user_id1 = int.Parse(court_id);
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[CourtOwner].[GetCourtSchedulesByCourtID]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CourtId", court_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TimeSpan? time = new TimeSpan();
                    DateTime date = new DateTime();
                    CourtDetails p = new CourtDetails();

                    time = rdr.SafeGetTime(1);
                    p.CStartTime = time.ToString();

                    time = rdr.SafeGetTime(2);
                    p.CEndTime = time.ToString();

                    date = rdr.SafeGetDateTime(3);
                    p.CDateEff = date.ToString("d");

                    p.CStatus = rdr.SafeGetString(4);

                    p.SchedID = rdr.SafeGetInt32(5);

                    log.Add(p);
                }
                con.Close();

            }
            json = JsonConvert.SerializeObject(log);

            return json;
        }

        public void DisplaySchedDetailsOptions()
        { }


        public string UpdateCourtDetailsInOwners(CourtDetails court_infor)
        {

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand("[Main].[UpdateCourtDetailsByCourtID]", connection))
            {
                string message;

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("courtid", SqlDbType.Int).Value = court_infor.CourtID.ToDbParameter();
                command.Parameters.Add("name", SqlDbType.NVarChar, 90).Value = court_infor.CName.ToDbParameter();
                int a = int.Parse(court_infor.CCapacity.ToString());
                command.Parameters.Add("cap", SqlDbType.TinyInt).Value = a.ToDbParameter();
                command.Parameters.Add("address", SqlDbType.NVarChar, 120).Value = court_infor.CAddress.ToDbParameter();
                command.Parameters.Add("description", SqlDbType.NVarChar, 250).Value = court_infor.CDesc.ToDbParameter();
                command.Parameters.Add("province", SqlDbType.NVarChar, 90).Value = court_infor.CProvince.ToDbParameter();
                command.Parameters.Add("city", SqlDbType.NVarChar, 90).Value = court_infor.CCity.ToDbParameter();


                int result = command.ExecuteNonQuery();
                message = "Court has been updated!";
                connection.Close();
                return message;
            }
        }

        public void UpdateCourtDetailsInOwnersOptions()
        { }
        //================================================================================================================
        public string UpdateScheduleDetailsByOwner(CourtDetails court_infor)
        {

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand("[CourtOwner].[UpdateScheduleAndAddByCourtID]", connection))
            {
                string message;

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("court", SqlDbType.Int).Value = court_infor.CourtID.ToDbParameter();
                command.Parameters.Add("start", SqlDbType.NVarChar, 15).Value = court_infor.CStartTime.ToDbParameter();
                command.Parameters.Add("end", SqlDbType.NVarChar, 15).Value = court_infor.CEndTime.ToDbParameter();
                command.Parameters.Add("date", SqlDbType.NVarChar, 15).Value = court_infor.CDateEff.ToDbParameter();



                int result = command.ExecuteNonQuery();
                message = "Schedule has been updated!";
                connection.Close();
                return message;
            }
        }

        public void UpdateScheduleDetailsByOwnerOptions()
        { }


        //====================================================================================================
        public string DeleteScheduleByOwner(string sched_id)
        {

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand("[CourtOwner].[DeleteScheduleByScheduleID]", connection))
            {
                string message;
                int a = int.Parse(sched_id);
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("sched", SqlDbType.Int).Value = a.ToDbParameter();


                int result = command.ExecuteNonQuery();
                message = "hello";
                connection.Close();
                return message;
            }
        }

        public void DeleteScheduleByOwnerOptions()
        { }

        //==============================================================================================
        public string DeleteCourtByOwner(string court_id)
        {

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand("[CourtOwner].[DeleteCourtByCourtID]", connection))
            {
                string message;
                int a = int.Parse(court_id);
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("court", SqlDbType.Int).Value = a.ToDbParameter();


                int result = command.ExecuteNonQuery();
                message = "hello";
                connection.Close();
                return message;
            }
        }

        public void DeleteCourtByOwnerOptions()
        { }


        //==================================================================================================
        public string DisplayReservation(string user_id)
        {
            string json;
            List<ReservationDetails> log = new List<ReservationDetails>();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[Main].[GetActiveReserveDetailsByUserID]", con);
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
                    p.courtId = rdr.SafeGetInt32(9);
                    p.schedId = rdr.SafeGetInt32(10);

                    log.Add(p);
                }
                con.Close();

            }
            json = JsonConvert.SerializeObject(log);

            return json;
        }

        public void DisplayReservationOptions()
        { }


        //====================================================================================================
        public string DisplayPastReservation(string user_id)
        {
            string json;
            List<ReservationDetails> log = new List<ReservationDetails>();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[Main].[GetPastReserveDetailsByUserID]", con);
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
                    p.courtId = rdr.SafeGetInt32(9);
                    p.schedId = rdr.SafeGetInt32(10);

                    log.Add(p);
                }
                con.Close();

            }
            json = JsonConvert.SerializeObject(log);

            return json;
        }

        public void DisplayPastReservationOptions()
        { }


        //===================================================================================================
        public string AddFeedbackDetails(string user_id, string court_id, string reserve_id, string sched_id, string ratings, string comments)
        {

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand("[Main].[AddFeedBackDeletePastReservationAndUpdateSchedule]", connection))
            {
                string message;
                int a = int.Parse(user_id);
                int b = int.Parse(court_id);
                int c = int.Parse(reserve_id);
                int d = int.Parse(ratings);
                int e = int.Parse(sched_id);
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("user", SqlDbType.Int).Value = a.ToDbParameter();
                command.Parameters.Add("court", SqlDbType.Int).Value = b.ToDbParameter();
                command.Parameters.Add("ratings", SqlDbType.TinyInt).Value = d.ToDbParameter();
                command.Parameters.Add("comments", SqlDbType.NVarChar, 250).Value = comments.ToDbParameter();

                command.Parameters.Add("date", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("reserv", SqlDbType.Int).Value = c.ToDbParameter();
                command.Parameters.Add("sched", SqlDbType.Int).Value = e.ToDbParameter();


                int result = command.ExecuteNonQuery();
                message = "hello";
                connection.Close();
                return message;
            }
        }

        public void AddFeedbackDetailsOptions()
        { }

        //==========================================================================================================
        public string DeleteReserveDetails(string reserve_id)
        {
            string msg = "";
            int a = int.Parse(reserve_id);
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            SqlCommand command = new SqlCommand("[Main].[DeleteReserveDetailsAndUpdateCourtSchedule]", con);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@reserve", SqlDbType.Int).Value = a.ToDbParameter();

            command.ExecuteNonQuery();

            con.Close();

            return msg = "Reservation Removed!";
        }

        public void DeleteReserveDetailsOptions()
        { }

        //==============================================================================================
        public string UpdateScheduleDates()
        {
            string json = string.Empty;
            string timeToday_s;

            DateTime today = DateTime.Today;

            DateTime t = DateTime.Now;
            timeToday_s = t.ToString("HH:mm:ss");
            TimeSpan timeToday = TimeSpan.Parse(timeToday_s);

            DateTime scheduleDate;

            List<ReservationDetails> log = new List<ReservationDetails>();

            using (SqlConnection con = new SqlConnection(connString))
            {

                SqlCommand cmd = new SqlCommand("[Main].[GetDateInCourtSchedules]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TimeSpan? time = new TimeSpan();
                    DateTime date = new DateTime();
                    ReservationDetails p = new ReservationDetails();

                    p.schedId = rdr.SafeGetInt32(0);

                    date = rdr.SafeGetDateTime(1);
                    p.DateEffective = date.ToString("d");

                    p.reserveId = rdr.SafeGetByte(2);

                    time = rdr.SafeGetTime(3);
                    p.EndTime = time.ToString();
                    log.Add(p);
                }
                con.Close();


                for (int i = 0; i < log.Count; i++)
                {
                    scheduleDate = DateTime.Parse(log[i].DateEffective);
                    TimeSpan end = TimeSpan.Parse(log[i].EndTime);
                    if (today > scheduleDate) // if schedule time is already past to date
                    {

                        if (log[i].reserveId == 2 || log[i].reserveId == 1)
                        {
                            cmd = new SqlCommand("[Main].[SetReservationStatusAndReservationDetails]", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("res", SqlDbType.TinyInt).Value = log[i].reserveId.ToDbParameter();
                            cmd.Parameters.Add("sched", SqlDbType.Int).Value = log[i].schedId.ToDbParameter();

                            cmd.ExecuteNonQuery();
                            con.Close();


                        }

                    }
                    else if(today == scheduleDate) // if date is same check time
                    {
                      

                        int a = TimeSpan.Compare(end, timeToday);

                        if (a < 0) // if schedule endtime is already past to date
                        {

                            if (log[i].reserveId == 2 || log[i].reserveId == 1)
                            {
                                cmd = new SqlCommand("[Main].[SetReservationStatusAndReservationDetails]", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("res", SqlDbType.TinyInt).Value = log[i].reserveId.ToDbParameter();
                                cmd.Parameters.Add("sched", SqlDbType.Int).Value = log[i].schedId.ToDbParameter();

                                cmd.ExecuteNonQuery();
                                con.Close();


                            }
                        }
                    }
                    else // if date is not same not past
                    {
                        json = "nothing";
                    }

                }

            }
            
            return json;

        }

        public void UpdateScheduleDatesOptions()
        { }
        //=================================================================================================
        public string DeleteAccountAndConnection(string user_id)
        {

            using (SqlConnection connection = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand("[Main].[DeleteAccountByUserID]", connection))
            {
                string message;
                int a = int.Parse(user_id);
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("user", SqlDbType.Int).Value = a.ToDbParameter();


                int result = command.ExecuteNonQuery();
                message = "hello";
                connection.Close();
                return message;
            }
        }

        public void DeleteAccountAndConnectionOptions()
        { }


    }
    public static class DBUtils   // database fetch usable methods (for nullable rows)
    {
        public static string SafeGetString(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetString(index);
            return string.Empty;
        }

        public static int SafeGetInt16(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetInt16(index);
            return 0;
        }

        public static int SafeGetByte(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetByte(index);
            return 0;
        }

        public static int SafeGetInt32(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetInt32(index);
            return 0;
        }

        public static TimeSpan? SafeGetTime(this SqlDataReader reader, int index)
        {
            if (!reader.IsDBNull(index))
                return reader.GetTimeSpan(index);
            return null;
        }

        public static DateTime SafeGetDateTime(this SqlDataReader reader, int index)
        {
            DateTime dat = new DateTime();

            if (!reader.IsDBNull(index))
                return reader.GetDateTime(index);
            return dat;
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
