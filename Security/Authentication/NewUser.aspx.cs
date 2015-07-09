using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

public partial class Authentication_NewUser : Page
{
    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        var password = txtPassword.Text;
        var email = txtEmail.Text;
        var ssn = txtSSN.Text;

        var salt = Yoda.Security.CreateSalt();
        var hashPassword = Yoda.Security.GenerateShawHash(password, salt);
        SaveUser(email, ssn, hashPassword, salt);
        Response.Redirect("~/Default.aspx");
    }

    private static void SaveUser(string email, string ssn, string password, string salt)
    {
        var connection = WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        const string sql = @"
           INSERT INTO Users 
                (email, ssn, password, salt, changed) 
            VALUES
                (@email, @ssn, @password, @salt, @changed)";

        using (var con = new SqlConnection(connection))
        {
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = email;
                command.Parameters.Add("ssn", SqlDbType.VarChar, 11).Value = ssn;
                command.Parameters.Add("password", SqlDbType.VarChar, 100).Value = password;
                command.Parameters.Add("salt", SqlDbType.VarChar, 50).Value = salt;
                command.Parameters.Add("changed", SqlDbType.DateTime).Value = DateTime.Now;

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
    }
}