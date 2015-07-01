<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Authentication_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
  <h2>Log In</h2>
  <br/>
  <div class="row">
    <div class="col-sm-5">
      Email: <asp:TextBox ID="txtEmail" runat="server" placeholder="example@email.com" MaxLength="254" CssClass="form-control"/>
    </div>
  </div>
  <br/>
  <div class="row">
    <div class="col-sm-5">
      Password: <asp:TextBox ID="txtPassword" runat="server" placeholder="Secure Password" MaxLength="100" CssClass="form-control" TextMode="Password"/>
    </div>
  </div>
  <p>
    <asp:Label runat="server" ID="lblError" Text="Login failed; Invalid email or password" Visible="False" CssClass="alert-danger"></asp:Label>
  </p>
  <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" CssClass="btn btn-primary" Text="Log In"/>
</asp:Content>