<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HomeServer.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        #vidPlayer
        {
            z-index: 1;
            position:relative;
            text-align: center;
            border:groove 2px black;
        }
        #serieslists
        {
            width:39%; 
            float:left;
            border:groove 2px black;
            height:90%;
            padding-left:2em;
            padding-top:2em;
            padding-bottom:2em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div id="serieslists">
        <asp:Label ID="lblSeries" runat="server" Text="Series"></asp:Label>&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddl_tv" runat="server" OnSelectedIndexChanged="ddl_tv_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="lblSeason" runat="server" Text="Season" Visible="False"></asp:Label>&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddl_seasons" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_seasons_SelectedIndexChanged" Visible="False"></asp:DropDownList>&nbsp;&nbsp;&nbsp;

        <asp:Button ID="btnScan" runat="server" Text="Scan for videos" OnClick="btnScan_Click" Visible="False" />

        <asp:Label ID="lblScanSuccess" runat="server" Text="Label" Visible="False"></asp:Label>

        <br />
        <br />
        <asp:Label ID="lblEpisode" runat="server" Text="Episode" Visible="False"></asp:Label>&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddl_episodes" runat="server" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddl_episodes_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
        <div id="videoDiv" runat="server" style="float:right;width:60%">
        </div>
    </form>
</body>
</html>
