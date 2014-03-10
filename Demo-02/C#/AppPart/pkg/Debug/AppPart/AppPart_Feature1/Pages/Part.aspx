<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<WebPartPages:AllowFraming ID="AllowFraming" runat="server" />

<html>
<head>
    <title>Who is?</title>

    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>

    <SharePoint:ScriptLink ID="ScriptLink1" Name="init.js" runat="server" OnDemand="false" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink2" Name="sp.init.js" runat="server" OnDemand="false" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink3" Name="sp.runtime.js" runat="server" OnDemand="false" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink4" Name="sp.core.js" runat="server" OnDemand="false" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink5" Name="sp.js" runat="server" OnDemand="false" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink6" Name="sp.userprofiles.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink7" Name="clienttemplates.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink8" Name="clientforms.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink9" Name="clientpeoplepicker.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink10" Name="autofill.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />

    <SharePoint:CssRegistration ID="CssRegistration1" runat="server" Name="default" />
    <SharePoint:CssLink ID="CssLink1" runat="server" />

    <link rel="Stylesheet" type="text/css" href="../Styles/App.css" />

    <script type="text/javascript" src="../Scripts/App.js"></script>
</head>

<body id="partBody">
    <div>
        <h3 id="editmodehdr" style="display: none">Part in edit mode.</h3>
    </div>

    <div id="content">
        <div id="peoplePicker" style="margin-top: 13px; height: 20px; width: 300px;"></div>
        <div id="userInfo" style="margin-top: 20px; width: 330px;">
            <span id="pic" class="ms-floatLeft"></span>
            <div id="basicInfo" style="margin-top: 13px; margin-left: 100px; display: none; width: 230px;">
                <div><span id="name" class="ms-textLarge" style="position: relative;"></span></div>
                <div><span id="title" style="position: relative;"></span></div>
                <div><span id="department" style="position: relative;"></span></div>
                <div><span id="email" style="position: relative;"></span></div>
                <div><span id="phone" style="position: relative;"></span></div>
            </div>
        </div>
        <div id="detailInfo" style="margin-top: 13px; width: 330px; display: none;"></div>
    </div>
</body>
</html>

