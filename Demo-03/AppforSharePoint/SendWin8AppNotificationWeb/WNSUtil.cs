using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace SendWin8AppNotificationWeb
{
    public class WNSUtil
    {
        public static void SendToastNotification(string data, string ChannelUrl)
        {
            string accessToken = GetAccessToken();
            var subscriptionUri = new Uri(ChannelUrl);
            var request = (HttpWebRequest)WebRequest.Create(subscriptionUri);
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.Headers = new WebHeaderCollection();
            request.Headers.Add("X-WNS-Type", "wns/toast");
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            byte[] notificationMessage = Encoding.Default.GetBytes(data);
            request.ContentLength = notificationMessage.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();

            response.ToString();
        }

        public static void SendTileNotification(string data, string ChannelUrl)
        {
            string accessToken = GetAccessToken();
            var subscriptionUri = new Uri(ChannelUrl);
            var request = (HttpWebRequest)WebRequest.Create(subscriptionUri);
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.Headers = new WebHeaderCollection();
            request.Headers.Add("X-WNS-Type", "wns/tile");
            request.Headers.Add("X-WNS-TTL", "600000");
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Headers.Add("X-WNS-Tag", "1");
            byte[] notificationMessage = Encoding.Default.GetBytes(data);
            request.ContentLength = notificationMessage.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();

            response.ToString();
        }

        public static string GetAccessToken()
        {
            string url = "https://login.live.com/accesstoken.srf";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string sid = HttpUtility.UrlEncode(ConfigurationManager.AppSettings["WNS.SID"]);
            string secret = HttpUtility.UrlEncode(ConfigurationManager.AppSettings["WNS.Clientsecret"]);
            string content = "grant_type=client_credentials&client_id={0}&client_secret={1}&scope=notify.windows.com";
            string data = string.Format(content, sid, secret);
            byte[] notificationMessage = Encoding.Default.GetBytes(data);
            request.ContentLength = notificationMessage.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            string result;
            using (Stream responseStream = response.GetResponseStream())
            {
                var streamReader = new StreamReader(responseStream);
                result = streamReader.ReadToEnd();
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            AccessTokenEntity o = js.Deserialize<AccessTokenEntity>(result);
            return o.access_token;
        }
    }

    public class AccessTokenEntity
    {
        public string token_type;
        public string access_token;
    }
}