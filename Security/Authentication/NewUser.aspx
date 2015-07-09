<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NewUser.aspx.cs" Inherits="Authentication_NewUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
 <script src="/Scripts/Format.js?<%=DateTime.Now.ToFileTimeUtc() %>"></script>
  <h2>Sign Up</h2>
  <br />
  <div class="row">
    <div class="col-sm-5">
      Email:
      <asp:TextBox ID="txtEmail" runat="server" placeholder="example@email.com" MaxLength="254" CssClass="form-control" />
    </div>
    <div class="col-sm-7" style="float: right; position: absolute; padding-left: 600px; width: 950px; z-index: -1">
      Password must...
      <ul>
        <li>Contain at least 1 upper case letter</li>
        <li>Contain at least 1 lower case letter</li>
        <li>Contain at least 1 number</li>
        <li>Have a minimum length of 8</li>
      </ul>
    </div>
  </div>
  <br />
  <div class="row">
    <div class="col-sm-5">
      SSN#:
      <asp:TextBox ID="txtSSN" runat="server" placeholder="999-99-9999" MaxLength="11" CssClass="form-control" onkeyup="jsFormatSSN(this)" />
    </div>
  </div>
  <br />
  <div class="row">
    <div class="col-sm-5">
      Password:
      <asp:TextBox ID="txtPassword" runat="server" placeholder="Secure Password" MaxLength="100" CssClass="form-control" TextMode="Password" />
      <ajaxToolkit:PasswordStrength
        TargetControlID="txtPassword"
        DisplayPosition="RightSide"
        StrengthIndicatorType="Text"
        PreferredPasswordLength="8"
        MinimumNumericCharacters="1"
        MinimumLowerCaseCharacters="1"
        MinimumSymbolCharacters="1"
        MinimumUpperCaseCharacters="1"
        TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
        TextStrengthDescriptionStyles="bg-danger;bg-danger;bg-warning;bg-success;bg-success"
        CalculationWeightings="50;15;15;20"
        runat="server" />
    </div>
  </div>
  <br />
  <div class="row">
    <div class="col-sm-5">
      Confirm Password:
      <asp:TextBox ID="txtConfirmPassword" runat="server" placeholder="Confirm Password" MaxLength="100" CssClass="form-control" TextMode="Password" />
    </div>
  </div>
  <br />
  <div class="row">
    <div class="col-sm-5">
       <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" CssClass="btn btn-primary" Text="Sign Up Now!" ValidationGroup="Login" />
    </div>
  </div>
  <br />
  <br />
  <p>
    <asp:RegularExpressionValidator ID="regexpSSN" runat="server" ErrorMessage="Incorrect SSN Number" ControlToValidate="txtSSN"  ValidationGroup="Login" ValidationExpression="^\d{3}-\d{2}-\d{4}$" Display="Dynamic" CssClass="alert-danger" />
    <asp:CompareValidator runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match" ValidationGroup="Login" CssClass="alert-danger" Display="Dynamic" />
    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" ValidationGroup="Login" CssClass="alert-danger" Display="Dynamic" />
    <asp:RequiredFieldValidator runat="server" ID="rfvSSN" ControlToValidate="txtSSN" ErrorMessage="SSN Is required" Display="Dynamic" CssClass="alert-danger"  ValidationGroup="Login" />

    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" ValidationGroup="Login" CssClass="alert-danger" Display="Dynamic" />
    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Email is required" ValidationGroup="Login" CssClass="alert-danger" Display="Dynamic" />
    <asp:RegularExpressionValidator runat="server" ErrorMessage="Incorrect Email Address" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$" ValidationGroup="Login" CssClass="alert-danger" Display="Dynamic" />
    <asp:RegularExpressionValidator runat="server" ErrorMessage="Password requirements not met" ControlToValidate="txtPassword" ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,100}$" CssClass="alert-danger" ValidationGroup="Login" Display="Dynamic" />
  </p>
</asp:Content>
