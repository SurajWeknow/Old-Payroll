<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppReportViewer_Deduct_New.aspx.cs" Inherits="Reports_AppReportViewer_New" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" Visible="true"  CssClass="hindi"/>
        </div>
        <div>
              <asp:Label ID="lblMessage" runat="server" Font-Size="Large" ForeColor="#009900"></asp:Label>
    </div>
    </form>
</body>
</html>
