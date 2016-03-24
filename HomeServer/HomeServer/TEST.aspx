<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TEST.aspx.cs" Inherits="HomeServer.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
        <asp:Panel ID="viewPanel" runat="server" Height="214px">
            <asp:MultiView ID="views" runat="server" ActiveViewIndex="0">
                <asp:View ID="seriesView" runat="server">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="title" DataValueField="seriesid" AutoPostBack="True">
                    </asp:RadioButtonList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="server=192.168.0.24;user id=RYANTIMMONS-PC;persistsecurityinfo=True;database=homeserver;password=K4tIpgxrbi1" ProviderName="MySql.Data.MySqlClient" SelectCommand="select * from tvseries"></asp:SqlDataSource>
                </asp:View>
            </asp:MultiView>
        </asp:Panel>
    </form>
</body>
</html>
