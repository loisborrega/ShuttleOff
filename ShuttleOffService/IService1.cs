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
        private string cname, cdesc, ccapacity, caddress, cprovince, ccity;
        private int courtid;
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
    }

}
