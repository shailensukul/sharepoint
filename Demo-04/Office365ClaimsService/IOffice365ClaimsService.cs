using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Office365ClaimsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IOffice365ClaimsService
    {

       [OperationContract]
       [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/authentication")]
        TokenContainer Authentication(string url, string userName, string password);

       [OperationContract]
       [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat=WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/search")]
       string GetSearchData(string FedAuth, string RtFa, string url, string query);
 
       
    }
    [DataContract]
    public class TokenContainer
    {
        [DataMember]
        public string  FedAuth { get; set; }

        [DataMember]
        public string RtFa { get; set; }
    }
   
}
