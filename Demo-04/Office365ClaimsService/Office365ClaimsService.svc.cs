using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;


namespace Office365ClaimsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
  
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Office365ClaimsService : IOffice365ClaimsService
    {
        public TokenContainer Authentication(string url, string userName, string password)
        {
          TokenContainer tokens=DAL.getTokens(url, userName, password);
          return tokens;
        }
        public string GetSearchData(string FedAuth, string RtFa, string url, string query)
        {
            return DAL.GetDataFromSP(FedAuth, RtFa, url, QueryType.Search, query);
        }
    }
}
