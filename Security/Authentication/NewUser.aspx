<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NewUser.aspx.cs" Inherits="Authentication_NewUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
  <h2>Sign Up</h2>

  <div class="row">
    <div class="col-sm-5">
      SSN: <asp:TextBox ID="txtSSN" runat="server" placeholder="XXX-XX-XXXX" MaxLength="11" CssClass="form-control" onkeyup="this.value=this.value.replace(/[^\d-]/,'')"/>
    </div>
  </div>

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
        runat="server"/>
    </div>
  </div>
  <p>
    <asp:RegularExpressionValidator ID="regexEmail" runat="server" ErrorMessage="Incorrect Email Address" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$" Display="Dynamic" ValidationGroup="grpSSN" CssClass="alert-danger"/>
    <asp:RegularExpressionValidator ID="regexpSSN" runat="server" ErrorMessage="Incorrect SSN Number" ControlToValidate="txtSSN" ValidationExpression="^\d{3}-\d{2}-\d{4}$" Display="Dynamic" ValidationGroup="grpSSN" CssClass="alert-danger"/>
    <asp:RegularExpressionValidator ID="regexpPassword" runat="server" ErrorMessage="Bad Password" ControlToValidate="txtPassword" ValidationExpression="^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,80}$" Display="Dynamic" ValidationGroup="grpSSN" CssClass="alert-danger"/>
    <asp:RequiredFieldValidator runat="server" ID="rfvSSN" ControlToValidate="txtSSN" ErrorMessage="SSN Is required" Display="Dynamic" ValidationGroup="grpSSN" CssClass="alert-danger"/>

  </p>
  <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" CssClass="btn btn-primary" Text="Sign Up"/>
</asp:Content>