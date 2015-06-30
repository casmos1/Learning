<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Example.aspx.cs" Inherits="Password_Example" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
  
  <div class="row">
    <div class="col-sm-5">
     <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" placeholder="Enter Password"/>
    </div>
    <div class="col-sm-5">
     <asp:Button runat="server" Text="Generate Hash" OnClick="OnClick" CssClass="btn btn-primary"/>
    </div>
  </div>
  
  

 
  
  
  <br/>
  <br/>
  Salt: <asp:Label runat="server" ID="lblSalt" />

  <br/>
  Password: <asp:Label runat="server" ID="lblOutput" />

</asp:Content>

