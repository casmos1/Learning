<%@ Application Language="C#" %>

<script runat="server">

  //Prevent clickjacking
  protected void Application_BeginRequest(object sender, EventArgs e)
  {
    //HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");
  }

</script>
