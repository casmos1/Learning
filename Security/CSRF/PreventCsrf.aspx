<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PreventCsrf.aspx.cs" Inherits="CSRF_PreventCsrf" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
      <div class="row">
      <div class="col-sm-5">
  <asp:TextBox runat="server" ID="txtCaptcha" CssClass="form-control" placeholder="000000"/> 
        </div>
        <div class="col-sm-5">
  <asp:Button ID="btnSubmit" runat="server" Text="Submit Form" OnClick="btnSubmit_OnClick" CssClass="btn btn-primary"/>
          </div>
        </div>
  <br/>
  <asp:Image ID="imgCaptcha" ImageUrl="Captcha.ashx" runat="server" />
  <br />
  <br/>
  <asp:Literal runat="server" ID="litSuccess" Text="Good Job" Visible="False" />
  
  <asp:Literal runat="server" ID="litFailure" Text="Bad, bad job" Visible="False" />
</asp:Content>

