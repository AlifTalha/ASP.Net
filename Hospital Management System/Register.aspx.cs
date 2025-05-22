using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalManagementSystem
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlMessage.Visible = false;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowErrorMessage("Please fill in all required fields");
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ShowErrorMessage("Passwords do not match");
                return;
            }

            if (txtPassword.Text.Length < 8)
            {
                ShowErrorMessage("Password must be at least 8 characters long");
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                ShowErrorMessage("Please enter a valid email address");
                return;
            }

            string connectionString = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                checkCommand.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                try
                {
                    connection.Open();
                    int userCount = (int)checkCommand.ExecuteScalar();

                    if (userCount > 0)
                    {
                        ShowErrorMessage("Username or email already exists");
                        return;
                    }

                    // Store password in plain text (not recommended for production)
                    string password = txtPassword.Text.Trim();

                    string insertQuery = @"INSERT INTO Users 
                        (FirstName, LastName, Email, Username, Password, Role, CreatedDate) 
                        VALUES 
                        (@FirstName, @LastName, @Email, @Username, @Password, @Role, @CreatedDate)";

                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    insertCommand.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    insertCommand.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    insertCommand.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    insertCommand.Parameters.AddWithValue("@Password", password); // Storing plain text password
                    insertCommand.Parameters.AddWithValue("@Role", ddlRole.SelectedValue);
                    insertCommand.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ShowSuccessMessage("🎉 Registration successful! Redirecting to login...");

                        // Add client-side script to show confetti
                        ScriptManager.RegisterStartupScript(this, GetType(), "confetti",
                            "confettiAnimation();", true);

                        // Redirect to login after 3 seconds
                        ScriptManager.RegisterStartupScript(this, GetType(), "redirect",
                            "setTimeout(function(){ window.location.href = 'Login.aspx'; }, 3000);", true);
                    }
                    else
                    {
                        ShowErrorMessage("Registration failed. Please try again.");
                    }
                }
                catch (SqlException ex)
                {
                    ShowErrorMessage("Database error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("Error: " + ex.Message);
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void ShowErrorMessage(string message)
        {
            pnlMessage.Visible = true;
            lblMessage.Text = message;
            pnlMessage.CssClass = "status-message animate__animated animate__shakeX";
            pnlMessage.Style["background-color"] = "#ffebee";
            pnlMessage.Style["color"] = "#c62828";
        }

        private void ShowSuccessMessage(string message)
        {
            pnlMessage.Visible = true;
            lblMessage.Text = message;
            pnlMessage.CssClass = "status-message animate__animated animate__bounceIn";
            pnlMessage.Style["background-color"] = "#e8f5e9";
            pnlMessage.Style["color"] = "#2e7d32";

            // Clear form fields
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
    }
}