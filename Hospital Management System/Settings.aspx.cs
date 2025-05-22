using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace HospitalManagementSystem
{
    public partial class Settings : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserData();
                SetRoleBadge();
                UpdateDisplayInfo();
            }
        }

        private void LoadUserData()
        {
            string currentUsername = Session["Username"]?.ToString();
            if (string.IsNullOrEmpty(currentUsername))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string connectionString = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";
            string query = "SELECT * FROM Users WHERE Username = @Username";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", currentUsername);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtUsername.Text = reader["Username"].ToString();
                    ddlRole.SelectedValue = reader["Role"].ToString();

                    // Store current password temporarily (not shown to user)
                    ViewState["CurrentPassword"] = reader["Password"].ToString();
                }
            }
        }

        private void SetRoleBadge()
        {
            string role = ddlRole.SelectedValue;
            string badgeClass = "badge bg-";

            switch (role)
            {
                case "Admin":
                    badgeClass += "danger";
                    break;
                case "Doctor":
                    badgeClass += "info";
                    break;
                case "Receptionist":
                    badgeClass += "warning text-dark";
                    break;
                case "Patient":
                    badgeClass += "success";
                    break;
                default:
                    badgeClass += "secondary";
                    break;
            }

            lblRoleBadge.CssClass = badgeClass;
            lblRoleBadge.Text = role;
        }

        private void UpdateDisplayInfo()
        {
            displayName.InnerText = $"{txtFirstName.Text} {txtLastName.Text}";
            displayEmail.InnerText = txtEmail.Text;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string currentUsername = Session["Username"]?.ToString();
            if (string.IsNullOrEmpty(currentUsername))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            string oldPasswordInput = txtOldPassword.Text.Trim();
            string currentPasswordInDb = ViewState["CurrentPassword"]?.ToString();

            // Validate old password if new password is provided
            if (!string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                if (string.IsNullOrEmpty(oldPasswordInput) || oldPasswordInput != currentPasswordInDb)
                {
                    ShowMessage("Old password is incorrect.", "danger");
                    return;
                }
            }

            string newPassword = txtNewPassword.Text.Trim();
            if (string.IsNullOrEmpty(newPassword))
            {
                newPassword = currentPasswordInDb; // Keep the old password if new one isn't provided
            }

            string connectionString = @"Data Source=DESKTOP-1DUMAFQ\SQLEXPRESS;Initial Catalog=LoginDB;Integrated Security=True";
            string updateQuery = @"UPDATE Users 
                                   SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                                       Username = @Username, Password = @Password, Role = @Role 
                                   WHERE Username = @CurrentUsername";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", newPassword);
                cmd.Parameters.AddWithValue("@Role", ddlRole.SelectedValue);
                cmd.Parameters.AddWithValue("@CurrentUsername", currentUsername);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    ShowMessage("Your information has been updated successfully!", "success");
                    Session["Username"] = txtUsername.Text; // Update session username if changed
                    UpdateDisplayInfo();
                    SetRoleBadge();
                }
                else
                {
                    ShowMessage("Update failed. Please try again.", "danger");
                }
            }
        }

       private void ShowMessage(string message, string type)
{
                    // Make sure the alert is visible
                    messageAlert.Visible = true;
    
                    // Set the appropriate CSS classes
                    messageAlert.Attributes["class"] = $"alert alert-{type} alert-dismissible fade show";
    
                    // Set the message text
                    lblMessage.Text = message;
    
                    // Register script to handle the Bootstrap alert
                    string script = $@"
                        setTimeout(function() {{
                            var alert = bootstrap.Alert.getOrCreateInstance(document.getElementById('{messageAlert.ClientID}'));
                            setTimeout(function() {{ alert.close(); }}, 5000);
                        }}, 100);";
    
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertAutoClose", script, true);
                }
    }
}