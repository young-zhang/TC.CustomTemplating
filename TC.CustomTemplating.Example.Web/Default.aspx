<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TC.CustomTemplating.Example.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CustomTemplating Example Web</title>
</head>
<body>
    <form id="_form" runat="server">
    <div>
        <h1>Argument</h1>
        <div>
            <asp:Label runat="server" Width="200">Name</asp:Label>
            <asp:TextBox ID="_argumentName" runat="server">Name</asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" Width="200">Value</asp:Label>
            <asp:TextBox ID="_argumentValue" runat="server">Homer Simpson</asp:TextBox>
        </div>
    </div>
    <div>
       <h1>Template</h1>
       <asp:TextBox ID="_template" runat="server" Height="200px" Width="600px" TextMode="MultiLine" /><br/>
       <asp:Button ID="_transform" runat="server" Text="Transform" onclick="_transform_Click" />
    </div>
    <div>
       <h1>Result</h1>
       <asp:TextBox ID="_result" runat="server" Height="200px" Width="600px" ReadOnly="true" TextMode="MultiLine" />
    </div>
 
    </form>
</body>
</html>
