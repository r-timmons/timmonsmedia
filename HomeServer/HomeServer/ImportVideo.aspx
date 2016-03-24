<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportVideo.aspx.cs" Inherits="HomeServer.ImportVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Items that already existed: " Visible="False"></asp:Label>
        <br />
        <asp:ListBox ID="ListBox1" runat="server" Height="301px" Visible="False" Width="432px"></asp:ListBox>
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
