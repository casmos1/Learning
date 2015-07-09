using System;
using System.Web.UI;

public partial class CSRF_PreventCsrf : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            SetCaptchaText();
    }

    private void SetCaptchaText()
    {
        var oRandom = new Random();
        var iNumber = oRandom.Next(100000, 999999);
        Session["Captcha"] = iNumber.ToString();
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        if (Session["Captcha"].ToString() != txtCaptcha.Text.Trim())
        {
            litFailure.Visible = true;
            litSuccess.Visible = false;
        }
        else
        {
            litSuccess.Visible = true;
            litFailure.Visible = false;
        }
    }
}