using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.Security.AntiXss;

public partial class SQL_Injection_PreventInjection : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void SafeSql(object sender, EventArgs e)
    {
        Page.Validate();
        litGoodJob.Visible = Page.IsValid;
        var ssn = txtSSN.Text;

        if (Page.IsValid)
            SafeSqlQuery(ssn);
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

    private static void SafeSqlQuery(string ssn)
    {
        var connection = "Connection String Goes Here";
        var sql = @"
            SELECT 
                FirstName, LastName, UserID
            FROM 
                Users
            WHERE
                SSN = @ssn";

        // Even better, use SPROCs.  
        // On the DB side, enforce "Least Privilege"
         

        try
        {
            using (var con = new SqlConnection(connection))
            {
                using (var command = new SqlCommand(sql, con))
                {
                    command.Parameters.Add(new SqlParameter("@ssn", ssn));
                    // Do something
                }
            }
        }
        catch (ArgumentException)
        {
            // Log the exception
        }
    }
}