using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security.AntiXss;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class XSS_XssExample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // HTML Encoding
        //AntiXssEncoder available in 4.5 Framework.  This is a white-list.  Available as download for older versions.

        // var encodedHtml = Server.HtmlEncode(untrustedData);

        // https://www.owasp.org/index.php/ASP.NET_Output_Encoding

    }
}