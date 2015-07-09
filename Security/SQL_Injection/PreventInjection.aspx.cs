using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class SQL_Injection_PreventInjection : Page
{
    protected void SafeSql(object sender, EventArgs e)
    {
        Page.Validate();
        litGoodJob.Visible = Page.IsValid;
        var ssn = txtSSN.Text;

        if (Page.IsValid)
            litEmail.Text = SafeSqlQuery(ssn);
    }

    protected void UnsafeSql(object sender, EventArgs e)
    {
        var ssn = txtUnsafeSSN.Text;
        UnprotectedSqlQuery(ssn);
    }

    private static void UnprotectedSqlQuery(string ssn)
    {
        var sql = @"
            SELECT 
                FirstName, LastName, UserID
            FROM 
                Users
            WHERE
                SSN = '" + ssn + "'";
    }

    private static string SafeSqlQuery(string ssn)
    {
        var connection = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Primary"].ConnectionString;
        const string sql = @"
            SELECT 
                email
            FROM 
                Users
            WHERE
                SSN = @ssn";

        // Even better, use SPROCs.  
        // On the DB side, enforce "Least Privilege"

        var email = string.Empty;

            using (var con = new SqlConnection(connection))
            {
                using (var command = new SqlCommand(sql, con))
                {
                    con.Open();
                    command.Parameters.Add(new SqlParameter("ssn", ssn));
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        email = reader.GetString(0);
                    }
                }
            }

        return email;
    }
}