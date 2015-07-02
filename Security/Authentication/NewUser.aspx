<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NewUser.aspx.cs" Inherits="Authentication_NewUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
  <h2>Sign Up</h2>
  <br />
  <div style="float: right; padding-right: 40px;">
    <b>Your Password must...</b>
  <ul>
    <li>have a length of 8 or greater</li>
    <li>contain a minimum of 1 upper case letter</li>
    <li>contain a minimum of 1 lower case letter</li>
    <li>contain a minimum of 1 number</li>
    <li>contain a minimum of 1 special character</li>
  </ul>
  </div>
  <div style="float: left;">
  <div class="row">
    <div class="col-sm-10">
      Email:
      <asp:TextBox ID="txtEmail" runat="server" placeholder="example@email.com" MaxLength="254" CssClass="form-control" />
    </div>
  </div>
  <br />
  <div class="row">
    <div class="col-sm-10">
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
    <div class="col-sm-10">
      Confirm Password:
      <asp:TextBox ID="txtConfirmPassword" runat="server" placeholder="Confirm Password" MaxLength="100" CssClass="form-control" TextMode="Password" />
    </div>
  </div>
  <p>
    <asp:CompareValidator runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match" ValidationGroup="Login" CssClass="alert-danger" />
    <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ControlToValidate="txtEmail" ErrorMessage="Email is required" ValidationGroup="Login" CssClass="alert-danger" />
    <asp:RequiredFieldValidator runat="server" ID="revPassword" ControlToValidate="txtPassword" ErrorMessage="Password is required" ValidationGroup="Login" CssClass="alert-danger" />
    <asp:RequiredFieldValidator runat="server" ID="revConfirmPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Email is required" ValidationGroup="Login" CssClass="alert-danger" />
    <asp:RegularExpressionValidator ID="regexEmail" runat="server" ErrorMessage="Incorrect Email Address" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$" Display="Dynamic" ValidationGroup="Login" CssClass="alert-danger" />
    <asp:RegularExpressionValidator ID="regexpPassword" runat="server" ErrorMessage="Password requirements not net" ControlToValidate="txtPassword" ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,100}$" Display="Dynamic" CssClass="alert-danger" ValidationGroup="Login" />
  </p>
  <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" CssClass="btn btn-primary" Text="Sign Up" ValidationGroup="Login" />
    </div>
</asp:Content>
