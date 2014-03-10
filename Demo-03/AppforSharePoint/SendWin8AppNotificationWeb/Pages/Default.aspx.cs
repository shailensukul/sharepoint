using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SendWin8AppNotificationWeb.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        string tileNotification = @"<tile>
                          <visual>
                            <binding template=""TileWideText03"">
                              <text id=""1"">{0}</text>
                            </binding>  
                          </visual>
                        </tile>
";
        string toastNotification = @"<toast>
                            <visual>
                                <binding template=""ToastText01"">
                                    <text id=""1"">{0}</text>
                                </binding>  
                            </visual>
                        </toast>
";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["ChannelUrl"]))
            {
                Application["Error"] = null;
                Application["ChannelUrl"] = this.Page.Request.QueryString["ChannelUrl"];
                try
                {
                    Database.Instance.SaveChannelUrl(this.Page.Request.QueryString["ChannelUrl"]);
                }
                catch (Exception ex)
                {
                    Application["Error"] = ex.ToString();
                }
                return;
            }

            if (Application["Error"] != null)
            {
                Label4.Text = Application["Error"] as string;
            }

            if (Application["ChannelUrl"] != null)
            {
                Label1.Text = string.Format("ChannelUrl: {0}", Application["ChannelUrl"]);
            }
            else
            {
                Label1.Text = "The ChannelUrl was not set by the Windows 8 App yet. Please open the Windows 8 App to set the ChannelUrl, and then refresh the page again.";
                Panel1.Visible = false;
            }

            if (Request.QueryString["SPAppWebUrl"] != null)
            {
                Label2.Text = string.Format("<a href='{0}/lists/tasks/allitems.aspx?ChannelUrl={1}' target='_blank'>Task List</a>", 
                    Request.QueryString["SPAppWebUrl"],
                    Application["ChannelUrl"]);
            }
            else
            {
                Label2.Text = "SPAppWebUrl not found";
            }
        }

        protected void btnSemdTileNotification_Click(object sender, EventArgs e)
        {
            WNSUtil.SendTileNotification(string.Format(tileNotification, txt_tileNotificationTemplate.Text), Application["ChannelUrl"].ToString());
        }

        protected void btnSemdToastNotification_Click(object sender, EventArgs e)
        {
            WNSUtil.SendToastNotification(string.Format(toastNotification, txt_toastNotificationTemplate.Text), Application["ChannelUrl"].ToString());
        }
    }
}