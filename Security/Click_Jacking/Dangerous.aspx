<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dangerous.aspx.cs" Inherits="Click_Jacking_Dangerous" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
    <div id="content">
      <asp:HyperLink runat="server" ID="lnk" ImageUrl="~/Content/Images/admiral-akbar-its-a-trap.jpg" NavigateUrl="https://www.owasp.org/index.php/Clickjacking" Target="_blank"/>
    </div>
    </form>
</body>
</html>
