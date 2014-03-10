<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.core.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.init.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.userprofiles.js"></script>

    <script type="text/javascript" src="../Scripts/App.js"></script>

    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Who is? App
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:ScriptLink ID="ScriptLink5" Name="clienttemplates.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink6" Name="clientforms.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink7" Name="clientpeoplepicker.js" runat="server" LoadAfterUI="true" Localizable="false" />
    <SharePoint:ScriptLink ID="ScriptLink8" Name="autofill.js" runat="server" LoadAfterUI="true" Localizable="false" />

    <div><h2>Welcome to the Who is? App for SharePoint</h2></div>
    <div><h4>This app provides an app part that you can use to get information on a given user</h4></div>
    <div><h4>Just add the Who Is? app part in your SharePoint host web</h4></div>
    <div></br><h3>Or, try the Who Is? functionality here by entering a name:</h3></div>
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
        <div id="detailInfo" style="margin-top: 13px; width: 330px;"></div>
    </div>
</asp:Content>
