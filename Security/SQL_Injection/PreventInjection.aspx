<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreventInjection.aspx.cs" Inherits="SQL_Injection_PreventInjection" MasterPageFile="/Site.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
  <h2>SQL Injection</h2>
  <br/>
  What is dangerous?
  <ul>
    <li>' OR 1=1;--</li>
    <li>'; DROP TABLE Users;</li>
    <li>' AND 1=(SELECT COUNT(*) FROM probeForTableName); --';</li>
  </ul>
  <div class="row">
    <div class="col-sm-5">
      <asp:TextBox runat="server" ID="txtUnsafeSSN" CssClass="form-control"/>
    </div>
    <div class="col-sm-5">
      <asp:Button runat="server" ID="btnUnsafe" OnClick="UnsafeSql" Text="Submit Unsafe SSN" CssClass="btn btn-danger"/>
    </div>
  </div>
    <br/>
    <br/>
    When you know what type of value to expect, check for it.
    <br/>

    <div class="row">
      <div class="col-sm-5">
        <asp:TextBox ID="txtSSN" runat="server" placeholder="XXX-XX-XXXX" MaxLength="11" CssClass="form-control"/>
      </div>
      <div class="col-sm-5">
        <asp:button ID="btnSSN" runat="server" Text="Submit SSN" OnClick="SafeSql" CssClass="btn btn-primary"/>
      </div>
    </div>


    <br/>

    <p>
      <asp:RegularExpressionValidator ID="regexpSSN" runat="server" ErrorMessage="Incorrect SSN Number" ControlToValidate="txtSSN" ValidationExpression="^\d{3}-\d{2}-\d{4}$" Display="Dynamic" ValidationGroup="grpSSN" CssClass="alert-danger" />
      <asp:RequiredFieldValidator runat="server" ID="rfvSSN" ControlToValidate="txtSSN" ErrorMessage="SSN Is required" Display="Dynamic" ValidationGroup="grpSSN"  CssClass="alert-danger" />
      <asp:Literal runat="server" ID="litGoodJob" Visible="False" Text="Good Job!  Valid SSN"></asp:Literal>
    </p>
</asp:Content>