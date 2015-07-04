using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;

public partial class Authentication_NewUser : Page
{
    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        var password = txtPassword.Text;
        var email = txtEmail.Text;

        var salt = CreateSalt(10);
        var hashPassword = GenerateShaw256Hash(password, salt);
        SaveUser(email, hashPassword, salt);
        Response.Redirect("~/Default.aspx");
    }

    private static void SaveUser(string email, string password, string salt)
    {
        var connection = WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        var sql = @"
           INSERT INTO Users 
                (email, password, salt) 
            VALUES
                (@email, @password, @salt)";


        using (var con = new SqlConnection(connection))
        {
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = email;
                command.Parameters.Add("password", SqlDbType.VarChar, 50).Value = password;
                command.Parameters.Add("salt", SqlDbType.VarChar, 50).Value = salt;

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
    }

    private string CreateSalt(int size)
    {
        var rng = new RNGCryptoServiceProvider();
        var buff = new byte[size];
        rng.GetBytes(buff);
        return Convert.ToBase64String(buff);
    }

    public string GenerateShaw256Hash(string input, string salt)
    {
        var bytes = Encoding.UTF8.GetBytes(input + salt);
        var sha256Managed = new SHA256Managed();
        var hash = sha256Managed.ComputeHash(bytes);

        return Encoding.Default.GetString(hash);
    }
}