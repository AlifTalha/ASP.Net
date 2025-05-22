using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace HospitalManagementSystem
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserId, Username, FirstName, LastName, Role FROM Users WHERE Username=@Username AND Password=@Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                command.Parameters.AddWithValue("@Password", txtPassword.Text.Trim()); // In production, use hashed passwords

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        // Store all user data in session
                        Session["UserId"] = reader["UserId"].ToString();
                        Session["Username"] = reader["Username"].ToString();
                        Session["FirstName"] = reader["FirstName"].ToString();
                        Session["LastName"] = reader["LastName"].ToString();
                        Session["Role"] = reader["Role"].ToString();

                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        lblErrorMessage.Text = "Invalid username or password";
                        lblErrorMessage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = "Error: " + ex.Message;
                    lblErrorMessage.Visible = true;
                }
            }
        }
    }
}