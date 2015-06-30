﻿using System;

using System.Security.Cryptography;

public partial class Password_Example : System.Web.UI.Page
{
    const string _fixedSalt =  @"pYi1BgjONo/2RQ==";
    protected void Page_Load(object sender, EventArgs e)
    {

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
        lblSalt.Text = salt;
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
        SHA256Managed sha256Managed = new SHA256Managed();
        byte[] hash = sha256Managed.ComputeHash(bytes);

        return System.Text.Encoding.Default.GetString(hash);
    }

    protected void OnClick(object sender, EventArgs e)
    {
        //string salt = CreateSalt(10);
        string salt = _fixedSalt;
        string hashPassword = GenerateShaw256Hash(txtPassword.Text, salt);

        lblOutput.Text = hashPassword;
        
    }
}