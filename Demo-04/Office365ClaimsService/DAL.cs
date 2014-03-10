using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Office365ClaimsConnector;

namespace Office365ClaimsService
{
    public static class DAL
    {
     

        public static TokenContainer getTokens(string url, string userName, string password)
        {
          TokenContainer tc = new TokenContainer();
          MsOnlineClaimsHelper claimsHelper = new MsOnlineClaimsHelper(url, userName,password);
          CookieContainer cc=claimsHelper.GetCookieContainer();
          tc.FedAuth = cc.GetCookies(new Uri(url))["FedAuth"].Value;
          tc.RtFa = cc.GetCookies(new Uri(url))["rtFa"].Value;
          return tc;
        }


        public static string GetDataFromSP(string FedAuth,string RtFa, string url, QueryType type, string query)
        {
            string responseJson = string.Empty;
            var uri = QueryStringHelper.BuildQuery(QueryType.Search, url, query);
            CookieContainer cc = GetCookieContainer(FedAuth, RtFa, new Uri(url));
            Uri queryUri = new Uri(uri.AbsoluteUri);
            var request = HttpWebRequest.CreateHttp(uri);
            request.Method = "GET";
            var accept = "application/json;odata=verbose";
            if (accept != null)
                request.Accept = accept;
            request.CookieContainer=cc;
            var response = request.GetResponse();
            Stream res = response.GetResponseStream();
            using (var reader = new StreamReader(res))
            {
                responseJson = reader.ReadToEnd();
            }
            return responseJson;
        }

        private static CookieContainer GetCookieContainer(string FedAuth, string rtFa, Uri uri)
        {
            CookieContainer _cachedCookieContainer = null;
            DateTime _expires = DateTime.MinValue;
              CookieContainer cc = new CookieContainer();
            if (_cachedCookieContainer == null || DateTime.Now > _expires)
            {
                  

                    // Set the FedAuth cookie
                    Cookie samlAuth = new Cookie("FedAuth", FedAuth)
                    {
                        Expires = _expires,
                        Path = "/",
                        Secure = uri.Scheme == "https",
                        HttpOnly = true,
                        Domain = uri.Host
                    };
                    cc.Add(samlAuth);
                        // Set the rtFA (sign-out) cookie, added march 2011
                        Cookie rtFaCookie = new Cookie("rtFA", rtFa)
                        {
                            Expires = _expires,
                            Path = "/",
                            Secure = uri.Scheme == "https",
                            HttpOnly = true,
                            Domain = uri.Host
                        };
                        cc.Add(rtFaCookie);
                    }
                    _cachedCookieContainer = cc;
                    return cc;
                }
 

    }
}