using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace ShuttleOffService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string UserLogin(UserDetails userLog);
        
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string AddUserDetails(UserDetails userInfo);

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string UpdateUserDetails(UserDetails userInfo);

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string UpdateUserPassword(UserDetails userInfo);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddCourtDetails")]
        string AddCourtDetails(CourtDetails courtInfo, string userID);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/AddCourtDetails")]
        void AddCourtDetailsOptions();

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/GetCourtID")]
        string GetCourtID(CourtDetails courtInfo, string userID);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/GetCourtID")]
        void GetCourtIDOptions();

        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddCourtSchedUponCourtReg")]
        string AddCourtSchedUponCourtReg(int CourtID, string StartTime, string EndTime, string SchedDate);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/AddCourtSchedUponCourtReg")]
        void AddCourtSchedUponCourtRegOptions();

        //============================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/DisplayCourtOwner")]
        string DisplayCourtOwner(string userID);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/DisplayCourtOwner")]
        void DisplayCourtOwnerOptions();

        //======================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/DisplaySchedDetails")]
        string DisplaySchedDetails(string court_id);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/DisplaySchedDetails")]
        void DisplaySchedDetailsOptions();
        //==============================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/UpdateCourtDetailsInOwners")]
        string UpdateCourtDetailsInOwners(CourtDetails court_infor);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/UpdateCourtDetailsInOwners")]
        void UpdateCourtDetailsInOwnersOptions();
        //============================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/UpdateScheduleDetailsByOwner")]
        string UpdateScheduleDetailsByOwner(CourtDetails court_infor);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/UpdateScheduleDetailsByOwner")]
        void UpdateScheduleDetailsByOwnerOptions();
        //=========================================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteScheduleByOwner")]
        string DeleteScheduleByOwner(string sched_id);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/DeleteScheduleByOwner")]
        void DeleteScheduleByOwnerOptions();
        //==================================================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteCourtByOwner")]
        string DeleteCourtByOwner(string court_id);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/DeleteCourtByOwner")]
        void DeleteCourtByOwnerOptions();
        //==================================================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/DisplayReservation")]
        string DisplayReservation(string user_id);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/DisplayReservation")]
        void DisplayReservationOptions();
        //==================================================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/DisplayPastReservation")]
        string DisplayPastReservation(string user_id);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/DisplayPastReservation")]
        void DisplayPastReservationOptions();
        //===================================================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddFeedbackDetails")]
        string AddFeedbackDetails(string user_id, string court_id, string reserve_id, string sched_id, string ratings, string comments);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/AddFeedbackDetails")]
        void AddFeedbackDetailsOptions();
        //================================================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteReserveDetails")]
        string DeleteReserveDetails(string reserve_id);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/DeleteReserveDetails")]
        void DeleteReserveDetailsOptions();
        //===================================================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/UpdateScheduleDates")]
        string UpdateScheduleDates();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/UpdateScheduleDates")]
        void UpdateScheduleDatesOptions();
        //===================================================================================================================
        [OperationContract]
        [WebInvoke(Method = "*", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteAccountAndConnection")]
        string DeleteAccountAndConnection(string user_id);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/DeleteAccountAndConnection")]
        void DeleteAccountAndConnectionOptions();
    }

    [DataContract]
    public class UserDetails
    {
        private string emailadd, userpw, fname, mname, lname, province, city;
        private int userid;
        private DateTime datecreated;

        [DataMember]
        public int UserID { get { return userid; } set { userid = value; } }
        [DataMember]
        public string EmailAdd { get { return emailadd; } set { emailadd = value; } }
        [DataMember]
        public string UserPW { get { return userpw; } set { userpw = value; } }
        [DataMember]
        public string FName { get { return fname; } set { fname = value; } }
        [DataMember]
        public string MName { get { return mname; } set { mname = value; } }
        [DataMember]
        public string LName { get { return lname; } set { lname = value; } }        
        [DataMember]
        public string Province { get { return province; } set { province = value; } }
        [DataMember]
        public string City { get { return city; } set { city = value; } }
        [DataMember]
        public DateTime DateCreated { get { return datecreated; } set { datecreated = value; } }

    }

    [DataContract]
    public class CourtDetails
    {
        private string cname, cdesc, ccapacity, caddress, cprovince, ccity, cstart, cend, cdateeff, cstatus;
        private int courtid,skid;
        private DateTime dateregistered;

        [DataMember]
        public int CourtID { get { return courtid; } set { courtid = value; } }
        [DataMember]
        public string CName { get { return cname; } set { cname = value; } }
        [DataMember]
        public string CDesc { get { return cdesc; } set { cdesc = value; } }
        [DataMember]
        public string CCapacity { get { return ccapacity; } set { ccapacity = value; } }
        [DataMember]
        public string CAddress { get { return caddress; } set { caddress = value; } }
        [DataMember]
        public string CProvince { get { return cprovince; } set { cprovince = value; } }
        [DataMember]
        public string CCity { get { return ccity; } set { ccity = value; } }
        [DataMember]
        public DateTime DateRegistered { get { return dateregistered; } set { dateregistered = value; } }

        [DataMember]
        public string CStartTime { get { return cstart; } set { cstart = value; } }
        [DataMember]
        public int SchedID { get { return skid; } set { skid = value; } }
        [DataMember]
        public string CEndTime { get { return cend; } set { cend = value; } }
        [DataMember]
        public string CDateEff { get { return cdateeff; } set { cdateeff = value; } }
        [DataMember]
        public string CStatus { get { return cstatus; } set { cstatus = value; } }
    }


    [DataContract]
    public class ReservationDetails
    {
        private string cname, cprovince, ccity, cstartime, cendtime,cdateeff, caddress, cstatus;
        private int rcourtid, rschedid, rreservedid, ruserid;


        [DataMember]
        public int schedId { get { return rcourtid; } set { rcourtid = value; } }
        [DataMember]
        public int userId { get { return rschedid; } set { rschedid = value; }}
        [DataMember]
        public int courtId { get { return rreservedid; } set { rreservedid = value; }}
        [DataMember]
        public int reserveId { get { return ruserid; } set { ruserid = value; } }

        [DataMember]
        public string Name { get { return cname; } set { cname = value; } }
        [DataMember]
        public string Province { get { return cprovince; } set { cprovince = value; }}
        [DataMember]
        public string City { get { return ccity; } set { ccity = value; } }
        [DataMember]
        public string StartTime { get { return cstartime; } set { cstartime = value; } }
        [DataMember]
        public string EndTime { get { return cendtime; } set { cendtime = value; } }
        [DataMember]
        public string DateEffective { get { return cdateeff; } set { cdateeff = value; } }
        [DataMember]
        public string Address { get { return caddress; } set { caddress = value; } }
        [DataMember]
        public string Status { get { return cstatus; } set { cstatus = value; } }



    }


}
