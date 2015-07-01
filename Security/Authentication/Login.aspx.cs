﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;

public partial class Authentication_Login : Page
{
    private static bool _emailExists = false;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        var email = txtEmail.Text.ToLower().Trim();
        var password = txtPassword.Text;

        if (ValidatePassword(email, password))
            {
                // TODO: Make sure user can try again and is not locked out.
                // TODO: Clear failed attempt count.
                FormsAuthentication.RedirectFromLoginPage(password, false);
            }
            else
            {
                // TODO: Add the attempt to the failure table.  Increase the count.  If it is >= maxFail, lock the account for (X)Min. Show number of attempts.
                lblError.Visible = true;
            }
        }


    private static string GetSaltForUser(string email)
    {
        var connection = WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        var sql = @"
            SELECT 
                salt
            FROM 
                Users
            WHERE
                email = @email";

        var salt = string.Empty;
        using (var con = new SqlConnection(connection))
        {
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = email;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    salt = reader.GetString(0);
                }
            }
        }

        return salt;
    }

    private static bool ValidatePassword(string email, string password)
    {
        var isValid = false;
        var salt = GetSaltForUser(email);
        var hash = GenerateShaw256Hash(password, salt);

        var connection = WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        var sql = @"
            SELECT 
               Count(*) 
            FROM 
                Users
            WHERE
                email = @email AND
                password = @password";

        using (var con = new SqlConnection(connection))
        {
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = email;
                command.Parameters.Add("password", SqlDbType.VarChar, 50).Value = hash;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isValid = Convert.ToBoolean(reader.GetInt32(0));
                }
            }
        }

        return isValid;
    }

    private static string GenerateShaw256Hash(string input, string salt)
    {
        var bytes = Encoding.UTF8.GetBytes(input + salt);
        var sha256Managed = new SHA256Managed();
        var hash = sha256Managed.ComputeHash(bytes);

        return Encoding.Default.GetString(hash);
    }

   
   




    private static void HandleFailure(string email)
    {
        
    }


}
