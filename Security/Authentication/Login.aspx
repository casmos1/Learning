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
    <asp:RegularExpressionValidator 
      ID="regexEmail" 
      runat="server" 
      ErrorMessage="Incorrect Email Address" 
      ControlToValidate="txtEmail" 
      ValidationExpression="^[a-zA-Z0-9+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$" 
      Display="Dynamic" 
      CssClass="alert-danger"/>
  </p>
  
  <p>
    Don't have an account? <asp:HyperLink runat="server" NavigateUrl="NewUser.aspx" Text="Sign Up Now!"/>
  </p>
  <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" CssClass="btn btn-primary" Text="Log In"/>
  <br/>
  <br />
  <asp:Literal runat="server" ID="litGenericError" Text="The credentials you entered are invalid." Visible="False"/>
  <asp:Literal runat="server" ID="litTime" Text="Account is locked" Visible="False"/>
  <asp:Literal runat="server" ID="litAttemptCount" Visible="False"/>
  <asp:Literal runat="server" ID="litReset" Text="Time to reset your password" Visible="False"/>
</asp:Content>