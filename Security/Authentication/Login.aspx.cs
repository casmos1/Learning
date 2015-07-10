using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;

public partial class Authentication_Login : Page
{
    private string _password;
    private string _email;
    private int _attemptCount;

    private const int MaxAttempts = 3;
    private const int LogoutDurationInSeconds = 30;
    private const int ForcePasswordChangeInDays = 90;
    private const bool AccountIsLocked = true;
    private const bool ResetAuthCount = true;
    private const bool DoNotResetAuthCount = false;
    private const bool CreatePersistentCookie = false;

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        _email = txtEmail.Text.ToLower().Trim();
        _password = txtPassword.Text;
        _attemptCount = GetAttemptCount();

        if (!IsAccountLocked()) // Account is not locked.
        {
            if (ValidatePassword()) // Creds are valid
            {
                UpdateAttempt(ResetAuthCount); // Successful, reset attempts

                if (TimeToChangePassword())
                    litReset.Visible = true;
                else
                    FormsAuthentication.RedirectFromLoginPage(_email, CreatePersistentCookie);
            }
            else // Failed attempt
            {
                UpdateAttempt(DoNotResetAuthCount);
                ShowFailure(IsAccountLocked());
            }
        }
        else // Account is locked.
        {
            UpdateAttempt(DoNotResetAuthCount);
            ShowFailure(AccountIsLocked);
        }
    }

    private string GetSaltForUser()
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
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = _email;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    salt = reader.GetString(0);
                }
            }
        }

        return salt;
    }

    private bool ValidatePassword()
    {
        var isValid = false;
        var salt = GetSaltForUser();
        var hash = Yoda.Security.GenerateShawHash(_password, salt);
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
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = _email;
                command.Parameters.Add("password", SqlDbType.VarChar, 100).Value = hash;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isValid = Convert.ToBoolean(reader.GetInt32(0));
                }
            }
        }

        return isValid;
    }

    #region --- Ignore for presentation ---

    private bool CheckAttemptExists()
    {
        var recordExists = false;

        var connection = WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        var sql = @"
            SELECT 
               Count(*) 
            FROM 
                UserAuthHistory
            WHERE
                email = @email";

        using (var con = new SqlConnection(connection))
        {
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = _email;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    recordExists = Convert.ToBoolean(reader.GetInt32(0));
                }
            }
        }

        return recordExists;
    }

    private void AddFirstAttempt()
    {
        var connection = WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        var sql = @"
           INSERT INTO UserAuthHistory 
                (email) 
            VALUES
                (@email)";

        using (var con = new SqlConnection(connection))
        {
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = _email;

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
    }

    private void UpdateAttempt(bool reset)
    {
        _attemptCount++;

        if (!CheckAttemptExists())
            AddFirstAttempt();

        if (reset)
            _attemptCount = 0;

        var connection = WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        var sql = @"
           UPDATE UserAuthHistory 
                SET attempts = @count,
                lastAttempt = @currentTime
            WHERE email = @email";

        using (var con = new SqlConnection(connection))
        {
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = _email;
                command.Parameters.Add("currentTime", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("count", SqlDbType.TinyInt).Value = _attemptCount;

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
    }

    private int GetAttemptCount()
    {
        var attempts = 0;

        var connection = WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        var sql = @"
            SELECT 
               attempts
            FROM 
                UserAuthHistory
            WHERE
                email = @email";

        using (var con = new SqlConnection(connection))
        {
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = _email;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        attempts = Convert.ToInt32(reader[0]);
                    }
                }
            }
        }
        return attempts;
    }

    private DateTime GetLastAttemptDateTime()
    {
        var lastAttemptDate = Convert.ToDateTime("1/1/1980");

        var connection = WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        var sql = @"
            SELECT 
               lastAttempt
            FROM 
                UserAuthHistory
            WHERE
                email = @email";

        using (var con = new SqlConnection(connection))
        {
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = _email;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lastAttemptDate = Convert.ToDateTime(reader.GetDateTime(0));
                    }
                }
            }
        }

        return lastAttemptDate;
    }

    private bool CanTryAgainByTime()
    {
        var dbTime = GetLastAttemptDateTime();
        var timePassed = DateTime.Now.Subtract(dbTime).TotalSeconds;

        return (timePassed >= LogoutDurationInSeconds);
    }

    private bool IsAccountLocked()
    {
        var hasExceededMaxAttempts = (GetAttemptCount() >= MaxAttempts);
        var canTryAgain = CanTryAgainByTime();

        return (hasExceededMaxAttempts && !canTryAgain);
    }

    private void ShowFailure(bool isLocked)
    {
        var message = (isLocked)
            ? string.Format("{0} failed attempts, your account has been locked", _attemptCount)
            : string.Format("{0} failed attempts", _attemptCount);

        litAttemptCount.Text = message;
        litAttemptCount.Visible = true;
        litGenericError.Visible = true;

        if (isLocked)
        {
            btnSubmit.Enabled = false;
            txtEmail.Enabled = false;
            txtPassword.Enabled = false;
            txtEmail.Text = "";
            txtPassword.Text = "";
        }
    }

    private DateTime GetLastChangedDate()
    {
        var lastChangedDate = Convert.ToDateTime("1/1/1980");
        var connection = WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        var sql = @"
            SELECT 
              changed 
            FROM 
                Users
            WHERE
                email = @email";

        using (var con = new SqlConnection(connection))
        {
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                command.Parameters.Add("email", SqlDbType.VarChar, 50).Value = _email;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lastChangedDate = Convert.ToDateTime(reader.GetDateTime(0));
                    }
                }
            }
        }
        return lastChangedDate;
    }

    private bool TimeToChangePassword()
    {
        var dbTime = GetLastChangedDate();
        var timePassed = DateTime.Now.Subtract(dbTime).TotalDays;

        return (timePassed >= ForcePasswordChangeInDays);
    }

    #endregion
}