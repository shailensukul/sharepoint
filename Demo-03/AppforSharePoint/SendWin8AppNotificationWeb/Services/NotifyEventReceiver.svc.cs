using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;
using System.Web;
using System.ServiceModel.Activation;
using System.Configuration;

namespace SendWin8AppNotificationWeb.Services
{
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class NotifyEventReceiver : IRemoteEventService
    {
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            SPRemoteEventResult result = new SPRemoteEventResult();

            //using (ClientContext clientContext = TokenHelper.CreateRemoteEventReceiverClientContext(properties))
            //{
            //    if (clientContext != null)
            //    {
            //        clientContext.Load(clientContext.Web);
            //        clientContext.ExecuteQuery();
            //    }
            //}

            try
            {
                WNSUtil.SendToastNotification(string.Format(@"
<toast launch=""{2}/Lists/Tasks/DispForm.aspx?ID={1}"">
                            <visual>
                                <binding template=""ToastText01"">
                                    <text id=""1"">{0}</text>
                                </binding>  
                            </visual>
                        </toast>
", properties.ItemEventProperties.AfterProperties["Title"],
 properties.ItemEventProperties.ListItemId,
 properties.ItemEventProperties.WebUrl),
                      Database.Instance.GetChannelUrl(ConfigurationManager.AppSettings["TenantId"]));
            }
            catch { }
            return result;
        }

        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            //https://sokool-00db3f37d16e41.sharepoint.com/SendWin8AppNotification/Lists/Tasks/DispForm.aspx?ID=1
            //https://sokool-00db3f37d16e41.sharepoint.com/SendWin8AppNotification
            try
            {
                WNSUtil.SendToastNotification(string.Format (@"
<toast launch=""{2}/Lists/Tasks/DispForm.aspx?ID={1}"">
                            <visual>
                                <binding template=""ToastText01"">
                                    <text id=""1"">{0}</text>
                                </binding>  
                            </visual>
                        </toast>
", properties.ItemEventProperties.AfterProperties["Title"], 
 properties.ItemEventProperties.ListItemId,
 properties.ItemEventProperties.WebUrl),
                      Database.Instance.GetChannelUrl(ConfigurationManager.AppSettings["TenantId"]));
            }
            catch { }
        }
    }
}
