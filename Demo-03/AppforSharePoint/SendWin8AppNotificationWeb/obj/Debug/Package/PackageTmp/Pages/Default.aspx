<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SendWin8AppNotificationWeb.Pages.Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>How to Send Toast and Tile notifications to Windows 8 applications from SharePoint via a Windows Azure Web Site</title>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js"></script>
    <script type="text/javascript">
        var hostweburl;

        // Load the SharePoint resources.
        $(document).ready(function () {

            // Get the URI decoded app web URL.
            hostweburl = decodeURIComponent(getQueryStringParameter("SPHostUrl"));

            // The SharePoint js files URL are in the form:
            // host_web_url/_layouts/15/<resource>.js
            var scriptbase = hostweburl + "/_layouts/15/";

            // Load the js file and continue to the success handler.
            $.getScript(scriptbase + "SP.UI.Controls.js", renderChrome);
        });

        // Function to prepare the options and render the control.
        function renderChrome() {

            // The Help, Account, and Contact pages receive the 
            // same query string parameters as the main page.
            var options = {
                "appTitle": "How to Send Toast and Tile notifications to Windows 8 applications from SharePoint via a Windows Azure Web Site"
            };

            var nav = new SP.UI.Controls.Navigation("SharePointChromeControl", options);
            nav.setVisible(true);
        }

        // Function to retrieve a query string value.
        // For production purposes you may want to use
        // a library to handle the query string.
        function getQueryStringParameter(paramToRetrieve) {
            var params = document.URL.split("?").length > 1 ?
                document.URL.split("?")[1].split("&") : [];
            var strParams = "";
            for (var i = 0; i < params.length; i = i + 1) {
                var singleParam = params[i].split("=");
                if (singleParam[0] == paramToRetrieve)
                    return singleParam[1];
            }
        }
    </script>
    <link href="/Styles/Style.css" rel="stylesheet" />
</head>
<body style="overflow: auto">
    <div id="SharePointChromeControl"></div>
    <form id="form1" runat="server">
        <div style="margin-left: 40px;">
            <p>
                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </p>
            <asp:Panel ID="Panel1" runat="server">
                <div class="tileNotification">
                    <h1>Send Tile Notification</h1>
                    <asp:TextBox ID="txt_tileNotificationTemplate" runat="server" TextMode="MultiLine" Height="200px" Width="420px">                       
                    </asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="btnSemdTileNotification" runat="server" Text="Send" OnClick="btnSemdTileNotification_Click" />
                    <br />
                    <p>
                        Notes: For more Tile notification templates,
                    </p>
                    <p>
                        see <a href="http://msdn.microsoft.com/en-us/library/windows/apps/Hh761491.aspx" target="_blank">http://msdn.microsoft.com/en-us/library/windows/apps/Hh761491.aspx</a>
                    </p>
                </div>
                <div class="toastNotification">
                    <h1>Send Toast Notification</h1>
                    <asp:TextBox ID="txt_toastNotificationTemplate" runat="server" TextMode="MultiLine" Height="200px" Width="420px">                       
                    </asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="btnSemdToastNotification" runat="server" Text="Send" OnClick="btnSemdToastNotification_Click" />
                    <br />
                    <p>
                        Notes: For more Toast notification templates,
                    </p>
                    <p>
                        see <a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh761494.aspx" target="_blank">http://msdn.microsoft.com/en-us/library/windows/apps/hh761494.aspx</a>
                    </p>
                </div>
               
            </asp:Panel>
        </div>
    </form>
</body>
</html>
