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

    }

    [DataContract]
    public class UserDetails
    {
        private string emailadd, userpw, fname, mname, lname, addline1, addline2, province, city;
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
        public string AddLine1 { get { return addline1; } set { addline1 = value; } }
        [DataMember]
        public string AddLine2 { get { return addline2; } set { addline2 = value; } }
        [DataMember]
        public string Province { get { return province; } set { province = value; } }
        [DataMember]
        public string City { get { return city; } set { city = value; } }
        [DataMember]
        public DateTime DateCreated { get { return datecreated; } set { datecreated = value; } }

    }

}
